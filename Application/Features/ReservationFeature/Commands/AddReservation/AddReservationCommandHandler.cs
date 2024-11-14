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
            // Check if the user or team can book
            string canBookMessage = await _reservationService.CanTeamOrUserBookAsync(request.CodeUIR, request.CodeUIRList, request.SportId);

            if (!canBookMessage.Contains("No conflicting reservations found"))
            {
                return canBookMessage; // Return the error message directly
            }

            // Attempt to create the reservation
            string bookingMessage = await _reservationService.BookAsync(
                request.CodeUIR,
                request.SportCategoryId,
                request.ReservationDate,
                request.DayBooking,
                request.HourStart,
                request.HourEnd,
                request.CodeUIRList,
                request.SportId
                
            );

            return bookingMessage; // Return the booking message
        }
    }
}
