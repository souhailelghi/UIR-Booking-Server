using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PlanningFeature.Queries.GetAvailableTimeRangesByReferenceSportAndDayQuerie
{
    public class GetAvailableTimeRangesByReferenceSportAndDayQuery : IRequest<List<TimeRange>>
    {
        public int ReferenceSport { get; }
        public DayOfWeekEnum Day { get; }

        public GetAvailableTimeRangesByReferenceSportAndDayQuery(int referenceSport, DayOfWeekEnum day)
        {
            ReferenceSport = referenceSport;
            Day = day;
        }
    }
}
