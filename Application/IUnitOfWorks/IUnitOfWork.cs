using Application.IRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.IUnitOfWorks
{
    public interface IUnitOfWork
    {
        ISportCategoryRepository SportCategoryRepository { get; }
        ISportRepository SportRepository { get; }
        IStudentRepository StudentRepository { get; }

        IReservationRepository ReservationRepository { get; }

        IPlanningRepository PlanningRepository { get; }
        ITimeRangeRepository TimeRangeRepository { get; }


        void Commit();
        Task CommitAsync();
        void Rollback();
        Task RollbackAsync();

        DbSet<Planning> Plannings { get; }
        Task<int> CompleteAsync();
    }
}
