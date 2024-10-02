using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PlanningFeature.Queries.GetAvailablePlanningQuerie
{
    public class GetAvailableTimeRangesQuery : IRequest<List<TimeRange>>
    {
    }
}
