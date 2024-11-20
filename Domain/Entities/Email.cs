using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Email
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string email { get; set; }
        public string Message { get; set; }


    }
}
