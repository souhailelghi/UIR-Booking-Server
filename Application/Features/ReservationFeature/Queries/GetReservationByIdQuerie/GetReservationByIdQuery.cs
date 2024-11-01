using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationFeature.Queries.GetReservationByIdQuerie
{
    public class GetReservationByIdQuery : IRequest<Reservation>
    {
        public Guid Id { get; set; }
        public GetReservationByIdQuery(Guid id)
        {
            Id=id;
        
        }
    }
}
