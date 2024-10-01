using Application.IServices;
using Application.IUnitOfWorks;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;




namespace Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ReservationService> _logger;


        public ReservationService(IUnitOfWork unitOfWork , ILogger<ReservationService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
           
        
           
        }



        public async Task<string> CanTeamOrUserReservationAsync(Guid studentId, List<Guid> studentIdList, Guid sportId)
        {
            var sport = await _unitOfWork.SportRepository.GetAsNoTracking(u => u.Id == sportId);
            if (sport == null)
            {
                return "Sport not found"; // Sport not found
            }

            var delayTimeMin = sport.Daysoff.GetValueOrDefault();
            var delayTime = DateTime.UtcNow.AddMinutes(-delayTimeMin);

            // Logging for debugging purposes
            _logger.LogInformation($"Checking existing reservation for student {studentId} after delay time {delayTime}");

            var existingReservation = await _unitOfWork.ReservationRepository
                .GetAsTracking(r => r.StudentId == studentId && r.ReservationDate >= delayTime);

            if (existingReservation != null)
            {
                // Conflict exists for this student
                return $"Conflict: Student {studentId} has an existing reservation.";
            }

            // Additional check for team members
            var reservations = await _unitOfWork.ReservationRepository
                .GetAllAsNoTracking(b => studentIdList.Contains(b.StudentId) && b.ReservationDate >= delayTime);

            if (reservations.Any())
            {
                // Conflict for team members
                return "Conflict: Some team members have existing reservations.";
            }

            // No conflicts found
            return null;
        }










        public async Task<string> ReservationAsync(Guid studentId, DateTime reservationDate, TimeSpan hourStart, TimeSpan hourEnd, List<Guid> studentIdList, Guid sportId)
        {
            string conflictMessage = await CanTeamOrUserReservationAsync(studentId, studentIdList, sportId);
            if (conflictMessage != null)
            {
                return conflictMessage;
            }

            var reservation = new Reservation
            {
                StudentId = studentId,
                SportId = sportId,
                ReservationDate = reservationDate,
                HourStart = hourStart,
                HourEnd = hourEnd,
                DateCreation = DateTime.UtcNow,
                StudentIdList = studentIdList ?? new List<Guid>()
            };

            reservation.Id = Guid.NewGuid();
            await _unitOfWork.ReservationRepository.CreateAsync(reservation);
            await _unitOfWork.CommitAsync();
            return null; // Reservation successful, no conflicts
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
