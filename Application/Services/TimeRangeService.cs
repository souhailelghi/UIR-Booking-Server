using Application.IServices;
using Application.IUnitOfWorks;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TimeRangeService : ITimeRangeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TimeRangeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task DeleteTimeRangeAsync(Guid id)
        {
            TimeRange sport = await _unitOfWork.TimeRangeRepository.GetAsNoTracking(u => u.Id == id);
            if (sport == null)
            {
                throw new ArgumentException("Sport not found.");
            }

            try
            {

                await _unitOfWork.TimeRangeRepository.RemoveAsync(sport);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }
    }
}
