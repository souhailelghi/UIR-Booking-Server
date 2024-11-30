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
       


        Task<List<Reservation>> GetReservationsForDateAsync( string codeUIR, List<string> teamMembersIds);
        Task RemoveAllAsync();
     
        Task<Reservation> GetAsync(Expression<Func<Reservation, bool>> filter);

        Task<List<Reservation>> GetReservationsBySportIdAsync(Guid sportId);

        //Task<List<Reservation>> GetReservationsByReferenceSportForTeamAsync(List<string> teamMemberIds, int referenceSport);
        Task<List<Reservation>> GetReservationsByReferenceSportAsync(string codeUIR, int referenceSport);
        Task<List<Reservation>> GetReservationsByStudentIdAsync(string codeUIR);
        Task<List<Reservation>> GetReservationsBysportCategoryIdAsync(Guid sportCategoryId);

        Task<List<Reservation>> GetReservationsByCategoryAndStudentIdAsync(Guid sportCategoryId, string codeUIR);
        Task<List<Reservation>> GetReservationsDateAsync(string codeUIR, List<string> teamMembersIds);

        Task<List<Reservation>> GetReservationsByCodeUIRsAsync(List<string> codeUIRs);

        //new
        Task<IEnumerable<Reservation>> GetReservationsByReferenceSportWithCodeUIRAsync(int referenceSport, DateTime delayTime);
        Task<IEnumerable<Reservation>> GetReservationsForSportAsync(Guid sportId, DateTime delayTime);


        Task<List<Reservation>> GetReservationsByCodeUIRsAsync(List<string> codeUIRs, int referenceSport);

    }
}
