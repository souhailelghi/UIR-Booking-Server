using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.EmailFeature.Commands.AddEmail
{
    internal class AddEmailCommand: IRequest<Email>
    {
        public String Name { get; set; }
        public string email { get; set; }

        public string Message { get; set; }

    }
}
