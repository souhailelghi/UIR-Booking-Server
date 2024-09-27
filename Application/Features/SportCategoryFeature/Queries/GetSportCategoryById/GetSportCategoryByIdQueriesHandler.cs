using Application.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportCategoryFeature.Queries.GetSportCategoryById
{
    public class GetSportCategoryByIdQueriesHandler : IRequestHandler<GetSportCategoryByIdQueries, SportCategory>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IMapper _mapper;

        public GetSportCategoryByIdQueriesHandler(IUnitOfService unitOfService , IMapper mapper)
        {
            _unitOfService = unitOfService;
            _mapper = mapper;
        
        }


        public async Task<SportCategory> Handle(GetSportCategoryByIdQueries request, CancellationToken cancellationToken)
        {
            SportCategory result = await _unitOfService.SportCategoryService.GetSportCategoryByIdAsync(request.Id);
            return result;
        }
    }
}
