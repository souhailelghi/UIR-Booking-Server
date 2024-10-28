using Application.IServices;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PlanningFeature.Queries.GetAllPlanningsBySportIdQuerie
{
    public class GetAllPlanningsBySportIdQueryHandler : IRequestHandler<GetAllPlanningsBySportIdQuery, List<Planning>>
    {
        private readonly IPlanningService _planningService;

        public GetAllPlanningsBySportIdQueryHandler(IPlanningService planningService)
        {
            _planningService = planningService;
        }

        public async Task<List<Planning>> Handle(GetAllPlanningsBySportIdQuery request, CancellationToken cancellationToken)
        {
            return await _planningService.GetAllPlanningsBySportId(request.SportId);
        }
    }

}
