using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationFeature.Queries.GetReservationsByCategorieIdQuerie
{
    public class GetReservationsByCategorieIdQuery : IRequest<List<Reservation>>
    {
        public Guid SportCategoryId { get; set; }
        public GetReservationsByCategorieIdQuery(Guid sportCategoryId)
        {
            SportCategoryId = sportCategoryId;
        }
    }
}
