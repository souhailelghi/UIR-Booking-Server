using Application.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Application.Features.PlanningFeature.Commands.UpdatePlanning
{
    public class UpdatePlanningCommandeHandler : IRequestHandler<UpdatePlanningCommand, string>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IMapper _mapper;

        public UpdatePlanningCommandeHandler(IUnitOfService unitOfService, IMapper mapper)
        {
            _unitOfService = unitOfService;
            _mapper = mapper;
        }

        public async Task<string> Handle(UpdatePlanningCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the existing planning entry
                var existingPlanning = await _unitOfService.PlanningService.GetPlanningByIdAsync(request.Id);
                if (existingPlanning == null)
                {
                    throw new Exception("Planning not found.");
                }

                // Update the Day
                existingPlanning.Day = request.Day;

                // Map the new TimeRanges from the request and update the Planning
                var updatedTimeRanges = _mapper.Map<List<TimeRange>>(request.TimeRanges);

                // Clear the existing TimeRanges and set the new ones
                existingPlanning.TimeRanges.Clear();
                existingPlanning.TimeRanges = updatedTimeRanges;

                // Call the service to update the planning
                await _unitOfService.PlanningService.UpdatePlanningAsync(existingPlanning);

                return "Planning updated successfully with time ranges";
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }
    }
}
