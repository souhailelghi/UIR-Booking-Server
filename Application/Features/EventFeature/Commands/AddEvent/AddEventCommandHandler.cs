using Application.Features.SportFeature.Commands.AddSport;
using Application.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.EventFeature.Commands.AddEvent
{
    public class AddEventCommandHandler : IRequestHandler<AddEventCommand,Event>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IMapper _mapper;
        public AddEventCommandHandler(IMapper mapper, IUnitOfService unitOfService)
        {
            _mapper = mapper;
            _unitOfService = unitOfService;

        }
        public async Task<Event> Handle(AddEventCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Event cannot be null.");
            }

            // Map the AddSportCommand to the Sport entity
            Event eventMapped = _mapper.Map<Event>(request);

            // Add the sport using the service and return the result
            Event addedEvent = await _unitOfService.EventService.AddEventAsync(eventMapped);
            return addedEvent;
        }
    }
}
