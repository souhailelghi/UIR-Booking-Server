using MediatR;
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
        public UpdateSportCommand(Guid id, string name)
        {
            Id = id;
            Name = name;



        }
    }
}
