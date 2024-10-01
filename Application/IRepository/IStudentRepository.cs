using Application.IRepository.IGenericRepositorys;
using Domain.Entities;

namespace Application.IRepository
{
    public  interface IStudentRepository: IGenericRepository<Student>
    {
        Task<List<Student>> GetStudentsByIdsAsync(List<Guid> studentIds);
    }
}
