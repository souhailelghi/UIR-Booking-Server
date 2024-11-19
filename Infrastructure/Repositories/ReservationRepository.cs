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
        //--------
        public async Task<Reservation> GetAsync(Expression<Func<Reservation, bool>> filter)
        {
            return await dbSet.FirstOrDefaultAsync(filter);
        }

        // new command : 
        // Fetch reservations by ReferenceSport for a single student
        public async Task<List<Reservation>> GetReservationsByReferenceSportAsync(string codeUIR, int referenceSport)
        {
            return await _context.Reservations
                .Include(r => r.Sport) // Ensure Sport is loaded
                .Where(r => r.Sport.ReferenceSport == referenceSport && r.CodeUIR == codeUIR)
                .ToListAsync();
        }





        //-----

        public async Task<List<Reservation>> GetReservationsBysportCategoryIdAsync(Guid sportCategoryId)
        {
            return await _context.Reservations
               .Where(r => r.SportCategoryId == sportCategoryId)
               .ToListAsync();
        }

        public async Task<List<Reservation>> GetReservationsByStudentIdAsync(string codeUIR)
        {
            return await _context.Reservations
                .Where(r => r.CodeUIR == codeUIR)
                .ToListAsync();
        }


   
        public async Task<List<Reservation>> GetReservationsBySportIdAsync(Guid sportId)
        {
           
            return await _context.Reservations
                .Where(r => r.SportId == sportId)
                .ToListAsync();
        }

        public async Task<List<Reservation>> GetReservationsForDateAsync( string codeUIR, List<string> teamMembersIds)
        {
            return await _context.Reservations
                .Where(r => (r.CodeUIR == codeUIR || teamMembersIds.Contains(r.CodeUIR)))
                .ToListAsync();
        }

        public async Task<List<Reservation>> GetReservationsDateAsync(string codeUIR, List<string> teamMembersIds)
        {
           var res = await _context.Reservations
                .Where(r => teamMembersIds.Contains(r.CodeUIR)) // Filter by team members
                .ToListAsync();
            return res;
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


     
     
        public async Task<List<Reservation>> GetReservationsByCategoryAndStudentIdAsync(Guid sportCategoryId, string codeUIR)
        {
            return await _context.Reservations
                .Where(r => r.SportCategoryId == sportCategoryId && r.CodeUIR == codeUIR)
                .ToListAsync();
        }

        public async Task<List<Reservation>> GetReservationsByCodeUIRsAsync(List<string> codeUIRs)
        {
            return await _context.Reservations
                .Where(r => r.CodeUIRList.Any(c => codeUIRs.Contains(c)))
                .ToListAsync();
        }

        //new 
        public async Task<IEnumerable<Reservation>> GetReservationsByReferenceSportWithCodeUIRAsync(int referenceSport, DateTime delayTime)
        {
            return await _context.Reservations
                .Where(r => r.Sport.ReferenceSport == referenceSport && r.DateCreation >= delayTime)
                .ToListAsync();
        }

        //new 
        public async Task<IEnumerable<Reservation>> GetReservationsForSportAsync(Guid sportId, DateTime delayTime)
        {
            return await _context.Reservations
                .Where(r => r.SportId == sportId && r.DateCreation >= delayTime)
                .ToListAsync();
        }



    }
}
