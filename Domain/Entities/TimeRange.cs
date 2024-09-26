namespace Domain.Entities
{
    public class TimeRange
    {
        public Guid Id { get; set; }
        public TimeSpan HoureStart { get; set; }
        public TimeSpan HourEnd { get; set; }
    }
}
