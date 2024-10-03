﻿using Application.IRepository.IGenericRepositorys;
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
        Task<List<TimeRange>> GetReservedTimeRangesBySportAndDayAsync(Guid sportId, DayOfWeekEnum day);
        Task<Reservation> GetAsync(Expression<Func<Reservation, bool>> filter);





    }
}
