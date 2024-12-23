﻿using Domain.Entities;
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
        Task<List<TimeRange>> GetTimeRangesBySportAndDayAsync(Guid sportId);
        Task<List<TimeRange>> GetTimeRangesByReferenceSportAndDayAsync(int referenceSport, DayOfWeekEnum day);

        Task UpdatePlanningAsync(Planning planning);
        Task<Planning> GetPlanningByIdAsync(Guid id);
        Task<List<Planning>> GetAllPlanningsBySportId(Guid sportId);

        Task DeletePlanningAsync(Guid id);
    }
}
