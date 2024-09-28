using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Student")]
        public Guid StudentId { get; set; }
        [ForeignKey("Sport")]
        public Guid SportId { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan HourStart { get; set; }
        public TimeSpan HourEnd { get; set; }

        //[ForeignKey("Student")]
        //public List<Guid> StudentIdList { get; set; }
        public List<Guid> StudentIdList { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }              
    }
}
   