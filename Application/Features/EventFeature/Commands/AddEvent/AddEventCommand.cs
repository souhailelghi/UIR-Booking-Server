using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.EventFeature.Commands.AddEvent
{
    public class AddEventCommand : IRequest<Event>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile ImageUpload { get; set; }
        public string? lien { get; set; } 
    }
}
