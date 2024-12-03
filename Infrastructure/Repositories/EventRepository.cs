using Application.IRepository;
using Domain.Entities;
using Infrastructure.Db;
using Infrastructure.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private readonly ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task AddAsync(Event entity)
        {
            await _context.Set<Event>().AddAsync(entity); // Add to DbContext
        }
    }
}
