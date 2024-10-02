using Application.IServices;
using Domain.Entities;
using MediatR;

namespace Application.Features.PlanningFeature.Queries.GetAllPlanningQuerie
{
    public class GetAllPlanningQueryHandler : IRequestHandler<GetAllPlanningQuery, List<Planning>>
    {
        private readonly IUnitOfService _unitOfService;

        public GetAllPlanningQueryHandler(IUnitOfService unitOfServices)
        {
            _unitOfService = unitOfServices;
        }
        
        public async Task<List<Planning>> Handle(GetAllPlanningQuery request, CancellationToken cancellationToken)
        {
            List<Planning> plannings = await _unitOfService.PlanningService.GetAllPlanningAsync();

            return plannings;
        }
    }
}
