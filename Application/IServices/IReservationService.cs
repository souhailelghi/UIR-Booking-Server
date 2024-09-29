using Domain.Entities;

namespace Application.IServices
{
    public interface IReservationService
    {
        Task<Reservation> AddReservationAsync(Reservation reservation);

        Task<bool> CanTeamOrUserReservationAsync(Guid studentId, List<Guid> studentIdList, Guid sportId);
        Task<bool> ReservationAsync(Guid studentId, DateTime reservationDate, TimeSpan hourStart, TimeSpan hourEnd, List<Guid> studentIdList, Guid sportId);

        Task<Reservation> GetBookingByIdAsync(Guid id);
    }
}
