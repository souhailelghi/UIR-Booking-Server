﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SportCategory
    {
        [Key]
        public Guid Id { get; set; }
        public DateOnly MYDATE { get; set; }
        public string Name { get; set; }
        public byte[]? Image { get; set; }
        public string? Description { get; set; }
        public DateTime? DateCreation { get; set; }
        public DateTime? DateModification { get; set; }
    }
}
