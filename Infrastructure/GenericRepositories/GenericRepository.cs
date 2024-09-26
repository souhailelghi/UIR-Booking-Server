using Application.IRepository.IGenericRepositorys;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GenericRepositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        internal DbSet<T> dbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            this.dbSet = _dbContext.Set<T>();
        }
        public async Task<List<T>> GetAllAsNoTracking(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet.AsNoTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }
        public async Task<T> GetAsNoTracking(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet.AsNoTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }
        public async Task<List<T>> GetAllAsTracking(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet.AsTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }
        public async Task<T> GetAsTracking(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet.AsTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }
        public async Task CreateRangeAsync(ICollection<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }
        public async Task CreateAsync(T entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"The error in generic {ex.Message} {ex.StackTrace}", ex);
            }
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
        }
        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
