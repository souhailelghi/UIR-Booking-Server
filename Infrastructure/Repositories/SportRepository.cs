using Application.IRepository;
using Domain.Entities;
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
    public class SportRepository : GenericRepository<Sport>, ISportRepository
    {
        private readonly ApplicationDbContext _context;
        public SportRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Sport> GetAsync(Expression<Func<Sport, bool>> filter)
        {
            return await dbSet.FirstOrDefaultAsync(filter);
        }
        // Implement GetByIdAsync
        public async Task<Sport> GetByIdAsync(Guid id)
        {
            return await dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

    }
}
