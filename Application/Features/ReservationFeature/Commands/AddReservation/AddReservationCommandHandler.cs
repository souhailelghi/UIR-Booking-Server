using Application.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ReservationFeature.Commands.AddReservation
{
    public class AddReservationCommandHandler : IRequestHandler<AddReservationCommand, bool>
    {

        private readonly IUnitOfService _unitOfService;
        private readonly IMapper _mapper;
        public AddReservationCommandHandler(IUnitOfService unitOfService  , IMapper mapper)
        {
            _unitOfService = unitOfService;
            _mapper = mapper;
        }
        public async Task<bool> Handle(AddReservationCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Reservation cannot be null.");
            }

            Reservation reservationMapped = _mapper.Map<Reservation>(request);
            bool isReservationSuccessful = await _unitOfService.ReservationService.ReservationAsync(
                reservationMapped.StudentId,
                reservationMapped.ReservationDate,
                reservationMapped.HourStart,
                reservationMapped.HourEnd,
                reservationMapped.StudentIdList,
                reservationMapped.SportId
            );

            return isReservationSuccessful;
        }

    }
}
