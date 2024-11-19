using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationFeature.Queries.GetReservationsByStudentIdQuerie
{
    public class GetReservationsByStudentIdQuery : IRequest<List<Reservation>>
    {
        public string CodeUIR { get; set; }
        public GetReservationsByStudentIdQuery(string codeUIR)
        {
            CodeUIR = codeUIR;
        }
    }
}
