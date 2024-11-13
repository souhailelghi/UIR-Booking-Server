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
        public async Task<List<Reservation>> GetReservationsByStudentIdAsync(Guid studentId)
        {
            return await _context.Reservations
                .Where(r => r.StudentId == studentId)
                .ToListAsync();
        }


        public async Task<Reservation> GetAsync(Expression<Func<Reservation, bool>> filter)
        {
            return await dbSet.FirstOrDefaultAsync(filter);
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


        // new command : 
        // Fetch reservations by ReferenceSport for a single student
        public async Task<List<Reservation>> GetReservationsByReferenceSportAsync(Guid studentId, int referenceSport)
        {
            return await _context.Reservations
                .Include(r => r.Sport) // Ensure Sport is loaded
                .Where(r => r.Sport.ReferenceSport == referenceSport && r.StudentId == studentId)
                .ToListAsync();
        }

        //public async Task<List<Reservation>> GetReservationsByReferenceSportForTeamAsync(List<Guid> teamMemberIds, int referenceSport)
        //{
        //    var res = await _context.Reservations
        //        .Include(r => r.Sport) // Ensure Sport is loaded
        //        .Where(r => teamMemberIds.Contains(r.StudentId) && r.Sport.ReferenceSport == referenceSport)
        //        .ToListAsync();
        //    Console.WriteLine($"resssssssssssssssssssssssssssss : respository {res}");
        //    return res;
        //}
        public async Task<List<Reservation>> GetReservationsByReferenceSportForTeamAsync(List<Guid> teamMemberIds, int referenceSport)
        {
            Console.WriteLine($"Team member IDddddddddddddddddddddddddddds: {string.Join(", ", teamMemberIds)}");
            Console.WriteLine($"Reference sssssssssssssssssssssssssssssssport: {referenceSport}");

            var reservations = await _context.Reservations
                .Include(r => r.Sport)
                .Where(r => teamMemberIds.Contains(r.StudentId) && r.Sport.ReferenceSport == referenceSport)
                .ToListAsync();

            Console.WriteLine($"Rrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrreservations found after filtering: {reservations.Count}");

            return reservations;
        }
        




    }
}
