using Application.Features.SportCategoryFeature.Commands.DeleteSportCategory;
using Application.IServices;
using Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.EventFeature.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, string>
    {
        private readonly IUnitOfService _unitOfService;
        public DeleteEventCommandHandler(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;
        }
        public async Task<string> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            await _unitOfService.EventService.DeleteEventAsync(request.Id);
            return "Delete Successfully";
        }
    }
}
