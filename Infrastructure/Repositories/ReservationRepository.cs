using Application.IRepository;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Db;
using Infrastructure.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation> , IReservationRepository
    {

        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;   
        }



        public async Task<List<Reservation>> GetReservationsBySportIdAsync(Guid sportId)
        {
            return await _context.Reservations
                .Where(r => r.SportId == sportId)
                .ToListAsync();
        }

        public async Task<List<Reservation>> GetReservationsForDateAsync( Guid studentId, List<Guid> teamMembersIds)
        {
            return await _context.Reservations
                .Where(r => (r.StudentId == studentId || teamMembersIds.Contains(r.StudentId)))
                .ToListAsync();
        }





        public async Task RemoveAllAsync()
        {
            var reservations = await dbSet.ToListAsync(); // Use dbSet from GenericRepository
            if (reservations.Count > 0)
            {
                dbSet.RemoveRange(reservations);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<List<TimeRange>> GetReservedTimeRangesBySportAndDayAsync(Guid sportId, DayOfWeekEnum day)
        {
            // Query to get reservations for the specified sport
            var reservations = await _context.Reservations
                .Where(r => r.SportId == sportId)
                .ToListAsync();

            // Perform day comparison in-memory
            var reservedTimeRanges = reservations
                .Where(r => r.OnlyDate.DayOfWeek == (DayOfWeek)day)
                .Select(r => new TimeRange
                {
                    HourStart = r.HourStart,
                    HourEnd = r.HourEnd
                })
                .ToList();

            return reservedTimeRanges;
        }



        public async Task<Reservation> GetAsync(Expression<Func<Reservation, bool>> filter)
        {
            return await dbSet.FirstOrDefaultAsync(filter);
        }
    }
}
