using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Student : User
    {
        


        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string CodeUIR { get; set; }
        //public bool IsDeleted { get; set; } = false;
    }
}
                 