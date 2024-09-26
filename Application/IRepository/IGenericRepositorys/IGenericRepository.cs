using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository.IGenericRepositorys
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsNoTracking(Expression<Func<T, bool>>? filter = null);
        Task<T> GetAsNoTracking(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAllAsTracking(Expression<Func<T, bool>>? filter = null);
        Task<T> GetAsTracking(Expression<Func<T, bool>> filter);
        Task CreateRangeAsync(ICollection<T> entities);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
    }
}
