using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Planning
    {
        public Guid Id { get; set; }
        public Guid SportId { get; set; } // SportId
        public string Day { get; set; }
        public ICollection<TimeRange> TimeRanges { get; set; } = new List<TimeRange>();
        public DateTime DateCreation { get; set; }
        
    }
}
