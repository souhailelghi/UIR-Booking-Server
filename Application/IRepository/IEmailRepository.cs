using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
    internal interface IEmailRepository: IGenericRepositorys<Email>
    {
        Task<Email> GetAsync(Expression<Func<Email, bool>> filter);
    }
}
