using Application.IServices;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PlanningFeature.Queries.GetAvailableTimeRangesBySportQuerie
{
    public class GetAvailableTimeRangesBySportQueryHandler : IRequestHandler<GetAvailableTimeRangesBySportQuery, List<TimeRange>>
    {
        private readonly IPlanningService _planningService;

        public GetAvailableTimeRangesBySportQueryHandler(IPlanningService planningService)
        {
            _planningService = planningService;
        }

        public async Task<List<TimeRange>> Handle(GetAvailableTimeRangesBySportQuery request, CancellationToken cancellationToken)
        {
            return await _planningService.GetAvailableTimeRangesBySportAsync(request.SportId);
        }
    }
}
