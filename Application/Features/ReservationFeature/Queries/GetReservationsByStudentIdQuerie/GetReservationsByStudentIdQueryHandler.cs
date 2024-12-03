using Application.IServices;
using Domain.Entities;
using MediatR;

namespace Application.Features.ReservationFeature.Queries.GetReservationsByStudentIdQuerie
{
    public class GetReservationsByStudentIdQueryHandler : IRequestHandler<GetReservationsByStudentIdQuery, List<Reservation>>
    {
        private readonly IUnitOfService _unitOfService;

        public GetReservationsByStudentIdQueryHandler(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;
        }

        public async Task<List<Reservation>> Handle(GetReservationsByStudentIdQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _unitOfService.ReservationService.GetReservationsByStudentIdAsync(request.CodeUIR);
            return reservations;
        }
    }
}