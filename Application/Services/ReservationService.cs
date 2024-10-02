using Application.IServices;
using Application.IUnitOfWorks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReservationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CanTeamOrUserBookAsync(Guid studentId, List<Guid> studentIdList, Guid sportId)
        {
            var sport = await _unitOfWork.SportRepository.GetAsync(s => s.Id == sportId);
            if (sport == null)
            {
                return false; // Sport not found
            }

            var delayTimeMinutes = sport.Daysoff ?? 0; // Assuming Daysoff is the delay time
            var delayTime = DateTime.UtcNow.AddMinutes(-delayTimeMinutes);

            // Check if the student has a recent reservation
            var existingReservations = await _unitOfWork.ReservationRepository
                .GetReservationsForDateAsync(studentId, new List<Guid> { });

            var existingReservation = existingReservations
                .Where(r => r.DateCreation >= delayTime)
                .FirstOrDefault();

            if (existingReservation != null)
            {
                return false; // Student has a reservation within the delay time
            }

            // Check if all team members exist
            var studentsExist = await _unitOfWork.StudentRepository
                .GetStudentsByIdsAsync(studentIdList); // Now this method exists

            if (studentsExist.Count() != studentIdList.Count)
            {
                return false; // Some students from the list don't exist in the database
            }

            // Check if any team member has a recent reservation
            var teamReservations = await _unitOfWork.ReservationRepository
                .GetReservationsForDateAsync(Guid.Empty, studentIdList);

            var teamReservationExists = teamReservations
                .Where(r => r.DateCreation >= delayTime)
                .Any();

            return !teamReservationExists; // Return true if no team members have reservations within the delay time
        }

        public async Task<bool> BookAsync(Guid studentId, DateTime reservationDate, TimeSpan hourStart, TimeSpan hourEnd,  List<Guid> studentIdList, Guid sportId)
        {
            // Check if the student or team can book
            if (!await CanTeamOrUserBookAsync(studentId, studentIdList, sportId))
            {
                return false;
            }

            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                SportId = sportId,
                ReservationDate = reservationDate,
                HourStart = hourStart,
                HourEnd = hourEnd,
                OnlyDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DateCreation = DateTime.UtcNow,
                StudentIdList = studentIdList ?? new List<Guid>()
            };

            await _unitOfWork.ReservationRepository.CreateAsync(reservation);
            await _unitOfWork.CommitAsync(); // Commit the transaction

            return true;
        }

        public Task CanBookReservationAsync(Guid StudentId, Guid SportId, DateTime ReservationDate, TimeSpan HourStart, TimeSpan HourEnd)
        {
            throw new NotImplementedException();
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
