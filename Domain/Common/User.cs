using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

                                         
    }
}
