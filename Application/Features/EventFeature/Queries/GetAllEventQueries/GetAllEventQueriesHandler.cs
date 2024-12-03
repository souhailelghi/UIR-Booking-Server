using Application.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.EventFeature.Queries.GetAllEventQueries
{
    public class GetAllEventQueriesHandler : IRequestHandler<GetAllEventQueries, List<Event>>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IMapper _mapper;

        public GetAllEventQueriesHandler(IUnitOfService unitOfService, IMapper mapper)
        {
            _unitOfService = unitOfService;
            _mapper = mapper;
        }

       
        
    
    public async Task<List<Event>> Handle(GetAllEventQueries request, CancellationToken cancellationToken)
        {
            List<Event> events = await _unitOfService.EventService.GetEventsList();
            return events;
        }
    }
}
