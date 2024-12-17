using Application.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationFeature.Queries.GetTotalReservationsQuerie
{
    public class GetTotalReservationsQueryHandler : IRequestHandler<GetTotalReservationsQuery, int>
    {
        private readonly IUnitOfService _unitOfService;
        public GetTotalReservationsQueryHandler(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;
        }
        public async Task<int> Handle(GetTotalReservationsQuery request, CancellationToken cancellationToken)
        {
           return await _unitOfService.ReservationService.GetTotalReservationsListAsync();
        }
    }
}
