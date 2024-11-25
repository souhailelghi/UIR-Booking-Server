using Application.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationFeature.Queries.CheckReservationAccessRequestQuerie
{
    public class CheckReservationAccessHandler : IRequestHandler<CheckReservationAccessRequest, bool>
    {
        private readonly IReservationService _reservationService;

        public CheckReservationAccessHandler(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<bool> Handle(CheckReservationAccessRequest request, CancellationToken cancellationToken)
        {
            // Call the service method to check access
            return await _reservationService.CheckUserHaveAccessReservationAsync(request.CodeUIR, request.CodeUIRList, request.SportId);
        }
    }

}
