using Application.IRepository;
using Application.IUnitOfWorks;
using Domain.Entities;
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

        public IEventRepository EventRepository { get; }

        public IReservationRepository ReservationRepository { get; }

        public IPlanningRepository PlanningRepository { get; }
        public ITimeRangeRepository TimeRangeRepository { get; }

        public UnitOfWork(ApplicationDbContext dbContext , ISportCategoryRepository sportCategoryRepository , ISportRepository sportRepository , IStudentRepository studentRepository ,IEventRepository eventRepository ,IReservationRepository reservationRepository , IPlanningRepository planningRepository , ITimeRangeRepository timeRangeRepository)
        {
             _dbContext = dbContext;
            SportCategoryRepository = sportCategoryRepository;
            SportRepository = sportRepository;
            StudentRepository = studentRepository;
            EventRepository = eventRepository;
            ReservationRepository = reservationRepository;
            PlanningRepository = planningRepository;
            TimeRangeRepository = timeRangeRepository;
        }


        public DbSet<Planning> Plannings => _dbContext.Plannings;

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
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
