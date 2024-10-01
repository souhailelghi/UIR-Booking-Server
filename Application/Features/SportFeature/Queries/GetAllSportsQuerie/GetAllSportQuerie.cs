using Domain.Entities;
using MediatR;

namespace Application.Features.SportFeature.Queries.GetAllSportsQuerie
{
    public class GetAllSportQuerie : IRequest<List<Sport>>
    {
    }
}
