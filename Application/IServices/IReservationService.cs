using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.IServices
{
    public interface IReservationService
    {



        Task<List<Reservation>> GetReservationsListAsync();
        Task<Reservation> GetReservationByIdAsync(Guid id);


         Task DeleteAllReservationsAsync();
        Task<bool> BookAsync(string codeUIR, DateTime reservationDate, DayOfWeekEnum dayBooking, TimeSpan hourStart, TimeSpan hourEnd, List<string> codeUIRList, Guid sportId);

    }
}
