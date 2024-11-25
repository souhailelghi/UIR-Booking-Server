using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    public class SportCategory
    {
        [Key]
        public Guid Id { get; set; }
        public DateOnly MYDATE { get; set; }
        public string Name { get; set; }


        // For uploading the image
        [NotMapped]  // This means it won't be mapped to the database
        public IFormFile ImageUpload { get; set; }
        public byte[]? Image { get; set; }
        public string? Description { get; set; }
        public DateTime? DateCreation { get; set; }
        public DateTime? DateModification { get; set; }
    }
}
