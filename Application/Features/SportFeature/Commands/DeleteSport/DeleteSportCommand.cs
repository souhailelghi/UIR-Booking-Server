using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportFeature.Commands.DeleteSport
{
    public class DeleteSportCommand: IRequest<string>
    {
        public Guid Id { get; set; }
        public DeleteSportCommand(Guid id)
        {
            Id = id;
        }
    }
}
