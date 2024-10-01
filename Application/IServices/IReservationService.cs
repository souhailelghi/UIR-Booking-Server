using Domain.Entities;

namespace Application.IServices
{
    public interface IReservationService
    {


        Task<Reservation> AddReservationAsync(Reservation reservation);

        Task<List<Reservation>> GetConflictingReservationsAsync(Guid studentId, List<Guid> teamMembersIds, DateTime reservationDate, TimeSpan hourStart, TimeSpan hourEnd);



        Task DeleteAllReservationsAsync();


    }
}
