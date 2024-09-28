using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SystemeInfo
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Home { get; set; }  
        public string AboutUs { get; set; }
        public byte[] Logo { get; set; }
        public byte[] BackgroundImage { get; set; }

    }
}
