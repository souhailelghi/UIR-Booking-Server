using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.EventFeature.Commands.DeleteEvent
{
    public class DeleteEventCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public DeleteEventCommand(Guid id)
        {
            Id = id;
        }
    }
}
