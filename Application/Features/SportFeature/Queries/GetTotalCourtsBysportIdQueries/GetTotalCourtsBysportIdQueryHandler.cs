using Application.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportFeature.Queries.GetTotalCourtsBysportIdQueries
{
    public class GetTotalCourtsBysportIdQueryHandler : IRequestHandler<GetTotalCourtsBysportIdQuery, int>
    {
        private readonly IUnitOfService _unitOfService;
        public GetTotalCourtsBysportIdQueryHandler(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;
        
        }
        public async Task<int> Handle(GetTotalCourtsBysportIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfService.SportService.FetchTotalCourtsBysportIdAsync(request.CategorieId);
        }
    }
}
