using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.IServices
{
    public interface IPlanningService
    {
        Task<List<Planning>> GetAllPlanningAsync();
        Task<List<TimeRange>> GetAvailableTimeRangesAsync();
        Task<List<TimeRange>> GetAvailableTimeRangesBySportAsync(Guid sportId);

        Task<Planning> AddPlanningAsync(Planning planning);
        Task<List<TimeRange>> GetAvailableTimeRangesBySportAndDayAsync(Guid sportId, DayOfWeekEnum day);

    }
}
