using Application.IRepository;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Db;
using Infrastructure.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class PlanningRepository : GenericRepository<Planning>, IPlanningRepository
    {
        private readonly ApplicationDbContext _context;

        public PlanningRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<TimeRange>> GetTimeRangesBySportAsync(Guid sportId)
        {
            // Get all TimeRanges from Planning filtered by SportId
            var planningTimeRanges = await _context.Plannings
                .Where(p => p.SportId == sportId)
                .Include(p => p.TimeRanges)
                .SelectMany(p => p.TimeRanges)
                .ToListAsync();

            // Get all HourStart and HourEnd from Reservations filtered by SportId
            var reservedTimeRanges = await _context.Reservations
                .Where(r => r.SportId == sportId)
                .Select(r => new { r.HourStart, r.HourEnd })
                .ToListAsync();

            // Filter out the time ranges that exist in Reservations
            var availableTimeRanges = planningTimeRanges
                .Where(tr => !reservedTimeRanges
                    .Any(res => res.HourStart == tr.HourStart && res.HourEnd == tr.HourEnd))
                .ToList();

            return availableTimeRanges;
        }


        // Get plannings ------------
        public async Task<List<Planning>> GetPlanningsBySportAndDayAsync(Guid sportId, DayOfWeekEnum day)
        {
            var planningTimeRange = await _context.Set<Planning>()
                .Where(p => p.SportId == sportId && p.Day == day)
                .Include(p => p.TimeRanges) // Ensure TimeRanges are included
                .ToListAsync();

            return planningTimeRange;
        }


        public async Task AddAsync(Planning planning)
        {
            planning.Id = Guid.NewGuid();
            await _context.Plannings.AddAsync(planning);
        }

        public async Task<Planning> FindAsync(Expression<Func<Planning, bool>> predicate)
        {
            return await _context.Plannings.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Planning>> GetPlanningsWithTimeRangesAsync()
        {
            return await _context.Plannings.Include(p => p.TimeRanges).ToListAsync();
        }

        public async Task<List<TimeRange>> GetAvailableTimeRangesAsync()
        {
            var planningTimeRanges = await _context.Plannings
                .Include(p => p.TimeRanges)
                .SelectMany(p => p.TimeRanges)
                .ToListAsync();

            var reservedTimeRanges = await _context.Reservations
                .Select(r => new { r.HourStart, r.HourEnd })
                .ToListAsync();

            var availableTimeRanges = planningTimeRanges
                .Where(tr => !reservedTimeRanges
                    .Any(res => res.HourStart == tr.HourStart && res.HourEnd == tr.HourEnd))
                .ToList();

            return availableTimeRanges;
        }

        public async Task<List<TimeRange>> GetTimeRangesBySportAndDayNotExistOnTableReservationAsync(Guid sportId, DayOfWeekEnum day)
        {
            // Get all TimeRanges from Planning filtered by SportId and Day
            var availableTimeRanges = await _context.Plannings
                .Where(p => p.SportId == sportId && p.Day == day)
                .Include(p => p.TimeRanges)
                .SelectMany(p => p.TimeRanges)
                .ToListAsync();

            // Get all HourStart and HourEnd from Reservations filtered by SportId
            var reservedTimeRanges = await _context.Reservations
                .Where(r => r.SportId == sportId && r.OnlyDate.DayOfWeek == (DayOfWeek)day)
                .Select(r => new { r.HourStart, r.HourEnd })
                .ToListAsync();

            // Filter out the TimeRanges from Planning that exist in the Reservation table
            var unreservedTimeRanges = availableTimeRanges
                .Where(tr => !reservedTimeRanges.Any(res => res.HourStart == tr.HourStart && res.HourEnd == tr.HourEnd))
                .ToList();

            return unreservedTimeRanges;
        }


    }
}
