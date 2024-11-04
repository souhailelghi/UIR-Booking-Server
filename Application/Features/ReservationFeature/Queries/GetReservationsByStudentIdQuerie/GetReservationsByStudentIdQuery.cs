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
        public Guid StudentId { get; set; }
        public GetReservationsByStudentIdQuery(Guid studentId)
        {
            StudentId = studentId;
        }
    }
}
