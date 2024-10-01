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


            Sport SportMapped = _mapper.Map<Sport>(request);
            Sport addedSport = await _unitOfService.SportService.AddSportAsync(SportMapped);
            return addedSport;
        }
    }
    }
