using Application.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.SportFeature.Commands.AddSport
{
    public class AddSportCommandHandler : IRequestHandler<AddSportCommand, Sport>
    {

        private readonly IUnitOfService _unitOfService;
        private readonly IMapper _mapper;
        public AddSportCommandHandler(IMapper mapper, IUnitOfService unitOfService)
        {
            _mapper = mapper;
            _unitOfService = unitOfService;

        }


        public async Task<Sport> Handle(AddSportCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Sport cannot be null.");
            }

            // Map the AddSportCommand to the Sport entity
            Sport sportMapped = _mapper.Map<Sport>(request);

            // Add the sport using the service and return the result
            Sport addedSport = await _unitOfService.SportService.AddSportAsync(sportMapped);
            return addedSport;
        }
    }
    }
