using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationFeature.Queries.CountTimeForReservation
{
    public class CountTimeForReservationQuery : IRequest<string>
    {
        public string CodeUIR { get; set; }
        public List<string> CodeUIRList { get; set; }
        public int Reference { get; set; }
     
    }
}
