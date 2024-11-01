using Application.IServices;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationFeature.Queries.GetAllReservationQuerie
{
    public class GetAllReservationQueryHandler : IRequestHandler<GetAllReservationQuery, List<Reservation>>
    {
        private readonly IUnitOfService _unitOfService;
        public GetAllReservationQueryHandler(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;
            
        }
            
        
        public async Task<List<Reservation>> Handle(GetAllReservationQuery request, CancellationToken cancellationToken)
        {
            List <Reservation> reservation = await _unitOfService.ReservationService.GetReservationsListAsync();
            return reservation;
        }
    }
}
