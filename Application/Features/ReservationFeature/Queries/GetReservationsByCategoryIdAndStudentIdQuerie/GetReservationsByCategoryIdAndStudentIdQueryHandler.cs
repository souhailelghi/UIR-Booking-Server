using Application.IServices;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationFeature.Queries.GetReservationsByCategoryIdAndStudentIdQuerie
{
    public class GetReservationsByCategoryIdAndStudentIdQueryHandler : IRequestHandler<GetReservationsByCategoryIdAndStudentIdQuery, List<Reservation>>
    {
        private readonly IUnitOfService _unitOfService;

        public GetReservationsByCategoryIdAndStudentIdQueryHandler(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;
        }

        public async Task<List<Reservation>> Handle(GetReservationsByCategoryIdAndStudentIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfService.ReservationService.GetReservationsByCategoryAndStudentIdAsync(request.SportCategoryId, request.StudentId);
        }
    }
}
