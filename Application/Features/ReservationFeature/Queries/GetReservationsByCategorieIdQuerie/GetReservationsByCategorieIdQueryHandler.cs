using Application.IServices;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationFeature.Queries.GetReservationsByCategorieIdQuerie
{
    public class GetReservationsByCategorieIdQueryHandler : IRequestHandler<GetReservationsByCategorieIdQuery, List<Reservation>>
    {
        private readonly IUnitOfService _unitOfService;

        public GetReservationsByCategorieIdQueryHandler(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;
        }

        public async Task<List<Reservation>> Handle(GetReservationsByCategorieIdQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _unitOfService.ReservationService.GetReservationsBySportCategoryIdAsync(request.SportCategoryId);
            return reservations;
        }
    }
}
