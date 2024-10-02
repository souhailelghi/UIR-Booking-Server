using Domain.Entities;
using MediatR;

namespace Application.IServices
{
    public interface IPlanningService
    {
        Task<List<Planning>> GetAllPlanningAsync();
        Task<List<TimeRange>> GetAvailableTimeRangesAsync();
        Task<List<TimeRange>> GetAvailableTimeRangesBySportAsync(Guid sportId);

        Task<Planning> AddPlanningAsync(Planning planning);

    }
}
