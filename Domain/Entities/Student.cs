using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Student 
    {
        [Key]
        public Guid Id { get; set; }
  
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [Required]
        public Guid UserId { get; set; }
        public string CodeUIR { get; set; }
    }
}
                 