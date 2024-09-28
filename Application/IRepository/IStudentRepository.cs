using Application.IRepository.IGenericRepositorys;
using Domain.Entities;

namespace Application.IRepository
{
    public  interface IStudentRepository: IGenericRepository<Student>
    {
    }
}
