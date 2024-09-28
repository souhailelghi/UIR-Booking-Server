using Application.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.SportFeature.Queries.GetAllSportsQuerie
{
    public class GetAllSportQuerieHandler : IRequestHandler<GetAllSportQuerie, List<Sport>>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IMapper _mapper;

        public GetAllSportQuerieHandler(IUnitOfService unitOfService, IMapper mapper)
        {
            _unitOfService = unitOfService;
            _mapper = mapper;
        }
        public async Task<List<Sport>> Handle(GetAllSportQuerie request, CancellationToken cancellationToken)
        {
            List<Sport> sports = await _unitOfService.SportService.GetSportsListAsync();
            return sports;
        }
    }
}
