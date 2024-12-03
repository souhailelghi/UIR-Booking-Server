using DataBaseStudentUIR.Models;

namespace DataBaseStudentUIR.Repositorys
{
    public interface IStudentRepository
    {
        Task<bool> CheckEmailAndPasswordAsync(string email, string password);
        Task<(bool, string?, string?, string?)> GetStudentDetailsAsync(string email, string password);
        Task AddStudentAsync(Student student);
        Task<List<Student>> GetAllStudentsAsync();
        Task DeleteAllStudentsAsync();
    }
}
