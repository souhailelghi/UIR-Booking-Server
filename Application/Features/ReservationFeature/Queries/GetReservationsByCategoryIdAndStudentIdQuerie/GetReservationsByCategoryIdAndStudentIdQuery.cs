using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationFeature.Queries.GetReservationsByCategoryIdAndStudentIdQuerie
{
    public class GetReservationsByCategoryIdAndStudentIdQuery : IRequest<List<Reservation>>
    {
        public Guid SportCategoryId { get; }
        public Guid StudentId { get; }

        public GetReservationsByCategoryIdAndStudentIdQuery(Guid sportCategoryId, Guid studentId)
        {
            SportCategoryId = sportCategoryId;
            StudentId = studentId;
        }
    }
}
