using Application.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportFeature.Queries.GetTotalCourtsQuerie
{
    public class GetTotalCourtsQueryHandler : IRequestHandler<GetTotalCourtsQuery, int>
    {
        private readonly IUnitOfService _unitOfService;
        public GetTotalCourtsQueryHandler(IUnitOfService unitOfService)
        {
         _unitOfService = unitOfService;   
        
        }
        public async Task<int> Handle(GetTotalCourtsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfService.SportService.GetTotalCourtsAsync();
            
        }
    }
}
