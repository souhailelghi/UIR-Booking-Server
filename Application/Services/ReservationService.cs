using Application.IServices;
using Application.IUnitOfWorks;
using Domain.Entities;
using Domain.Enums;
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

        

        public async Task<string> CanTeamOrUserBookAsync(string codeUIR, List<string> codeUIRList, Guid sportId)
        {
            // Fetch the sport entity
            var sport = await _unitOfWork.SportRepository.GetAsync(s => s.Id == sportId);
            if (sport == null || !sport.ReferenceSport.HasValue)
            {
                return "Sport not found or ReferenceSport is null"; // Sport not found or ReferenceSport is null
            }

            // Get the student's CodeUIR
            var student = await _unitOfWork.StudentRepository.GetAsync(s => s.CodeUIR == codeUIR);
            if (student == null)
            {
                return "Student's CodeUIR not found";
            }

            var CodeUIRStudent = student.CodeUIR;

            // Get ReferenceSport from the sport entity
            var referenceCodeSport = sport.ReferenceSport.Value; // Convert nullable int? to int

            // Delay time based on the number of days off (if any)
            var delayTimeMinutes = sport.Daysoff ?? 0; // Assuming Daysoff is the delay time in minutes
            var delayTime = DateTime.UtcNow.AddMinutes(-delayTimeMinutes);

            // Check if the student has a recent reservation based on ReferenceSport
            var existingReservations = await _unitOfWork.ReservationRepository
                .GetReservationsByReferenceSportAsync(student.Id, referenceCodeSport);

            var existingReservation = existingReservations
                .Where(r => r.DateCreation >= delayTime)
                .FirstOrDefault();

            if (existingReservation != null)
            {
                return "Student has a reservation within the delay time"; // Student has a reservation within the delay time
            }

            // Check if all team members exist
            var studentsExist = await _unitOfWork.StudentRepository
                .GetStudentsByCodeUIRsAsync(codeUIRList);
            // Find any missing students
            var missingStudents = codeUIRList.Except(studentsExist.Select(s => s.CodeUIR)).ToList();

            if (missingStudents.Any())
            {
                return $"Some students don't exist in the database: {string.Join(", ", missingStudents)}"; // Specific feedback for missing students
            }

            

            // Check if any team member has a recent reservation based on ReferenceSport
            var teamReservations = await _unitOfWork.ReservationRepository
                .GetReservationsByReferenceSportForTeamAsync(studentsExist.Select(s => s.Id).ToList(), referenceCodeSport);

            // Get the CodeUIR of team members with reservations within the delay time
            var teamMembersWithReservations = teamReservations
                .Where(r => r.DateCreation >= delayTime)
                .Select(r => studentsExist.First(s => s.Id == r.StudentId).CodeUIR)
                .ToList();

            if (teamMembersWithReservations.Any())
            {
                return $"Team members with reservations within the delay time: {string.Join(", ", teamMembersWithReservations)}";
            }

            return "No conflicting reservations found";
        }

      

        public async Task<string> BookAsync(
           string codeUIR, DateTime reservationDate, DayOfWeekEnum dayBooking, TimeSpan hourStart,
           TimeSpan hourEnd, List<string> codeUIRList, Guid sportId)
        {
            // Check if the student or team can book
            string canBookResult = await CanTeamOrUserBookAsync(codeUIR, codeUIRList, sportId);
            if (!canBookResult.Contains("No conflicting reservations found"))
            {
                return "Booking not allowed. Either the student or team has a recent reservation or does not meet booking criteria.";
            }

            // Fetch the student's entity using CodeUIR
            var student = await _unitOfWork.StudentRepository.GetAsync(s => s.CodeUIR == codeUIR);
            if (student == null)
            {
                return "Student not found with the provided CodeUIR.";
            }

            // Create the reservation
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                StudentId = student.Id,
                SportId = sportId,
                ReservationDate = reservationDate,
                DayBooking = dayBooking,
                HourStart = hourStart,
                HourEnd = hourEnd,
                OnlyDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DateCreation = DateTime.UtcNow,
                CodeUIRList = codeUIRList ?? new List<string>()
            };

            await _unitOfWork.ReservationRepository.CreateAsync(reservation);
            await _unitOfWork.CommitAsync();

            return "Reservation successfully created.";
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

        public async Task<Reservation> GetReservationByIdAsync(Guid id)
        {
            Reservation reservation = await _unitOfWork.ReservationRepository.GetAsNoTracking(u => u.Id == id);

            return reservation;
        }

        public async Task<List<Reservation>> GetReservationsListAsync()
        {
            List<Reservation> reservationsList = await _unitOfWork.ReservationRepository.GetAllAsNoTracking();
            return reservationsList;
        }


    }
}
