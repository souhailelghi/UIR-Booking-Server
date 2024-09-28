using Application.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportFeature.Commands.DeleteSport
{
    public class DeleteSportCommandHandler : IRequestHandler<DeleteSportCommand, string>
    {

        private readonly IUnitOfService _unitOfService;
        public DeleteSportCommandHandler(IUnitOfService unitOfService)
        {
         _unitOfService = unitOfService;   
        
        }
        public async Task<string> Handle(DeleteSportCommand request, CancellationToken cancellationToken)
        {
            await _unitOfService.SportService.DeleteSportAsync(request.Id);
            return "Delete Sport Successfully";
        }
    }
}
