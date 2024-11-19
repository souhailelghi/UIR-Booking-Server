using Application.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportFeature.Commands.UpdateSport
{
    public class UpdateSportCommandHanlder : IRequestHandler<UpdateSportCommand, string>
    {

        private readonly IUnitOfService _unitOfService;
        private readonly IMapper _mapper;

        public UpdateSportCommandHanlder(IUnitOfService unitOfService , IMapper mapper)
        {
           _unitOfService = unitOfService;
           _mapper = mapper;
        }

        public async Task<string> Handle(UpdateSportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Map the command to the Sport entity
                Sport sport = await _unitOfService.SportService.GetSportByIdAsync(request.Id);
                if (sport == null)
                {
                    return "Sport not found.";
                }

                // Update the properties of the sport entity
                sport.Name = request.Name;
                sport.ReferenceSport = request.ReferenceSport;
                sport.NbPlayer = request.NbPlayer;
                sport.Daysoff = request.Daysoff;
                sport.Conditions = request.Conditions;
                sport.Description = request.Description;

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
                await _unitOfService.SportService.UpdateSportAsync(sport);
                return "Sport updated successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }
    }
}
