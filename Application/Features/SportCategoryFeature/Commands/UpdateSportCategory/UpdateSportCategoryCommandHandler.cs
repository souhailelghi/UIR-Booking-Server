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
                // Map the command to the Sport entity
                SportCategory sport = await _unitOfService.SportCategoryService.GetSportCategoryByIdAsync(request.Id);
                if (sport == null)
                {
                    return "Sport not found.";
                }

                // Update the properties of the sport entity
                sport.Name = request.Name;
              

                // Handle image upload if provided
                if (request.ImageUpload != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await request.ImageUpload.CopyToAsync(ms);
                        sport.Image = ms.ToArray();  // Store the byte array image
                    }
                }

                // Update the sport in the database
                await _unitOfService.SportCategoryService.UpdateSportCategoryAsync(sport);
                return "Sport updated successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }
    }
}
