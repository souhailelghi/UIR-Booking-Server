using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportFeature.Queries.GetAllSportByCategorieIdQuerie
{
    public class GetAllSportByCategorieIdQuery : IRequest<List<Sport>>
    {
        public Guid CategorieId { get; }

        public GetAllSportByCategorieIdQuery(Guid categorieId)
        {
            CategorieId = categorieId;
        }
    }
}
