using Application.IRepository.IGenericRepositorys;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.IRepository
{
    public  interface IStudentRepository: IGenericRepository<Student>
    {
        Task<List<Student>> GetStudentsByIdsAsync(List<Guid> studentIds);
        Task<Student> GetByUserIdAsync(Guid userId);
        Task<Student> FindAsync(Expression<Func<Student, bool>> predicate);
    }
}
