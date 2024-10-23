using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PlanningFeature.Commands.UpdatePlanning
{
    public class UpdatePlanningCommand:IRequest<string>
    {
        public Guid Id { get; set; }
        public DayOfWeekEnum Day { get; set; }
        public List<TimeRange> TimeRanges { get; set; }  // Added TimeRanges property
        public UpdatePlanningCommand(Guid id , DayOfWeekEnum day, List<TimeRange> timeRanges)
        {
            Id = id;
            Day = day;
            TimeRanges = timeRanges;

        }
    }
    //public class TimeRangeDto  // Simple DTO for TimeRange
    //{
    //    public TimeSpan Start { get; set; }
    //    public TimeSpan End { get; set; }
    //}
}
