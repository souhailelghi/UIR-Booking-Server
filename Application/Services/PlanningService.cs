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


        // Get available time ranges for a specific sport and day ----------------------------service
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

            // Fetch reserved time ranges for the sport
            var reservedReservations = await _unitOfWork.ReservationRepository.GetReservationsBySportIdAsync(sportId);
            var reservedTimeRanges = reservedReservations.Select(r => new { r.HourStart, r.HourEnd }).ToList();

            // Filter out the time ranges that exist in Reservations
            var filteredTimeRanges = availableTimeRanges
                .Where(tr => !reservedTimeRanges
                    .Any(res => res.HourStart == tr.HourStart && res.HourEnd == tr.HourEnd))
                .ToList();

            return filteredTimeRanges;
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





        //----
      

        public async Task<List<TimeRange>> GetTimeRangesBySportAndDayNotExistOnTableReservationAsync(Guid sportId, DayOfWeekEnum day)
        {
            return await _unitOfWork.PlanningRepository.GetTimeRangesBySportAndDayNotExistOnTableReservationAsync(sportId, day);
        }


    }
}
