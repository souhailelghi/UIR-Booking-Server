using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
       
        public string Image { get; set; }
      
        public DateTime? DateCreation { get; set; }
        public DateTime? DateModification { get; set; }
    }
}
