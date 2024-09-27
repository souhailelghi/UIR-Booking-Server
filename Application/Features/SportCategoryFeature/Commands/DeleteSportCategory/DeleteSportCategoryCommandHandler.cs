using Application.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportCategoryFeature.Commands.DeleteSportCategory
{
    public class DeleteSportCategoryCommandHandler : IRequestHandler<DeleteSportCategoryCommand, string>
    {
        private readonly IUnitOfService _unitOfService;
        public DeleteSportCategoryCommandHandler(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;
        }

        public async Task<string> Handle(DeleteSportCategoryCommand request, CancellationToken cancellationToken)
        {
           await _unitOfService.SportCategoryService.DeleteSportCategoryAsync(request.Id);
            return "Delete Successfully";
        }
    }
}
