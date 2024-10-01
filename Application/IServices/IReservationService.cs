using Domain.Entities;

namespace Application.IServices
{
    public interface IReservationService
    {
    

        Task<string> CanTeamOrUserReservationAsync(Guid studentId, List<Guid> studentIdList, Guid sportId);
        Task<string> ReservationAsync(Guid studentId, DateTime reservationDate, TimeSpan hourStart, TimeSpan hourEnd, List<Guid> studentIdList, Guid sportId);

        

        Task DeleteAllReservationsAsync();


    }
}
