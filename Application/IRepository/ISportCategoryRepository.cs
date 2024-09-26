using Application.IRepository.IGenericRepositorys;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
    public interface ISportCategoryRepository : IGenericRepository<SportCategory>
    {
    }
}
