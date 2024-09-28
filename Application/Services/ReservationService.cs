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
    }
}
