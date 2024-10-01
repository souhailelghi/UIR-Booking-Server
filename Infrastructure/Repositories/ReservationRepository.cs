using Application.IRepository;
using Domain.Entities;
using Infrastructure.Db;
using Infrastructure.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation> , IReservationRepository
    {

        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;   
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
    }
}
