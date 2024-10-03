using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PlanningFeature.Queries.GetTimeRangesBySportAndDayNotExistOnTableReservationQuerie
{

    public class GetTimeRangesBySportAndDayNotExistOnTableReservationQuery : IRequest<List<TimeRange>>
    {
        public Guid SportId { get; }
        public DayOfWeekEnum Day { get; }

        public GetTimeRangesBySportAndDayNotExistOnTableReservationQuery(Guid sportId, DayOfWeekEnum day)
        {
            SportId = sportId;
            Day = day;
        }
    }
}
