using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.IServices
{
    public interface IReservationService
    {


    


         Task DeleteAllReservationsAsync();
        Task<bool> BookAsync(Guid studentId, DateTime reservationDate, DayOfWeekEnum dayBooking, TimeSpan hourStart, TimeSpan hourEnd,  List<Guid> studentIdList, Guid sportId);

    }
}
