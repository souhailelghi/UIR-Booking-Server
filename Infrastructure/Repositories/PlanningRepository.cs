using Application.IRepository;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Db;
using Infrastructure.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PlanningRepository : GenericRepository<Planning>, IPlanningRepository
    {
        private readonly ApplicationDbContext _context;

        public PlanningRepository(ApplicationDbContext context) : base(context)
        {
           _context = context;
        }

        // Adding a new planning entry
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
            // Get all TimeRanges from Planning
            var planningTimeRanges = await _context.Plannings
                .Include(p => p.TimeRanges)
                .SelectMany(p => p.TimeRanges)
                .ToListAsync();

            // Get all HourStart and HourEnd from Reservations
            var reservedTimeRanges = await _context.Reservations
                .Select(r => new { r.HourStart, r.HourEnd })
                .ToListAsync();

            // Filter out the time ranges that exist in Reservations
            var availableTimeRanges = planningTimeRanges
                .Where(tr => !reservedTimeRanges
                    .Any(res => res.HourStart == tr.HourStart && res.HourEnd == tr.HourEnd))
                .ToList();

            return availableTimeRanges;
        }

        public async Task<List<TimeRange>> GetAvailableTimeRangesBySportAsync(Guid sportId)
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


        public async Task<List<Planning>> GetPlanningsBySportAndDayAsync(Guid sportId, DayOfWeekEnum day)
        {
            return await _context.Set<Planning>()
                .Where(p => p.SportId == sportId && p.Day == day)
                .Include(p => p.TimeRanges) // Include time ranges
                .ToListAsync();
        }
    }
}
