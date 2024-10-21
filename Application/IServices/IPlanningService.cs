using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.IServices
{
    public interface IPlanningService
    {
        Task<List<Planning>> GetAllPlanningAsync();
        Task<List<TimeRange>> GetAvailableTimeRangesAsync();
        Task<List<TimeRange>> GetTimeRangesBySportAsync(Guid sportId);

        Task<Planning> AddPlanningAsync(Planning planning);
        Task<List<TimeRange>> GetTimeRangesBySportAndDayAsync(Guid sportId, DayOfWeekEnum day);
        Task<List<TimeRange>> GetTimeRangesByReferenceSportAndDayAsync(int referenceSport, DayOfWeekEnum day);






    }
}
