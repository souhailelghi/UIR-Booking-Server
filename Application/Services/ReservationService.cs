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

        public async Task<bool> CanBookReservationAsync(Guid studentId, Guid sportId, DateTime reservationDate, TimeSpan hourStart, TimeSpan hourEnd)
        {
            // Fetch the sport's Daysoff value
            var sport = await _unitOfWork.SportRepository.GetAsync(s => s.Id == sportId);
            if (sport == null)
            {
                throw new Exception("Sport not found.");
            }

            // Retrieve reservations made by the student for the same sport
            var studentReservations = await _unitOfWork.ReservationRepository
                .GetReservationsForDateAsync(reservationDate, studentId, new List<Guid> { });

            // Check if the student has a reservation for the same sport
            var lastReservation = studentReservations
                .Where(res => res.SportId == sportId)
                .OrderByDescending(res => res.ReservationDate)
                .FirstOrDefault();

            if (lastReservation != null)
            {
                var daysoffEnd = lastReservation.ReservationDate.AddMinutes(sport.Daysoff ?? 0);

                // Enforce Daysoff restriction if the student tries to book the same sport before the waiting period ends
                if (reservationDate < daysoffEnd)
                {
                    return false; // The student must wait before booking the same sport again.
                }
            }

            // Check for conflicting reservations for the same sport at the same hour by other students
            var reservations = await _unitOfWork.ReservationRepository
                .GetReservationsForDateAsync(reservationDate, Guid.Empty, new List<Guid> { studentId });

            var conflictingReservations = reservations
                .Where(res => res.SportId == sportId && res.HourStart < hourEnd && res.HourEnd > hourStart)
                .ToList();

            if (conflictingReservations.Any())
            {
                return false; // There are conflicting reservations for the same sport at the same time.
            }

            return true;
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
