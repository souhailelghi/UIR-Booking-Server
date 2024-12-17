using Application.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportCategoryFeature.Queries.GetTotalSportCategoryQueries
{
    public class GetTotalSportCategoryQueryHandler : IRequestHandler<GetTotalSportCategoryQuery, int>
    {
        private readonly IUnitOfService _unitOfService;

        public GetTotalSportCategoryQueryHandler(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;
        }


        public async Task<int> Handle(GetTotalSportCategoryQuery request, CancellationToken cancellationToken)
        {
          int total =  await _unitOfService.SportCategoryService.GetTotalSportCategorysAsync();
            return total;

        }
    }
}
