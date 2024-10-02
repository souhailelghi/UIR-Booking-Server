using Application.IServices;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PlanningFeature.Queries.GetAvailablePlanningQuerie
{
    public class GetAvailableTimeRangesQueryHandler : IRequestHandler<GetAvailableTimeRangesQuery, List<TimeRange>>
    {
        private readonly IPlanningService _planningService;

        public GetAvailableTimeRangesQueryHandler(IPlanningService planningService)
        {
            _planningService = planningService;
        }

        public async Task<List<TimeRange>> Handle(GetAvailableTimeRangesQuery request, CancellationToken cancellationToken)
        {
            return await _planningService.GetAvailableTimeRangesAsync();
        }
    }
}
