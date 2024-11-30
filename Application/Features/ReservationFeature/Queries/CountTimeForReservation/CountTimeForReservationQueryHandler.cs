using Application.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationFeature.Queries.CountTimeForReservation
{
    public class CountTimeForReservationQueryHandler : IRequestHandler<CountTimeForReservationQuery, string>
    {
        private readonly IReservationService _reservationService;

        public CountTimeForReservationQueryHandler(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<string> Handle(CountTimeForReservationQuery request, CancellationToken cancellationToken)
        {
            // Delegate the logic to the service layer
            return await _reservationService.CountTimeAsync(request.CodeUIR, request.CodeUIRList, request.SportId);
        }
    }
}
