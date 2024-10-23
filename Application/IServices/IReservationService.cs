using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.IServices
{
    public interface IReservationService
    {


    


         Task DeleteAllReservationsAsync();
        Task<bool> BookAsync(string codeUIR, DateTime reservationDate, DayOfWeekEnum dayBooking, TimeSpan hourStart, TimeSpan hourEnd, List<string> codeUIRList, Guid sportId);

    }
}
