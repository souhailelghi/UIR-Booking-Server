using Application.IRepository;
using Domain.Entities;
using Infrastructure.Db;
using Infrastructure.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        // Implementing the GetStudentsByIdsAsync method
        public async Task<List<Student>> GetStudentsByIdsAsync(List<Guid> studentIds)
        {
            return await _context.Students
                .Where(student => studentIds.Contains(student.Id)) // Assuming Id is the primary key in Student
                .ToListAsync();
        }
    }
}
