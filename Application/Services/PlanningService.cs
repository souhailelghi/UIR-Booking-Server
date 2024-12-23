﻿using Application.IServices;
using Application.IUnitOfWorks;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;



namespace Application.Services
{
    public class PlanningService : IPlanningService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
 

        public PlanningService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        
        }

        public async Task DeletePlanningAsync(Guid id)
        {
            Planning planning = await _unitOfWork.PlanningRepository.GetAsNoTracking(u => u.Id == id);
            if (planning == null)
            {
                throw new ArgumentException("Planning not found.");
            }

            try
            {

                await _unitOfWork.PlanningRepository.RemoveAsync(planning);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }




        public async Task<List<Planning>> GetAllPlanningsBySportId(Guid sportId)
        {
            // Fetch all plannings for the specified SportId, now including TimeRanges
            var plannings = await _unitOfWork.PlanningRepository.GetAllAsync(p => p.SportId == sportId);

            return plannings ?? new List<Planning>();
        }




        public async Task<List<TimeRange>> GetTimeRangesByReferenceSportAndDayAsync(int referenceSport, DayOfWeekEnum day)
        {
            // Fetch the sport by referenceSport to ensure it exists
            var sport = await _unitOfWork.SportRepository.GetAsync(s => s.ReferenceSport == referenceSport);
            if (sport == null)
            {
                return new List<TimeRange>(); // Reference sport not found
            }

            // Fetch planning entries for the specific sport and day
            var plannings = await _unitOfWork.PlanningRepository.GetPlanningsBySportAndDayAsync(sport.Id, day);

            // Extract available time ranges from the planning entries
            var availableTimeRanges = plannings.SelectMany(p => p.TimeRanges).ToList();

            // Fetch reserved reservations for the sport
            var reservedReservations = await _unitOfWork.ReservationRepository.GetReservationsBySportIdAsync(sport.Id);

            // Check if today's date matches the 'OnlyDate' field in Reservation
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var matchingReservation = reservedReservations.FirstOrDefault(r => r.OnlyDate == today && r.DayBooking == day);

            if (matchingReservation != null)
            {
                // Filter out the reserved time ranges for today
                var reservedTimeRanges = reservedReservations
                    .Where(r => r.OnlyDate == today && r.DayBooking == day)
                    .Select(r => new { r.HourStart, r.HourEnd })
                    .ToList();

                var filteredTimeRanges = availableTimeRanges
                    .Where(tr => !reservedTimeRanges
                        .Any(res => res.HourStart == tr.HourStart && res.HourEnd == tr.HourEnd))
                    .ToList();

                return filteredTimeRanges;
            }
            else
            {
                // If the day doesn't match 'OnlyDate', return all available time ranges for that sport and day
                return availableTimeRanges;
            }
        }

     

        public async Task<List<TimeRange>> GetTimeRangesBySportAndDayAsync(Guid sportId)
        {
            // Fetch the sport to ensure it exists
            var sport = await _unitOfWork.SportRepository.GetAsync(s => s.Id == sportId);
            if (sport == null)
            {
                return new List<TimeRange>(); // Sport not found
            }

            // Determine today's day of the week
            var today = DateTime.Now; // Use current UTC time without adjustment
            var todayDayOfWeek = GetDayOfWeekEnum(today.DayOfWeek);

            // Log the day for debugging
            var tt = todayDayOfWeek.ToString();

            // Fetch planning entries for the specific sport and today's day
            var plannings = await _unitOfWork.PlanningRepository.GetPlanningsBySportAndDayAsync(sportId, todayDayOfWeek);

            // Extract available time ranges from the planning entries
            var availableTimeRanges = plannings.SelectMany(p => p.TimeRanges).ToList();

            // Fetch reservations for today associated with the sport
            var todayDateOnly = DateOnly.FromDateTime(today);
            var reservations = await _unitOfWork.ReservationRepository.GetReservationsBySportIdAsync(sportId);

            // Filter reservations for today
            var todayReservations = reservations
                .Where(r => r.OnlyDate == todayDateOnly)
                .Select(r => new { r.HourStart, r.HourEnd })
                .ToList();

            // Filter out the reserved time ranges from available time ranges
            var filteredTimeRanges = availableTimeRanges
                .Where(tr => !todayReservations
                    .Any(res => res.HourStart < tr.HourEnd && res.HourEnd > tr.HourStart)) // Overlap check
                .ToList();
            // Additional filtering to remove times earlier than the current time
            var currentTime = today.TimeOfDay;
            var finalFilteredTimeRanges = filteredTimeRanges
    .Where(tr => tr.HourStart >= currentTime)
    .ToList();

            //return filteredTimeRanges;
            return finalFilteredTimeRanges;
        }

        // Helper method to map System.DayOfWeek to DayOfWeekEnum
        private DayOfWeekEnum GetDayOfWeekEnum(DayOfWeek systemDayOfWeek)
        {
            return systemDayOfWeek switch
            {
                DayOfWeek.Sunday => DayOfWeekEnum.Sunday,
                DayOfWeek.Monday => DayOfWeekEnum.Monday,
                DayOfWeek.Tuesday => DayOfWeekEnum.Tuesday,
                DayOfWeek.Wednesday => DayOfWeekEnum.Wednesday,
                DayOfWeek.Thursday => DayOfWeekEnum.Thursday,
                DayOfWeek.Friday => DayOfWeekEnum.Friday,
                DayOfWeek.Saturday => DayOfWeekEnum.Saturday,
                _ => throw new ArgumentOutOfRangeException(nameof(systemDayOfWeek), "Invalid day of the week"),
            };
        }


    
        public async Task<List<TimeRange>> GetTimeRangesBySportAsync(Guid sportId)
        {
            return await _unitOfWork.PlanningRepository.GetTimeRangesBySportAsync(sportId);
        }


        public async Task<Planning> AddPlanningAsync(Planning planning)
        {
            // Check if a planning already exists for the same SportId and Day
            var existingPlanning = await _unitOfWork.PlanningRepository
                .FindAsync(p => p.SportId == planning.SportId && p.Day == planning.Day);

            if (existingPlanning != null)
            {
                throw new InvalidOperationException("Planning for this sport and day already exists.");
            }

            // Add the planning and its time ranges
            planning.Id = Guid.NewGuid();
            planning.DateCreation = DateTime.UtcNow;

            await _unitOfWork.PlanningRepository.AddAsync(planning);
            await _unitOfWork.CommitAsync();

            return planning;
        }

        public async Task<List<Planning>> GetAllPlanningAsync()
        {
            
            List<Planning> PlanningsList = await _unitOfWork.Plannings
                .Include(p => p.TimeRanges) 
                .ToListAsync();

            return PlanningsList;
        }


        public async Task<List<TimeRange>> GetAvailableTimeRangesAsync()
        {
            return await _unitOfWork.PlanningRepository.GetAvailableTimeRangesAsync();
        }

        public async Task UpdatePlanningAsync(Planning planning)
        {
            if (planning == null)
            {
                throw new ArgumentNullException(nameof(Planning));
            }

            Planning existingplanning = await _unitOfWork.PlanningRepository.GetAsNoTracking(
                d => d.Id == planning.Id);
            if (existingplanning == null)
            {
                throw new ArgumentException("planning not found.");
            }

            existingplanning.Day = planning.Day;
            existingplanning.TimeRanges = planning.TimeRanges;


            try
            {
                await _unitOfWork.PlanningRepository.UpdateAsync(existingplanning);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {

                throw new ArgumentException($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        //public async Task<Planning> GetPlanningByIdAsync(Guid id)
        //{

        //    Planning planning = await _unitOfWork.PlanningRepository.GetAsNoTracking(u => u.Id == id);

        //    return planning;
        //}

        public async Task<Planning> GetPlanningByIdAsync(Guid id)
        {
            // Fetch the planning entry by ID, including related TimeRanges
            var query = _unitOfWork.PlanningRepository
                .GetAsNoTrackings(p => p.Id == id); // Keep it IQueryable

            // Now include the related TimeRanges
            var planning = await query
                .Include(p => p.TimeRanges) // Include TimeRanges
                .FirstOrDefaultAsync(); // Now await the task

            if (planning == null)
            {
                throw new KeyNotFoundException("Planning not found.");
            }

            return planning;
        }







    }
}
