using Domain.Entities;
using MediatR;

namespace Application.IServices
{
    public interface IReservationService
    {


    
       Task CanBookReservationAsync(Guid StudentId, Guid SportId, DateTime ReservationDate, TimeSpan   HourStart, TimeSpan HourEnd);

         Task DeleteAllReservationsAsync();
        Task<bool> BookAsync(Guid studentId, DateTime reservationDate, TimeSpan hourStart, TimeSpan hourEnd, List<Guid> studentIdList, Guid sportId);

    }
}
