using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.EventFeature.Commands.UpdateEvent
{
    public class UpdateEventCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Lien { get; set; }
        public IFormFile? ImageUpload { get; set; }
        public UpdateEventCommand()
        {

        }
        public UpdateEventCommand(Guid id, string title, string description, string lien)
        {
            Id = id;
            Title = title;
            Description = description;
            Lien = lien;





        }
    }
}
