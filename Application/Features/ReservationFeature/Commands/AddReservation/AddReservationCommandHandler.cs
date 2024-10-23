using Application.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
            // Create a new Reservation entity from the request
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(), // Generate a new ID for the reservation
                SportId = request.SportId,
                ReservationDate = request.ReservationDate,
                DayBooking = request.DayBooking,
                HourStart = request.HourStart,
                HourEnd = request.HourEnd,
                DateCreation = DateTime.UtcNow,
                DateModification = DateTime.UtcNow,
                CodeUIRList = request.CodeUIRList ?? new List<string>()// Assuming you have a way to handle this in the Reservation entity
            };

            // Call the booking service to add the reservation
            bool isBooked = await _reservationService.BookAsync(
                request.CodeUIR,
                request.ReservationDate,
                request.DayBooking,
                request.HourStart,
                request.HourEnd,
                request.CodeUIRList,
                request.SportId);

            if (!isBooked)
            {
                // Return an appropriate message if booking fails
                return "Reservation could not be created. Please check if you meet the requirements.";
            }

            // Return success message
            return "Reservation successfully created.";
        }
    }
}
