using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PlanningFeature.Commands.DeletePlanning
{
    public class DeletePlanningCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public DeletePlanningCommand(Guid id)
        {
            Id = id;
        }
    }
}
