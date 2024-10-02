using Domain.Enums;

namespace Domain.Dtos
{
    public class PlanningDto
    {
        public Guid SportId { get; set; }
        public DayOfWeekEnum Day { get; set; }
        public List<TimeRangeDto> TimeRanges { get; set; } = new List<TimeRangeDto>();
        public DateTime DateCreation { get; set; }
    }
}

