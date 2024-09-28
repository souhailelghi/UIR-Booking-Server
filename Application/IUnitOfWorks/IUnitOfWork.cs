using Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
