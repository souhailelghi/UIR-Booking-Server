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

        public ISportRepository SportRepository { get; }

        public IStudentRepository StudentRepository  { get; }

        public IReservationRepository ReservationRepository { get; }

        public UnitOfWork(ApplicationDbContext dbContext , ISportCategoryRepository sportCategoryRepository , ISportRepository sportRepository , IStudentRepository studentRepository , IReservationRepository reservationRepository)
        {
             _dbContext = dbContext;
            SportCategoryRepository = sportCategoryRepository;
            SportRepository = sportRepository;
            StudentRepository = studentRepository;
            ReservationRepository = reservationRepository;
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
