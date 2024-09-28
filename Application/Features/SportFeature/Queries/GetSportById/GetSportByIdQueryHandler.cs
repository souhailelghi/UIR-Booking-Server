using Application.IServices;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportFeature.Queries.GetSportById
{
    public class GetSportByIdQueryHandler : IRequestHandler<GetSportByIdQuery, Sport>
    {
        private readonly IUnitOfService _unitOfService;
        public GetSportByIdQueryHandler(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;

        }   

        

        public async Task<Sport> Handle(GetSportByIdQuery request, CancellationToken cancellationToken)
        {
            Sport result = await _unitOfService.SportService.GetSportByIdAsync(request.Id);
            return result;
        }
    }
}
