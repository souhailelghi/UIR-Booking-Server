using Application.IRepository;
using Application.IUnitOfWorks;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks
{
    public  class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

       

        public ISportCategoryRepository SportCategoryRepository { get; }

        public UnitOfWork(ApplicationDbContext dbContext , ISportCategoryRepository sportRepository)
        {
             _dbContext = dbContext;
            SportCategoryRepository = sportRepository;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }



        public async Task CommitAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException != null ? $" Inner exception: {ex.InnerException.Message}" : string.Empty;
                throw new ApplicationException($"An error occurred while saving changes to the database.{innerException}", ex);

            }
        }

        public void Rollback()
        {
            _dbContext.SaveChanges();
        }

        public async Task RollbackAsync()
        {
            await _dbContext.SaveChangesAsync();
        }










    }
}
