
using Application.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.PlanningFeature.Commands.AddPlanning
{
    public class AddPlanningCommandHandler : IRequestHandler<AddPlanningCommand, Planning>
    {
        private readonly IPlanningService _planningService;
        private readonly IMapper _mapper;

        public AddPlanningCommandHandler(IPlanningService planningService, IMapper mapper)
        {
            _planningService = planningService;
            _mapper = mapper;
        }

        public async Task<Planning> Handle(AddPlanningCommand request, CancellationToken cancellationToken)
        {
            // Map the PlanningDto to the Planning entity
            var planning = _mapper.Map<Planning>(request.PlanningDto);
            return await _planningService.AddPlanningAsync(planning);
        }
    }
}
