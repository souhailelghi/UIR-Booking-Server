using Application.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.EventFeature.Queries.GetTotalEventsQueries
{
    public class GetTotalEventsQueryHandler : IRequestHandler<GetTotalEventsQuery, int>
    {

        private readonly IUnitOfService _unitOfService;
        public GetTotalEventsQueryHandler(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;
        
        }
        public async Task<int> Handle(GetTotalEventsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfService.EventService.GetTotalEventsList();
        }
    }
}
