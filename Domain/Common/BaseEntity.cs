using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; } = false; // Ensure this aligns with the soft delete logic
        public DateTime? DateDeleted { get; set; }
    }
}
