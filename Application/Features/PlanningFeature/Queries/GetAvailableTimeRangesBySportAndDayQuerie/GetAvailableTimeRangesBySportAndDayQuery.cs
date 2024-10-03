using Domain.Entities;
using Domain.Enums;
using MediatR;

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
