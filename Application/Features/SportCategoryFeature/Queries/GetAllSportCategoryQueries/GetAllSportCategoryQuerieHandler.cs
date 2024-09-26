using Application.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Features.SportCategoryFeature.Queries.GetAllSportCategoryQueries
{
    public class GetAllSportCategoryQuerieHandler : IRequestHandler<GetAllSportCategoryQuerie, List<SportCategory>>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IMapper _mapper;

        public GetAllSportCategoryQuerieHandler(IUnitOfService unitOfService, IMapper mapper)
        {
            _unitOfService = unitOfService;
            _mapper = mapper;
        }

        public async Task<List<SportCategory>> Handle(GetAllSportCategoryQuerie request, CancellationToken cancellationToken)
        {
            List<SportCategory> sportCategorys = await _unitOfService.SportCategoryService.GetSportCategorysList();
            return sportCategorys;
        }
    }
}
