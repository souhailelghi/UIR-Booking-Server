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





        //add reservation : 
        public async Task<string> CanTeamOrUserBookAsync(string codeUIR, List<string> codeUIRList, Guid sportId)
        {
            if (codeUIRList != null && codeUIRList.Contains(codeUIR))
            {
                return "The student's CodeUIR cannot be part of the CodeUIR list.";
            }

            var sport = await FetchSportAsync(sportId);
            if (sport == null) return "Sport not found or ReferenceSport is null";

            var student = await FetchStudentAsync(codeUIR);
            if (student == null) return "Student's CodeUIR not found";

            var missingStudents = await AreStudentsMissingAsync(codeUIRList);

            if (missingStudents.Any())
            {
                return $"Some students don't exist in the database: {string.Join(", ", missingStudents)}";
            }

            var delayTime = CalculateDelayTime(sport);
            if (await HasRecentReservationAsync(codeUIR, sport.ReferenceSport.Value, delayTime))
            {
                return "You are in a reservation within the delay time.";
            }

            return "No conflicting reservations found";
        }



        //Helper Methods
        private async Task<Sport> FetchSportAsync(Guid sportId)
        {
            return await _unitOfWork.SportRepository.GetAsync(s => s.Id == sportId);
        }
        private async Task<Student> FetchStudentAsync(string codeUIR)
        {
            return await _unitOfWork.StudentRepository.GetAsync(s => s.CodeUIR == codeUIR);
        }

        private DateTime CalculateDelayTime(Sport sport)
        {
            var delayTimeMinutes = sport.Daysoff ?? 0;
            return DateTime.UtcNow.AddMinutes(-delayTimeMinutes);
        }

        private async Task<bool> HasRecentReservationAsync(string codeUIR, int referenceSport, DateTime delayTime)
        {
            // Check if the student has recent reservations based on DateCreation
            var existingReservations = await _unitOfWork.ReservationRepository
                .GetReservationsByReferenceSportAsync(codeUIR, referenceSport);

            bool hasRecentReservation = existingReservations.Any(r => r.DateCreation >= delayTime);

            if (hasRecentReservation)
            {
                return true;
            }

            // Check if the CodeUIR exists in any CodeUIRList in recent reservations
            var reservationsWithCodeUIR = await _unitOfWork.ReservationRepository
                .GetReservationsByReferenceSportWithCodeUIRAsync(referenceSport, delayTime);

            return reservationsWithCodeUIR.Any(r => r.CodeUIRList != null && r.CodeUIRList.Contains(codeUIR));
        }


        private async Task<List<string>> AreStudentsMissingAsync(List<string> codeUIRList)
        {
            var studentsExist = await _unitOfWork.StudentRepository.GetStudentsByCodeUIRsAsync(codeUIRList);
            var missingStudents = codeUIRList.Except(studentsExist.Select(s => s.CodeUIR)).ToList();
            return missingStudents;
        }








        public async Task<string> BookAsync(string codeUIR, Guid sportCategoryId, DateTime reservationDate, DayOfWeekEnum dayBooking, TimeSpan hourStart,
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
                CodeUIR = student.CodeUIR,
                SportId = sportId,
                SportCategoryId = sportCategoryId,
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


        //get reservation : -----------------------------
        public async Task<List<Reservation>> GetReservationsByCategoryAndStudentIdAsync(Guid sportCategoryId, string codeUIR)
        {
            return await _unitOfWork.ReservationRepository.GetReservationsByCategoryAndStudentIdAsync(sportCategoryId, codeUIR);
        }

        public async Task<List<Reservation>> GetReservationsBySportCategoryIdAsync(Guid sportCategoryId)
        {
            var reservations = await _unitOfWork.ReservationRepository.GetReservationsBysportCategoryIdAsync(sportCategoryId);
            return reservations;
        }

        public async Task<List<Reservation>> GetReservationsByStudentIdAsync(string codeUIR)
        {
            var reservations = await _unitOfWork.ReservationRepository.GetReservationsByStudentIdAsync(codeUIR);
            return reservations;
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
