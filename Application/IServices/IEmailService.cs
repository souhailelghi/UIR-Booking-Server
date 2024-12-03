using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    internal interface IEmailService
    {
        Task<List<Email>> GetEmailsListAsync();
        Task<Email> AddEmailAsync(Email email);
        Task DeleteEmailAsync(Guid id);
    }
}
