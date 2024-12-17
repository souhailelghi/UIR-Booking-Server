using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportFeature.Queries.GetTotalCourtsBysportIdQueries
{
    public class GetTotalCourtsBysportIdQuery : IRequest<int>
    {
        public Guid CategorieId { get; }

        public GetTotalCourtsBysportIdQuery(Guid categorieId)
        {
            CategorieId = categorieId;
        }

    }
}
