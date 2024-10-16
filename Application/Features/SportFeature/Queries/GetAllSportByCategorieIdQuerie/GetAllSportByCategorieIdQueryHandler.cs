using Application.IServices;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportFeature.Queries.GetAllSportByCategorieIdQuerie
{
    public class GetAllSportByCategorieIdQueryHandler : IRequestHandler<GetAllSportByCategorieIdQuery, List<Sport>>
    {
        private readonly IUnitOfService _unitOfService;

        public GetAllSportByCategorieIdQueryHandler(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;
        }

        public async Task<List<Sport>> Handle(GetAllSportByCategorieIdQuery request, CancellationToken cancellationToken)
        {
            // Fetch the sports using the service
            List<Sport> sportsList = await _unitOfService.SportService.GetAllSportByCategorieIdAsync(request.CategorieId);

            return sportsList;
        }
    }
}
