using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Student : User
    {

  
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public Guid UserId { get; set; }
        public string CodeUIR { get; set; }
    }
}
                 