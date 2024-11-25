using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.PlanningFeature.Queries.GetAvailableTimeRangesBySportAndDayQuerie
{
    public class GetAvailableTimeRangesBySportAndDayQuery : IRequest<List<TimeRange>>
    {
        public Guid SportId { get; }
 

        public GetAvailableTimeRangesBySportAndDayQuery(Guid sportId)
        {
            SportId = sportId;
         
        }
    }
}
