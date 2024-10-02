using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PlanningFeature.Queries.GetAvailableTimeRangesBySportQuerie
{
    public class GetAvailableTimeRangesBySportQuery : IRequest<List<TimeRange>>
    {
        public Guid SportId { get; }

        public GetAvailableTimeRangesBySportQuery(Guid sportId)
        {
            SportId = sportId;
        }
    }
}
