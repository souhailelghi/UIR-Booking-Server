using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportCategoryFeature.Queries.GetSportCategoryById
{
    public class GetSportCategoryByIdQueries : IRequest<SportCategory>
    {
        public Guid Id { get; set; }

        public GetSportCategoryByIdQueries(Guid id)
        {
            Id = id;
            
        }
    }
}
