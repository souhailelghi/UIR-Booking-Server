using Application.IRepository.IGenericRepositorys;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.IRepository
{
    public interface ISportRepository : IGenericRepository<Sport>
    {
        Task<Sport> GetAsync(Expression<Func<Sport, bool>> filter);
    }
}
