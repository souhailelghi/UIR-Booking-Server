using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IReservationService
    {
        Task<List<Reservation>> GetReservationsListAsync();
        Task<Reservation> GetReservationByIdAsync(Guid id);
        Task DeleteAllReservationsAsync();

        // Method to check if the user or team can book
        Task<string> CanTeamOrUserBookAsync(string codeUIR, List<string> codeUIRList, Guid sportId);
        Task<bool> CheckUserHaveAccessReservationAsync(string codeUIR, List<string> codeUIRList, Guid sportId);

        // Updated BookAsync method that returns a tuple
        Task<string> BookAsync(
            string codeUIR,
            Guid sportCategoryId,
            DateTime reservationDate,
            DayOfWeekEnum dayBooking,
            TimeSpan hourStart,
            TimeSpan hourEnd,
            List<string> codeUIRList,
            Guid sportId);

        Task<List<Reservation>> GetReservationsByStudentIdAsync(string codeUIR);
        Task<List<Reservation>> GetReservationsBySportCategoryIdAsync(Guid sportCategoryId);
        Task<List<Reservation>> GetReservationsByCategoryAndStudentIdAsync(Guid sportCategoryId, string codeUIR);
        //Task<string> CheckUserHaveAccessReservationAsync(string codeUIR,  Guid sportId);
        Task<string> CountTimeAsync(string codeUIR, List<string> codeUIRList, Guid sportId);




    }
}

