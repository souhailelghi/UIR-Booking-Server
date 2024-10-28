using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PlanningFeature.Queries.GetAllPlanningsBySportIdQuerie
{
    public class GetAllPlanningsBySportIdQuery : IRequest<List<Planning>>
    {
        public Guid SportId { get; set; }
        public GetAllPlanningsBySportIdQuery(Guid sportId)
        {
            SportId = sportId;
        }
    }

}
