using Application.IServices;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PlanningFeature.Queries.GetAvailableTimeRangesByReferenceSportAndDayQuerie
{
    public class GetAvailableTimeRangesByReferenceSportAndDayQueryHandler : IRequestHandler<GetAvailableTimeRangesByReferenceSportAndDayQuery, List<TimeRange>>
    {
        private readonly IPlanningService _planningService;

        public GetAvailableTimeRangesByReferenceSportAndDayQueryHandler(IPlanningService planningService)
        {
            _planningService = planningService;
        }

        public async Task<List<TimeRange>> Handle(GetAvailableTimeRangesByReferenceSportAndDayQuery request, CancellationToken cancellationToken)
        {
            return await _planningService.GetTimeRangesByReferenceSportAndDayAsync(request.ReferenceSport, request.Day);
        }
    }
}
