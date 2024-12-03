using DataBaseStudentUIR.DB;
using DataBaseStudentUIR.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBaseStudentUIR.Repositorys
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }


        // Method to check email and password, and return CodeUIR, FirstName, LastName
        public async Task<(bool, string?, string?, string?)> GetStudentDetailsAsync(string email, string password)
        {
            var student = await _context.Students
                .Where(s => s.Email.ToLower() == email.ToLower() && s.Password == password)
                .Select(s => new { s.CodeUIR, s.FirstName, s.LastName })
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return (false, null, null, null);
            }

            return (true, student.CodeUIR, student.FirstName, student.LastName);
        }

        public async Task AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Students
                .AnyAsync(s => s.Email == email && s.Password == password);
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task DeleteAllStudentsAsync()
        {
            var students = await _context.Students.ToListAsync();
            _context.Students.RemoveRange(students);
            await _context.SaveChangesAsync();
        }
    }
}
