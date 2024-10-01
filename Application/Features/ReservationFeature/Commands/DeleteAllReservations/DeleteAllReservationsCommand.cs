using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationFeature.Commands.DeleteAllReservations
{
    public class DeleteAllReservationsCommand : IRequest<string>
    {
    }
}
