using Application.IServices;
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



        public async Task<List<TimeRange>> GetTimeRangesBySportAndDayAsync(Guid sportId, DayOfWeekEnum day)
        {
            // Fetch the sport to ensure it exists
            var sport = await _unitOfWork.SportRepository.GetAsync(s => s.Id == sportId);
            if (sport == null)
            {
                return new List<TimeRange>(); // Sport not found
            }

            // Fetch planning entries for the specific sport and day
            var plannings = await _unitOfWork.PlanningRepository.GetPlanningsBySportAndDayAsync(sportId, day);

            // Extract available time ranges from the planning entries
            var availableTimeRanges = plannings.SelectMany(p => p.TimeRanges).ToList();

            // Fetch reserved reservations for the sport
            var reservedReservations = await _unitOfWork.ReservationRepository.GetReservationsBySportIdAsync(sportId);

            // Check if today's date matches the 'OnlyDate' field in Reservation
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var matchingReservation = reservedReservations.FirstOrDefault(r => r.OnlyDate == today && r.DayBooking == day);

            if (matchingReservation != null)
            {
                // If today's date matches 'OnlyDate', filter based on the reserved time ranges for today
                var reservedTimeRanges = reservedReservations
                    .Where(r => r.OnlyDate == today && r.DayBooking == day)
                    .Select(r => new { r.HourStart, r.HourEnd })
                    .ToList();

                // Filter out the reserved time ranges from available time ranges
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

        public async Task<Planning> GetPlanningByIdAsync(Guid id)
        {

            Planning planning = await _unitOfWork.PlanningRepository.GetAsNoTracking(u => u.Id == id);

            return planning;
        }
    }
}
