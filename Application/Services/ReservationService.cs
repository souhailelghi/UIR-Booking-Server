using Application.IServices;
using Application.IUnitOfWorks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;




namespace Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;



        public ReservationService(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
         
           
        
           
        }


        public async Task<List<Reservation>> GetConflictingReservationsAsync(Guid studentId, List<Guid> teamMembersIds, DateTime reservationDate, TimeSpan hourStart, TimeSpan hourEnd)
        {
            // Retrieve existing reservations for the student and team members on the same day
            var allReservations = await _unitOfWork.ReservationRepository
                .GetReservationsForDateAsync(reservationDate, studentId, teamMembersIds);

            // Check for time conflicts
            var conflictingReservations = allReservations
                .Where(res => res.HourStart < hourEnd && res.HourEnd > hourStart)
                .ToList();

            return conflictingReservations;
        }




        public async Task<Reservation> AddReservationAsync(Reservation reservation)
        {
            // Add reservation
            reservation.Id = Guid.NewGuid();
            await _unitOfWork.ReservationRepository.CreateAsync(reservation);

            // Commit the transaction
            await _unitOfWork.CommitAsync();

            return reservation;
        }


        public async Task DeleteAllReservationsAsync()
        {
            var reservations = await _unitOfWork.ReservationRepository.GetAllAsTracking();
            if (reservations == null || reservations.Count == 0)
            {
                throw new ArgumentException("No reservations found to delete.");
            }

            await _unitOfWork.ReservationRepository.RemoveAllAsync();
            await _unitOfWork.CommitAsync();
        }

    }
}
