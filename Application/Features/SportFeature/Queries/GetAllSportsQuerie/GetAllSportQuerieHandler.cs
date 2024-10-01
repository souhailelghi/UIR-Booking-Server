using Application.IServices;
using Domain.Entities;
using MediatR;

namespace Application.Features.SportFeature.Queries.GetAllSportsQuerie
{
    public class GetAllSportQuerieHandler : IRequestHandler<GetAllSportQuerie, List<Sport>>
    {
        private readonly IUnitOfService _unitOfService;
        public GetAllSportQuerieHandler(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;
        }



        public async Task<List<Sport>> Handle(GetAllSportQuerie request, CancellationToken cancellationToken)
        {
           List<Sport> sport = await _unitOfService.SportService.GetSportsListAsync();

            return sport;
        }
    }
}
