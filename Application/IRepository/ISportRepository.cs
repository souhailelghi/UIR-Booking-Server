using Application.IRepository.IGenericRepositorys;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.IRepository
{
    public interface ISportRepository : IGenericRepository<Sport>
    {
        Task<Sport> GetByIdAsync(Guid id);
        Task<Sport> GetAsync(Expression<Func<Sport, bool>> filter);

    }
}
