using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PlanningFeature.Queries.GetAvailableTimeRangesBySportAndDayQuerie
{
    public class GetAvailableTimeRangesBySportAndDayQuery : IRequest<List<TimeRange>>
    {
        public Guid SportId { get; }
        public DayOfWeekEnum Day { get; }

        public GetAvailableTimeRangesBySportAndDayQuery(Guid sportId, DayOfWeekEnum day)
        {
            SportId = sportId;
            Day = day;
        }
    }
}
