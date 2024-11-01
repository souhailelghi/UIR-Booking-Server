using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationFeature.Queries.GetAllReservationQuerie
{
    public class GetAllReservationQuery : IRequest<List<Reservation>>
    {
    }
}
