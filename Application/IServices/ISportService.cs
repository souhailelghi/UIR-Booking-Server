using Domain.Dtos;
using Domain.Entities;

namespace Application.IServices
{
    public interface ISportService 
    {
        Task<List<Sport>> GetSportsListAsync();
        Task<Sport> GetSportByIdAsync(Guid id);
        Task<Sport> AddSportAsync(Sport sport);
        Task UpdateSportAsync(Sport sport);
        Task DeleteSportAsync(Guid id);
        Task<List<Sport>> GetAllSportByCategorieIdAsync(Guid categorieId);
        Task<int> GetTotalCourtsAsync();
    }
}
