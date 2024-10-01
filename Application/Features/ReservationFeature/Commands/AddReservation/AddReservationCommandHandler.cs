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
            // Step 1: Retrieve reservations for the student and team members
            var conflictingReservations = await _reservationService.GetConflictingReservationsAsync(request.StudentId, request.StudentIdList, request.ReservationDate, request.HourStart, request.HourEnd);

            // Step 2: Check for any conflicts
            if (conflictingReservations.Any())
            {
                return "Conflict exists with another reservation.";
            }

            // Step 3: Create a new reservation if no conflicts
            var reservation = _mapper.Map<Reservation>(request);
            await _reservationService.AddReservationAsync(reservation);

            return "Reservation successfully created.";
        }
    }
}
