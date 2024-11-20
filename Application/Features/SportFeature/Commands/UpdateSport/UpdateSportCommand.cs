using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportFeature.Commands.UpdateSport
{
    public class UpdateSportCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? ReferenceSport { get; set; }
        public int? NbPlayer { get; set; }
        public int? Daysoff { get; set; }
        public string? Conditions { get; set; }
        public string? Description { get; set; }
        public IFormFile? ImageUpload { get; set; } // Ensure this property is nullable for optional images

        // Parameterless constructor
        public UpdateSportCommand()
        {
        }

        // Optional convenience constructor
        public UpdateSportCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }

}
