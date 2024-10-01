using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        
        public List<Guid> StudentIdList { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }              
    }
}
   