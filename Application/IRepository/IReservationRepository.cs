using Application.IRepository.IGenericRepositorys;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        Task<List<Reservation>> GetReservationsForDateAsync(DateTime reservationDate, Guid studentId, List<Guid> teamMembersIds);
        Task RemoveAllAsync();
    }
}
