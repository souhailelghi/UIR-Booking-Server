using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportFeature.Queries.GetSportById
{
    public class GetSportByIdQuery : IRequest<Sport>
    {
        public Guid Id { get; set; }
        public GetSportByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
