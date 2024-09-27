using Application.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportCategoryFeature.Commands.UpdateSportCategory
{
    public class UpdateSportCategoryCommandHandler : IRequestHandler<UpdateSportCategoryCommand, string>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IMapper _mapper;

        public UpdateSportCategoryCommandHandler(IUnitOfService unitOfService , IMapper mapper)
        {
            _unitOfService = unitOfService;
            _mapper = mapper;

        }

        
        public async Task<string> Handle(UpdateSportCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                SportCategory sportCategory = _mapper.Map<SportCategory>(request);
                await _unitOfService.SportCategoryService.UpdateSportCategoryAsync(sportCategory);
                return "SportCategory updated successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }
    }
}
