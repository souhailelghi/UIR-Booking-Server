﻿using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportCategoryFeature.Commands.UpdateSportCategory
{
    public class UpdateSportCategoryCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IFormFile? ImageUpload { get; set; }
        public UpdateSportCategoryCommand()
        {
            
        }
        public UpdateSportCategoryCommand(Guid id , string name  )
        {
            Id = id;
            Name=name;
            
            
            
        }
    }
}
