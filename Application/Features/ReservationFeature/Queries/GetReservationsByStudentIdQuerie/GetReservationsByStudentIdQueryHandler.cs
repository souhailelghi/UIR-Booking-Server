using Application.IServices;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var reservations = await _unitOfService.ReservationService.GetReservationsByStudentIdAsync(request.StudentId);
            return reservations;
        }
    }
}
