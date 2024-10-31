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
    public class TimeRangeRepository :  GenericRepository<TimeRange>, ITimeRangeRepository
    {
         private readonly ApplicationDbContext _context;
    public TimeRangeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
}
}
