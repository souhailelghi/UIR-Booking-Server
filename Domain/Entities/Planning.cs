using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Planning
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Sport")]
        public Guid SportId { get; set; } 
        public DayOfWeekEnum Day { get; set; }
        public ICollection<TimeRange> TimeRanges { get; set; } = new List<TimeRange>();
        public DateTime DateCreation { get; set; }
        
    }
}
