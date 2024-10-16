using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Sport
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("SportCategory")]
        public Guid CategorieId { get; set; }
        public int? ReferenceSport { get; set; }
        public int? NbPlayer { get; set; }
        public int? Daysoff { get; set; }
        public string? Conditions { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        // For uploading the image
        [NotMapped]  // This means it won't be mapped to the database
        public IFormFile ImageUpload { get; set; }
    
        public byte[]? Image { get; set; }

        public DateTime? DateCreation { get; set; }
        public DateTime? DateModification { get; set; }
        // Navigation Property
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
