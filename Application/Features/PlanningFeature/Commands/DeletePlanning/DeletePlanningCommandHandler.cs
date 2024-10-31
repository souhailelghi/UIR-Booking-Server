using Application.Features.SportFeature.Commands.DeleteSport;
using Application.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PlanningFeature.Commands.DeletePlanning
{
    public class DeletePlanningCommandHandler : IRequestHandler<DeletePlanningCommand, string>
    {
        private readonly IUnitOfService _unitOfService;
        public DeletePlanningCommandHandler(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;

        }
        public async Task<string> Handle(DeletePlanningCommand request, CancellationToken cancellationToken)
        {
            await _unitOfService.PlanningService.DeletePlanningAsync(request.Id);
            return "Delete Planning Successfully";
        }
    }
}
