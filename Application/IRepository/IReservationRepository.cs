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
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        Task<List<Reservation>> GetReservationsForDateAsync( Guid studentId, List<Guid> teamMembersIds);
        Task RemoveAllAsync();
     
        Task<Reservation> GetAsync(Expression<Func<Reservation, bool>> filter);

        Task<List<Reservation>> GetReservationsBySportIdAsync(Guid sportId);

        Task<List<Reservation>> GetReservationsByReferenceSportForTeamAsync(List<Guid> teamMemberIds, int referenceSport);
        Task<List<Reservation>> GetReservationsByReferenceSportAsync(Guid studentId, int referenceSport);
        Task<List<Reservation>> GetReservationsByStudentIdAsync(Guid studentId);
        Task<List<Reservation>> GetReservationsBysportCategoryIdAsync(Guid sportCategoryId);

        Task<List<Reservation>> GetReservationsByCategoryAndStudentIdAsync(Guid sportCategoryId, Guid studentId);





    }
}
