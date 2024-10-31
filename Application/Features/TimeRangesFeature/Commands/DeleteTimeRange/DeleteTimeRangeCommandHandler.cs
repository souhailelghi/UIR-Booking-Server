using Application.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TimeRangesFeature.Commands.DeleteTimeRange
{
    public class DeleteTimeRangeCommandHandler :IRequestHandler<DeleteTimeRangeCommand, string>
    {
        private readonly IUnitOfService _unitOfService;
        public DeleteTimeRangeCommandHandler(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;

        }

        public async Task<string> Handle(DeleteTimeRangeCommand request, CancellationToken cancellationToken)
        {
            await _unitOfService.TimeRangeService.DeleteTimeRangeAsync(request.Id);
            return "Delete TimeRange Successfully";
        }
    }
}
