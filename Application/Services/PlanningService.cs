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

        public async Task<List<TimeRange>> GetTimeRangesBySportAsync(Guid sportId)
        {
            return await _unitOfWork.PlanningRepository.GetTimeRangesBySportAsync(sportId);
        }


        // Get available time ranges for a specific sport and day..
        //public async Task<List<TimeRange>> GetTimeRangesBySportAndDayAsync(Guid sportId, DayOfWeekEnum day)
        //{
        //    // Fetch the sport to ensure it exists
        //    var sport = await _unitOfWork.SportRepository.GetAsync(s => s.Id == sportId);
        //    if (sport == null)
        //    {
        //        return new List<TimeRange>(); // Sport not found
        //    }

        //    // Fetch planning entries for the specific sport and day
        //    var plannings = await _unitOfWork.PlanningRepository.GetPlanningsBySportAndDayAsync(sportId, day);

        //    // Extract available time ranges from the planning entries
        //    var availableTimeRanges = plannings.SelectMany(p => p.TimeRanges).ToList();

        //    // Fetch reserved reservations for the sport on the specified day
        //    var reservedReservations = await _unitOfWork.ReservationRepository.GetReservationsBySportIdAsync(sportId);

        //    // Filter reserved time ranges for the specified day
        //    var reservedTimeRanges = reservedReservations
        //        .Where(r => r.DayBooking == day) // Only keep reservations that match the specified day
        //        .Select(r => new { r.HourStart, r.HourEnd })
        //        .ToList();

        //    // Filter out the time ranges that exist in Reservations for the specified day
        //    var filteredTimeRanges = availableTimeRanges
        //        .Where(tr => !reservedTimeRanges
        //            .Any(res => res.HourStart == tr.HourStart && res.HourEnd == tr.HourEnd))
        //        .ToList();

        //    return filteredTimeRanges;
        //}

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




        //public async Task<List<TimeRange>> GetTimeRangesBySportAndDayAsync(Guid sportId, DayOfWeekEnum day)
        //{
        //    // Fetch the sport to ensure it exists
        //    var sport = await _unitOfWork.SportRepository.GetAsync(s => s.Id == sportId);
        //    if (sport == null)
        //    {
        //        return new List<TimeRange>(); // Sport not found
        //    }

        //    // Fetch today's date
        //    var today = DateOnly.FromDateTime(DateTime.UtcNow);

        //    // Fetch reservations to check if any reservation exists for today
        //    var reservationsForSport = await _unitOfWork.ReservationRepository.GetReservationsBySportIdAsync(sportId);

        //    // Check if today's date matches the OnlyDate in any of the reservations
        //    bool isTodayReserved = reservationsForSport.Any(r => r.OnlyDate == today && r.DayBooking == day);

        //    if (isTodayReserved)
        //    {
        //        // Call the method to fetch time ranges for today's date and the specified day
        //        var plannings = await _unitOfWork.PlanningRepository.GetPlanningsBySportAndDayAsync(sportId, day);

        //        // Extract available time ranges from the planning entries
        //        var availableTimeRanges = plannings.SelectMany(p => p.TimeRanges).ToList();

        //        // Filter out the reserved time ranges for today
        //        var reservedTimeRanges = reservationsForSport
        //            .Where(r => r.OnlyDate == today && r.DayBooking == day) // Filter by today's date and the specified day
        //            .Select(r => new { r.HourStart, r.HourEnd })
        //            .ToList();

        //        // Filter out the time ranges that exist in Reservations for today
        //        var filteredTimeRanges = availableTimeRanges
        //            .Where(tr => !reservedTimeRanges
        //                .Any(res => res.HourStart == tr.HourStart && res.HourEnd == tr.HourEnd))
        //            .ToList();

        //        return filteredTimeRanges;
        //    }
        //    else
        //    {
        //        // If today's date doesn't match, return all available HourStart and HourEnd
        //        var allTimeRanges = await _unitOfWork.PlanningRepository.GetTimeRangesBySportAsync(sportId);
        //        return allTimeRanges;
        //    }
        //}












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





       


    }
}
