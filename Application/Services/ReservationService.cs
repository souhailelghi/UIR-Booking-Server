using Application.IServices;
using Application.IUnitOfWorks;
using AutoMapper;
using Domain.Entities;




namespace Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReservationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Reservation> AddReservationAsync(Reservation reservation)
        {
            reservation.Id = Guid.NewGuid();
            await _unitOfWork.ReservationRepository.CreateAsync(reservation);
            await _unitOfWork.CommitAsync();
            return reservation;
        }

        public async Task<bool> CanTeamOrUserReservationAsync(Guid studentId, List<Guid> studentIdList, Guid sportId)
        {
            var sport = await _unitOfWork.SportRepository.GetAsNoTracking(u => u.Id == sportId);
            if (sport == null)
            {
                return false; // Sport not found
            }

            var delayTime = DateTime.UtcNow.AddMinutes(-sport.Daysoff.GetValueOrDefault());
            var existingReservation = await _unitOfWork.ReservationRepository
                .GetAsTracking(r => r.StudentId == studentId && r.ReservationDate >= delayTime);

            if (existingReservation != null)
            {
                // Log the conflict reason for the student
                Console.WriteLine($"Conflict: Student {studentId} has an existing reservation.");
                return false; // Conflict exists for this student
            }

            var studentsExist = (await _unitOfWork.StudentRepository
                .GetAllAsTracking(u => studentIdList.Contains(u.Id)))
                .Select(u => u.Id)
                .ToList();

            if (studentsExist.Count() != studentIdList.Count)
            {
                Console.WriteLine(" Some students from the list don't exist in the database.");
                return false; // Some students don't exist
            }

            var reservations = await _unitOfWork.ReservationRepository
                .GetAllAsNoTracking(b => studentIdList.Contains(b.StudentId) && b.ReservationDate >= delayTime);

            if (reservations.Any())
            {
                // Log the conflict reason for the team
                Console.WriteLine("Conflict: Team members have existing reservations.");
                return false; // Conflict for team members
            }

            // No conflicts found
            return true;
        }















        public async Task<bool> ReservationAsync(Guid studentId, DateTime reservationDate, TimeSpan hourStart, TimeSpan hourEnd, List<Guid> studentIdList, Guid sportId)
        {
            if (!await CanTeamOrUserReservationAsync(studentId, studentIdList, sportId))
            {
                return false;
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
            return true;
        }

        public async Task<Reservation> GetBookingByIdAsync(Guid id)
        {
            Reservation reservation = await _unitOfWork.ReservationRepository.GetAsNoTracking(u => u.Id == id);
            return reservation;
        }
    }
}
