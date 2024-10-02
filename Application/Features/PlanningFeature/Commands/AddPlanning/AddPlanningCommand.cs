using Domain.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.Features.PlanningFeature.Commands.AddPlanning
{
    public class AddPlanningCommand : IRequest<Planning>
    {
        public PlanningDto PlanningDto { get; }

        public AddPlanningCommand(PlanningDto planningDto)
        {
            PlanningDto = planningDto;
        }
    }
}
