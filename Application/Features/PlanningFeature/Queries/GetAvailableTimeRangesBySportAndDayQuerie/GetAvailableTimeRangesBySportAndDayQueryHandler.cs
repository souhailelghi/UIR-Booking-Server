using Application.IServices;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PlanningFeature.Queries.GetAvailableTimeRangesBySportAndDayQuerie
{
    public class GetAvailableTimeRangesBySportAndDayQueryHandler : IRequestHandler<GetAvailableTimeRangesBySportAndDayQuery, List<TimeRange>>
    {
        private readonly IPlanningService _planningService;

        public GetAvailableTimeRangesBySportAndDayQueryHandler(IPlanningService planningService)
        {
            _planningService = planningService;
        }

        public async Task<List<TimeRange>> Handle(GetAvailableTimeRangesBySportAndDayQuery request, CancellationToken cancellationToken)
        {
            return await _planningService.GetAvailableTimeRangesBySportAndDayAsync(request.SportId, request.Day);
        }
    }
}
