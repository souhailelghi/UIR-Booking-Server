using Application.IRepository;
using Domain.Entities;
using Infrastructure.Db;
using Infrastructure.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SportCategoryRepository : GenericRepository<SportCategory>, ISportCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public SportCategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


    }
}
