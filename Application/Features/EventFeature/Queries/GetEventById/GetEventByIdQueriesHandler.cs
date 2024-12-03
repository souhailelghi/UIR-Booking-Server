using Application.Features.SportCategoryFeature.Queries.GetSportCategoryById;
using Application.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.EventFeature.Queries.GetEventById
{
    public class GetEventByIdQueriesHandler : IRequestHandler<GetEventByIdQueries, Event>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IMapper _mapper;

        public GetEventByIdQueriesHandler(IUnitOfService unitOfService, IMapper mapper)
        {
            _unitOfService = unitOfService;
            _mapper = mapper;

        }
        public async Task<Event> Handle(GetEventByIdQueries request, CancellationToken cancellationToken)
        {
            Event result = await _unitOfService.EventService.GetEventByIdAsync(request.Id);
            return result;
        }
    }
}
