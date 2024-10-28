using Application.IRepository.IGenericRepositorys;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
    public interface IPlanningRepository: IGenericRepository<Planning>
    {
        Task<List<Planning>> GetAllAsync(Expression<Func<Planning, bool>> predicate = null);

        Task<List<TimeRange>> GetAvailableTimeRangesAsync();

        Task<List<TimeRange>> GetTimeRangesBySportAsync(Guid sportId);
        Task AddAsync(Planning planning);
        Task<Planning> FindAsync(Expression<Func<Planning, bool>> predicate);
        Task<List<Planning>> GetPlanningsBySportAndDayAsync(Guid sportId, DayOfWeekEnum day);
        Task<List<TimeRange>> GetTimeRangesBySportAndDayNotExistOnTableReservationAsync(Guid sportId, DayOfWeekEnum day);

    }
}
