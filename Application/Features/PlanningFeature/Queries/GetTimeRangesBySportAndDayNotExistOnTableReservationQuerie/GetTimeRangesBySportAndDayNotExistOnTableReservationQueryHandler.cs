using Application.IServices;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PlanningFeature.Queries.GetTimeRangesBySportAndDayNotExistOnTableReservationQuerie
{
    public class GetTimeRangesBySportAndDayNotExistOnTableReservationQueryHandler : IRequestHandler<GetTimeRangesBySportAndDayNotExistOnTableReservationQuery, List<TimeRange>>
    {
        private readonly IPlanningService _planningService;

        public GetTimeRangesBySportAndDayNotExistOnTableReservationQueryHandler(IPlanningService planningService)
        {
            _planningService = planningService;
        }

        public async Task<List<TimeRange>> Handle(GetTimeRangesBySportAndDayNotExistOnTableReservationQuery request, CancellationToken cancellationToken)
        {
            return await _planningService.GetTimeRangesBySportAndDayNotExistOnTableReservationAsync(request.SportId, request.Day);
        }
    }
}
