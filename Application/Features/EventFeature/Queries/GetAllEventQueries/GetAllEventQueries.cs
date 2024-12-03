using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.EventFeature.Queries.GetAllEventQueries
{
    public class GetAllEventQueries:IRequest<List<Event>>
    {
    }
}
