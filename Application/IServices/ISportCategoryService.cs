using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface ISportCategoryService
    {
        Task<List<SportCategory>> GetSportCategorysList();
        Task<SportCategory> GetSportCategoryByIdAsync(Guid id);
        Task<SportCategory> AddSportCategoryAsync(SportCategory sportCategory);
        Task UpdateSportCategoryAsync(SportCategory sportCategory);
        Task DeleteSportCategoryAsync(Guid id);
        Task<int> GetTotalSportCategorysAsync();


    }
}
