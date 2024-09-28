using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class TimeRange
    {
        [Key]
        public Guid Id { get; set; }
        public TimeSpan HourStart { get; set; }
        public TimeSpan HourEnd { get; set; }
    }
}
