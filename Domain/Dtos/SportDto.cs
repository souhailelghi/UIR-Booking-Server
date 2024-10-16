using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class SportDto
    {
        public Guid CategorieId { get; set; }
        public int ReferenceSport { get; set; }
        public int NbPlayer { get; set; }
        public int Daysoff { get; set; }
        public string Conditions { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }

        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }
    }
}
