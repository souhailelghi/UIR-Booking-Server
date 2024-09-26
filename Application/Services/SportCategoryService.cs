using Application.IServices;
using Application.IUnitOfWorks;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SportCategoryService : ISportCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SportCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<SportCategory>> GetSportCategorysList()
        {
            List<SportCategory> sportCategorysList = await _unitOfWork.SportCategoryRepository.GetAllAsNoTracking();
            //if (sportCategorysList.IsNullOrEmpty())
            //{
            //    throw new ArgumentException("No sportCategorysList  found.");
            //}
            return sportCategorysList;
        }
    }
}
