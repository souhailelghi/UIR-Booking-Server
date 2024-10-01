using Application.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ReservationFeature.Commands.AddReservation
{
    public class AddReservationCommandHandler : IRequestHandler<AddReservationCommand, string>
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public AddReservationCommandHandler(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        public async Task<string> Handle(AddReservationCommand request, CancellationToken cancellationToken)
        {
            // Step 1: Check if the student can book the reservation based on Daysoff and time conflicts
            var canBook = await _reservationService.CanBookReservationAsync(request.StudentId, request.SportId, request.ReservationDate, request.HourStart, request.HourEnd);

            if (!canBook)
            {
                return "Cannot book this reservation due to time conflict or Daysoff restriction.";
            }

            // Step 2: Create a new reservation if no conflicts
            var reservation = _mapper.Map<Reservation>(request);
            await _reservationService.AddReservationAsync(reservation);

            return "Reservation successfully created.";
        }
    }
}
