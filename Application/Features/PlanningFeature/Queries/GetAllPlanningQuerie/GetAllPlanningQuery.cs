using Domain.Entities;
using MediatR;

namespace Application.Features.PlanningFeature.Queries.GetAllPlanningQuerie
{
    public class GetAllPlanningQuery : IRequest<List<Planning>>
    {
    }
}
