using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class BlackList
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Reservation")]
        public Guid ReservationId { get; set; }
        public DateTime DateCreation { get; set; }
    }
}
