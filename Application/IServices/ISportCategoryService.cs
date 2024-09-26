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
    }
}
