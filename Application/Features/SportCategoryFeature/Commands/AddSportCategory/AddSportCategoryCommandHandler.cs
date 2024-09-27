using Application.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Reflection.Metadata;

namespace Application.Features.SportCategoryFeature.Commands.AddSportCategory
{
    public class AddSportCategoryCommandHandler : IRequestHandler<AddSportCategoryCommand, SportCategory>
    {

        private readonly IUnitOfService _unitOfService;
        private readonly IMapper _mapper;
        public AddSportCategoryCommandHandler(IMapper mapper, IUnitOfService unitOfService)
        {
            _mapper = mapper;
            _unitOfService=unitOfService;
            
        }

       

        public async Task<SportCategory> Handle(AddSportCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "SportCategory cannot be null.");
            }

           
            SportCategory SportCategoryMapped = _mapper.Map<SportCategory>(request);
            SportCategory addedSportCategory = await _unitOfService.SportCategoryService.AddSportCategoryAsync(SportCategoryMapped);
            return addedSportCategory;
        }

      

    }
}
