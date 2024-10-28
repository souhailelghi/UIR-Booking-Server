using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PlanningFeature.Queries.GetPlanningByIdQuerie
{
    public class GetPlanningByIdQuery : IRequest<Planning>
    {
        public Guid PlanningId { get; set; }

        public GetPlanningByIdQuery(Guid planningId)
        {
            PlanningId = planningId;
        }
    }
}
