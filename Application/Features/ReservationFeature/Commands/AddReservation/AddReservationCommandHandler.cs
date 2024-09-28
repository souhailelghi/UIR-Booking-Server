using Application.IServices;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationFeature.Commands.AddReservation
{
    public class AddReservationCommandHandler : IRequestHandler<AddReservationCommand, Reservation>
    {

        private readonly IUnitOfService _unitOfService;
        private readonly IMapper _mapper;
        public AddReservationCommandHandler(IUnitOfService unitOfService  , IMapper mapper)
        {
            _unitOfService = unitOfService;
            _mapper = mapper;
        }
        public async Task<Reservation> Handle(AddReservationCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Reservation cannot be null.");
            }


            Reservation ReservationMapped = _mapper.Map<Reservation>(request);
            Reservation addedReservation = await _unitOfService.ReservationService.AddReservationAsync(ReservationMapped);
            return addedReservation;
        }
    }
}
