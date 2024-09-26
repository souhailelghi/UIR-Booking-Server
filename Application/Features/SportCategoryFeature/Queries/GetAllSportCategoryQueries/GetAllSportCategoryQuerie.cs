using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportCategoryFeature.Queries.GetAllSportCategoryQueries
{
    public class GetAllSportCategoryQuerie : IRequest<List<SportCategory>>
    {
    }
}
