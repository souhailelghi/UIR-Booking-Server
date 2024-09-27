using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportCategoryFeature.Commands.DeleteSportCategory
{
    public class DeleteSportCategoryCommand  : IRequest<string>
    {
        public Guid Id { get; set; }
        public DeleteSportCategoryCommand(Guid id)
        {
            Id = id;
        }
    }
}
