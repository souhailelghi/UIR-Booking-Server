using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IStudentService 
    {
        Task<Student> AddStudentAsync(Student student);

        Task<Student> GetStudentByUserIdAsync(Guid userId);

        Task<bool> CheckCodeUirExistsAsync(string codeUir);
        Task<Student> GetStudentByIdAsync(Guid id);
    }
}
