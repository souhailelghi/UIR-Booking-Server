using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        // For uploading the image
        [NotMapped]  // This means it won't be mapped to the database
        public IFormFile ImageUpload { get; set; }

        public byte[] Image { get; set; }
        
        public string lien { get; set; }
    }
}
