using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationFeature.Queries.CheckReservationAccessRequestQuerie
{
    public class CheckReservationAccessRequest : IRequest<bool>
    {
        public string CodeUIR { get; set; }
        public List<string> CodeUIRList { get; set; }
        public Guid SportId { get; set; }
    }

}
