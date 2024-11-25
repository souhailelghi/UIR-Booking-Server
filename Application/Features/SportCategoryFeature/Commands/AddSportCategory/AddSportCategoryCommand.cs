using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportCategoryFeature.Commands.AddSportCategory
{
    public class AddSportCategoryCommand  : IRequest<SportCategory>
    {
        public string Name { get; set; }
        public IFormFile ImageUpload { get; set; }

    }
}
