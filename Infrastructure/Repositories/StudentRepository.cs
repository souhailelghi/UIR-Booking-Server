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
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Student> GetAsync(Expression<Func<Student, bool>> filter)
        {
            return await dbSet.FirstOrDefaultAsync(filter);
        }


        // Implementing the GetStudentsByIdsAsync method
        public async Task<List<Student>> GetStudentsByIdsAsync(List<Guid> studentIds)
        {
            return await _context.Students
                .Where(student => studentIds.Contains(student.Id)) // Assuming Id is the primary key in Student
                .ToListAsync();
        }

        public async Task<Student> GetByUserIdAsync(Guid userId)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public async Task<Student> FindAsync(Expression<Func<Student, bool>> predicate)
        {
            return await _context.Students.FirstOrDefaultAsync(predicate);
        }

        //------------
        public async Task<List<Student>> GetStudentsByCodeUIRsAsync(List<string> codeUIRList)
        {
            return await _context.Students
               .Where(student => codeUIRList.Contains(student.CodeUIR)) // Assuming Id is the primary key in Student
               .ToListAsync();
        }
    }
}
