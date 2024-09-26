using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid SportId { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan HoureStart { get; set; }
        public TimeSpan HoureEnd { get; set; }
        public Guid UserIdList { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }              
    }
}
   