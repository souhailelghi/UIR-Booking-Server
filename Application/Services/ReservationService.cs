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

        #region Constructors 
        public ReservationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion


        //Public Methods : 

        #region Public Methods : 

        #region count time 
        public async Task<string> CountTimeAsync(string codeUIR, List<string> codeUIRList, int referenceSport)
        {
            // Fetch the sport details
            var sport = await FetchSportByReferenceAsync(referenceSport);
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
            if (!sport.Daysoff.HasValue)
                return "The sport's day off value is not defined.";

            var delayTime = DateTime.UtcNow.AddDays(-sport.Daysoff.Value);
            Console.WriteLine($"***************************** sport.Daysoff.Value  : {sport.Daysoff.Value} **************");
            Console.WriteLine($"***************************** delayTime : {delayTime} ***************");

            if (!await HasConflictingCodeUIRsAsync(codeUIRList, referenceSport, delayTime))
            {
                if (!sport.ReferenceSport.HasValue)
                {
                    return "Sport's reference value is missing.";
                }


                var remainingTime = await GetDelayTimeAsync(codeUIRList, sport.Id, delayTime);
                var totalHours = Math.Floor(remainingTime.TotalHours); // Full hours
                var minutes = remainingTime.Minutes;
                var seconds = remainingTime.Seconds;
                if (remainingTime > TimeSpan.Zero)
                {
                    //return $"Vous n'avez pas la permission de faire une réservation. Veuillez attendre {remainingTime:hh\\:mm\\:ss}.";
                    return $"Vous n'avez pas la permission de faire une réservation. Veuillez attendre {totalHours:00}:{minutes:00}:{seconds:00}.";
                }
            }

            if (await HasRecentReservationAsync(codeUIR, referenceSport, delayTime))
            {
                var recentReservation = await GetMostRecentReservationAsync(codeUIR, referenceSport, delayTime);
                Console.WriteLine($"----------------------------DateCreation: {recentReservation.DateCreation}, Current UTC: {DateTime.UtcNow}");

                Console.WriteLine($"-----------with-add--------DateCreation: {recentReservation.DateCreation.AddDays(sport.Daysoff.Value)}");

                if (recentReservation != null)
                {
                    //var remainingTime = recentReservation.DateCreation.AddDays(sport.Daysoff.Value) - DateTime.UtcNow;
                    var expectedTime = recentReservation.DateCreation.AddDays(sport.Daysoff.Value);
                    var remainingTime = expectedTime - DateTime.UtcNow;
                    // Format the remaining time in total hours
                    var totalHours = Math.Floor(remainingTime.TotalHours); // Full hours
                    var minutes = remainingTime.Minutes;
                    var seconds = remainingTime.Seconds;
                    Console.WriteLine($"**********remainingTime :  {remainingTime} :::::::: {expectedTime} ");
                    if (remainingTime > TimeSpan.Zero)
                    {
                        //return $"Vous n'avez pas la permission de faire une réservation. Veuillez attendre {remainingTime:hh\\:mm\\:ss}.";
                        return $"Vous n'avez pas la permission de faire une réservation. Veuillez attendre {totalHours:00}:{minutes:00}:{seconds:00}.";
                    }
                }
            }

            return "You can make a reservation.";
        }



        #endregion


        #region CanTeamOrUserBookAsync : 
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
            var conflictingCodeUIRs = await GetConflictingCodeUIRsAsync(codeUIRList, sportId, delayTime);
            if (conflictingCodeUIRs.Any())
            {
                //return $"Certains CodeUIR de la liste sont déjà associés à une autre réservation durant la période concernée: {string.Join(", ", conflictingCodeUIRs
                //     // Check if only onecode uir list
                if (conflictingCodeUIRs.Count == 1)
                {
                    return $"Cette code étudiant  sont déjà associés à une autre réservation.: {string.Join(", ", conflictingCodeUIRs)}";
                }
                // If more than one code uir list
                else
                {
                    return $"Les codes étudiants suivants sont déjà associés à une autre réservation.: {string.Join(", ", conflictingCodeUIRs)}";
                }
            }

            return "No conflicting reservations found";
        }

        #endregion


        #region CheckUserHaveAccessReservationAsync
        public async Task<bool> CheckUserHaveAccessReservationAsync(string codeUIR, List<string> codeUIRList, Guid sportId)
        {


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
        #endregion

        #region BookAsync
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
        #endregion

        #region GetReservationsByCategoryAndStudentIdAsync 
        public async Task<List<Reservation>> GetReservationsByCategoryAndStudentIdAsync(Guid sportCategoryId, string codeUIR)
        {
            return await _unitOfWork.ReservationRepository.GetReservationsByCategoryAndStudentIdAsync(sportCategoryId, codeUIR);
        }
        #endregion

        #region GetReservationsBySportCategoryIdAsync
        public async Task<List<Reservation>> GetReservationsBySportCategoryIdAsync(Guid sportCategoryId)
        {
            var reservations = await _unitOfWork.ReservationRepository.GetReservationsBysportCategoryIdAsync(sportCategoryId);
            return reservations;
        }
        #endregion

        #region GetReservationsByStudentIdAsync 
        public async Task<List<Reservation>> GetReservationsByStudentIdAsync(string codeUIR)
        {
            var reservations = await _unitOfWork.ReservationRepository.GetReservationsByStudentIdAsync(codeUIR);
            return reservations;
        }
        #endregion

        #region DeleteAllReservationsAsync
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

        #endregion


        #region GetReservationByIdAsync
        public async Task<Reservation> GetReservationByIdAsync(Guid id)
        {
            Reservation reservation = await _unitOfWork.ReservationRepository.GetAsNoTracking(u => u.Id == id);

            return reservation;
        }
        #endregion


        #region GetReservationsListAsync

        public async Task<List<Reservation>> GetReservationsListAsync()
        {
            List<Reservation> reservationsList = await _unitOfWork.ReservationRepository.GetAllAsNoTracking();
            return reservationsList;
        }
        #endregion


        #endregion


        // Herlper Methods : 

        #region Helpers methods  

        private async Task<TimeSpan> GetDelayTimeAsync(List<string> codeUIRList, Guid sportId, DateTime delayThreshold)
        {
            if (codeUIRList == null || !codeUIRList.Any())
                throw new ArgumentException("No CodeUIR list provided.");

            var sport = await FetchSportAsync(sportId);
            var ReferenceSport = sport.ReferenceSport.Value;

            // Fetch reservations for the sport within the delay threshold
            var reservations = await _unitOfWork.ReservationRepository
                .GetReservationsByReferenceSportWithCodeUIRAsync(ReferenceSport, delayThreshold);

            // Find the most recent conflicting reservation
            var conflictingReservation = reservations
                .Where(r => r.CodeUIRList != null)
                .FirstOrDefault(r => r.CodeUIRList.Intersect(codeUIRList).Any());




            if (conflictingReservation != null)
            {
                // Calculate the delay time based on the most recent reservation's creation date
                var allowedNextReservationTime = conflictingReservation.DateCreation.AddDays(sport.Daysoff.Value);
                var delay = allowedNextReservationTime - DateTime.UtcNow;

                return delay > TimeSpan.Zero ? delay : TimeSpan.Zero; // Ensure no negative delay
            }

            return TimeSpan.Zero; // No conflict found
        }




        private async Task<List<string>> GetConflictingCodeUIRsAsync(List<string> codeUIRList, Guid sportId, DateTime delayTime)
        {
            if (codeUIRList == null || !codeUIRList.Any())
                return new List<string>();


            var sport = await FetchSportAsync(sportId);
            var ReferenceSport = sport.ReferenceSport.Value;

            var reservations = await _unitOfWork.ReservationRepository
                .GetReservationsByReferenceSportWithCodeUIRAsync(ReferenceSport, delayTime);

            // Find all conflicting CodeUIR values
            var conflictingCodeUIRs = reservations
                .SelectMany(r => r.CodeUIRList ?? new List<string>())
                .Intersect(codeUIRList)
                .ToList();

            //return reservationsWithCodeUIR.Any(r => r.CodeUIRList != null && r.CodeUIRList.Contains(codeUIR));

            return conflictingCodeUIRs;
        }
        // Add helper for fetching sport by ReferenceSport
        private async Task<Sport> FetchSportByReferenceAsync(int referenceSport)
        {
            return await _unitOfWork.SportRepository.GetAsync(s => s.ReferenceSport == referenceSport);
        }



        private async Task<Reservation> GetMostRecentReservationAsync(string codeUIR, int referenceSport, DateTime delayTime)
        {
            var reservations = await _unitOfWork.ReservationRepository
                .GetReservationsByReferenceSportAsync(codeUIR, referenceSport);

            var recentReservation = reservations.Where(r => r.DateCreation >= delayTime).OrderByDescending(r => r.DateCreation).FirstOrDefault();
            return recentReservation;
        }





        //Helper Methods
        private async Task<bool> HasConflictingCodeUIRListAsync(List<string> codeUIRList, Guid sportId, DateTime delayTime)
        {
            if (codeUIRList == null || !codeUIRList.Any())
                return false;

            var reservations = await _unitOfWork.ReservationRepository
                .GetReservationsForSportAsync(sportId, delayTime);

            // Check if any CodeUIR in the list exists in another reservation's CodeUIRList
            var conflictingReservations = reservations.Any(r =>
                r.CodeUIRList != null &&
                r.CodeUIRList.Intersect(codeUIRList).Any());
            return conflictingReservations;
        }







        private async Task<bool> HasConflictingCodeUIRsAsync(List<string> codeUIRList, int referenceSport, DateTime delayTime)
        {
            if (codeUIRList == null || !codeUIRList.Any())
                return false;

            // Fetch reservations with overlapping CodeUIR values
            var reservations = await _unitOfWork.ReservationRepository
                .GetReservationsByReferenceSportAndListCodeUIRAsync(codeUIRList, referenceSport);

            // Check if any reservation has a conflict
            var conflictingReservations = reservations.Any(r =>
                r.CodeUIRList != null &&
                r.CodeUIRList.Intersect(codeUIRList).Any());

            return conflictingReservations;
        }






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
            return DateTime.UtcNow.AddDays(-delayTimeMinutes);
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




        #endregion

    }
}