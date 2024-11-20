using Application.IRepository.IGenericRepositorys;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.IRepository
{
    public interface IEmailRepository : IGenericRepository<Email>
    {
        Task<Email> GetAsync(Expression<Func<Email, bool>> filter);
    }
}
