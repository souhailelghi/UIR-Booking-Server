using Application.IServices;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PlanningFeature.Queries.GetPlanningByIdQuerie
{
    public class GetPlanningByIdQueryHandler : IRequestHandler<GetPlanningByIdQuery, Planning>
    {
        private readonly IPlanningService _planningService;

        public GetPlanningByIdQueryHandler(IPlanningService planningService)
        {
            _planningService = planningService;
        }

        public async Task<Planning> Handle(GetPlanningByIdQuery request, CancellationToken cancellationToken)
        {
            return await _planningService.GetPlanningByIdAsync(request.PlanningId);
        }
    }
}
