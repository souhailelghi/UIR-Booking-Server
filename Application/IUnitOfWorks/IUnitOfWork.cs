using Application.IRepository;

namespace Application.IUnitOfWorks
{
    public interface IUnitOfWork
    {
        ISportCategoryRepository SportCategoryRepository { get; }
        ISportRepository SportRepository { get; }
        IStudentRepository StudentRepository { get; }

        IReservationRepository ReservationRepository { get; }

        void Commit();
        Task CommitAsync();
        void Rollback();
        Task RollbackAsync();
    }
}
