using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BlackList
    {
        public Guid Id { get; set; }
        public Guid ReservationId { get; set; }
        public DateTime DateCreation { get; set; }
    }
}
