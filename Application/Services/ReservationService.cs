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

        public async Task<string> CountTimeAsync(string codeUIR, List<string> codeUIRList, Guid sportId)
        {
            // Fetch the sport details
            var sport = await FetchSportAsync(sportId);
            if (sport == null)
                return "Sport not found.";

            // Fetch the student details
            var student = await FetchStudentAsync(codeUIR);
            if (student == null)
                return "Student not found.";

            // Check for missing students in the provided list
            var missingStudents = await AreStudentsMissingAsync(codeUIRList);
            if (missingStudents.Any())
            {
                return $"Some students are missing: {string.Join(", ", missingStudents)}.";
            }

            // Calculate the allowed delay time for making reservations
            var delayTime = CalculateDelayTime(sport);

            // Check if the user has made a recent reservation
            if (await HasRecentReservationAsync(codeUIR, sport.ReferenceSport.Value, delayTime))
            {
                // Calculate the remaining wait time
                var recentReservation = await GetMostRecentReservationAsync(codeUIR, sport.ReferenceSport.Value, delayTime);
                var remainingTime = recentReservation.DateCreation.AddMinutes(sport.Daysoff.Value) - DateTime.UtcNow;

                if (remainingTime > TimeSpan.Zero)
                {
                    return $"You don't have permission to make a reservation. Please wait for {remainingTime:hh\\:mm\\:ss}.";
                }
            }

            // Check if any student in the list has a conflicting recent reservation
            if (await HasConflictingCodeUIRListAsync(codeUIRList, sportId, delayTime))
            {
                return "Conflicting reservation found for one or more students in the list.";
            }

            // All checks passed, user can make a reservation
            return "You can make a reservation.";
        }

        private async Task<Reservation> GetMostRecentReservationAsync(string codeUIR, int referenceSport, DateTime delayTime)
        {
            var reservations = await _unitOfWork.ReservationRepository
                .GetReservationsByReferenceSportAsync(codeUIR, referenceSport);

            return reservations
                .Where(r => r.DateCreation >= delayTime)
                .OrderByDescending(r => r.DateCreation)
                .FirstOrDefault();
        }

        //public async Task<string> CountTimeAsync(string codeUIR, List<string> codeUIRList, Guid sportId)
        //{
        //    var sport = await FetchSportAsync(sportId);
        //    if (sport == null) return "Sport not found.";

        //    var student = await FetchStudentAsync(codeUIR);
        //    if (student == null) return "Student not found.";

        //    var missingStudents = await AreStudentsMissingAsync(codeUIRList);
        //    if (missingStudents.Any())
        //    {
        //        return "Some students are missing.";
        //    }

        //    var delayTime = CalculateDelayTime(sport);

        //    // Check if the user has a recent reservation
        //    if (await HasRecentReservationAsync(codeUIR, sport.ReferenceSport.Value, delayTime))
        //    {
        //        // Calculate remaining time to wait
        //        var remainingTime = delayTime - DateTime.UtcNow;
        //        return $"You don't have permission to make a reservation. Please wait for {remainingTime:hh\\:mm\\:ss}.";
        //    }

        //    // Validate that no `CodeUIR` in the provided list has been recently used in another list
        //    if (await HasConflictingCodeUIRListAsync(codeUIRList, sportId, delayTime))
        //    {
        //        return "Conflicting reservation found for one or more students in the list.";
        //    }

        //    return "You can make a reservation.";
        //}



        public async Task<string> CanTeamOrUserBookAsync(string codeUIR, List<string> codeUIRList, Guid sportId)
        {
            if (codeUIRList != null && codeUIRList.Contains(codeUIR))
            {
                return "Le CodeUIR de l'étudiant ne peut pas être inclus dans la liste des CodeUIR.";
            }

            var sport = await FetchSportAsync(sportId);
            if (sport == null) return "Sport not found or ReferenceSport is null";

            var student = await FetchStudentAsync(codeUIR);
            if (student == null) return "Student's CodeUIR not found";

            var missingStudents = await AreStudentsMissingAsync(codeUIRList);

            if (missingStudents.Any())
            {
                return $"Certains élèves ne sont pas enregistrés dans la base de données. Les codes est incorrect.: {string.Join(", ", missingStudents)}";
            }

            var delayTime = CalculateDelayTime(sport);
            if (await HasRecentReservationAsync(codeUIR, sport.ReferenceSport.Value, delayTime))
            {
                return "Vous avez déjà une réservation en cours pour la période concernée.";
            }

            // Validate that no `CodeUIR` in the provided list has been recently used in another list
            if (await HasConflictingCodeUIRListAsync(codeUIRList, sportId, delayTime))
            {
                return "Certains CodeUIR de la liste sont déjà associés à une autre réservation durant la période concernée";
            }

            return "No conflicting reservations found";
        }

        public async Task<bool> CheckUserHaveAccessReservationAsync(string codeUIR, List<string> codeUIRList, Guid sportId)
        {
            //if (codeUIRList != null && codeUIRList.Contains(codeUIR))
            //{
            //    return "The student's CodeUIR cannot be part of the CodeUIR list.";
            //}

            var sport = await FetchSportAsync(sportId);
            if (sport == null) return false;

            var student = await FetchStudentAsync(codeUIR);
            if (student == null) return false;

            var missingStudents = await AreStudentsMissingAsync(codeUIRList);

            if (missingStudents.Any())
            {
                return false;
            }

            var delayTime = CalculateDelayTime(sport);
            if (await HasRecentReservationAsync(codeUIR, sport.ReferenceSport.Value, delayTime))
            {
                return false;
            }

            // Validate that no `CodeUIR` in the provided list has been recently used in another list
            if (await HasConflictingCodeUIRListAsync(codeUIRList, sportId, delayTime))
            {
                return false;
            }

            return true;
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



        private async Task<bool> HasConflictingCodeUIRListAsync(List<string> codeUIRList, Guid sportId, DateTime delayTime)
        {
            if (codeUIRList == null || !codeUIRList.Any())
                return false;

            var reservations = await _unitOfWork.ReservationRepository
                .GetReservationsForSportAsync(sportId, delayTime);

            // Check if any CodeUIR in the list exists in another reservation's CodeUIRList
            return reservations.Any(r =>
                r.CodeUIRList != null &&
                r.CodeUIRList.Intersect(codeUIRList).Any());
        }


        //helper methods 


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
