using Application.IRepository.IGenericRepositorys;
using Domain.Entities;
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
        Task<List<Planning>> GetPlanningsWithTimeRangesAsync();

        Task<List<TimeRange>> GetAvailableTimeRangesAsync();

        Task<List<TimeRange>> GetAvailableTimeRangesBySportAsync(Guid sportId);
        Task AddAsync(Planning planning);
        Task<Planning> FindAsync(Expression<Func<Planning, bool>> predicate);
    }
}
