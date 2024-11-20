using Domain.Entities;
using Infrastructure.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EmailRepository : GenericRepositories<Email>, IEmailRepository
    {
        private readonly ApplicationDbContext _context;
        public EmailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
