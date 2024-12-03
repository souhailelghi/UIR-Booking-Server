using Application.Features.SportCategoryFeature.Commands.UpdateSportCategory;
using Application.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.EventFeature.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, string>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandler(IUnitOfService unitOfService, IMapper mapper)
        {
            _unitOfService = unitOfService;
            _mapper = mapper;

        }
        public async Task<string> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {

            try
            {
                // Map the command to the events entity
                Event events = await _unitOfService.EventService.GetEventByIdAsync(request.Id);
                if (events == null)
                {
                    return "event not found.";
                }

                // Update the properties of the event entity
                events.Title = request.Title;
                events.Description = request.Description;
                events.lien = request.Lien ;


                // Handle image upload if provided
                if (request.ImageUpload != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await request.ImageUpload.CopyToAsync(ms);
                        events.Image = ms.ToArray();  // Store the byte array image
                    }
                }

                // Update the event in the database
                await _unitOfService.EventService.UpdateEventAsync(events);
                return "Event updated successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }
    }
}
