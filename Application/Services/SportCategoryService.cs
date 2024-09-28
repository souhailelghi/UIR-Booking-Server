using Application.IServices;
using Application.IUnitOfWorks;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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

        public async Task<SportCategory> AddSportCategoryAsync(SportCategory sportCategory)
        {
            
            sportCategory.Id = Guid.NewGuid();
            await _unitOfWork.SportCategoryRepository.CreateAsync(sportCategory);
            await _unitOfWork.CommitAsync();
            return sportCategory;
        }

        public async Task DeleteSportCategoryAsync(Guid id)
        {
            SportCategory SportCategory = await _unitOfWork.SportCategoryRepository.GetAsNoTracking(u => u.Id == id);
            if (SportCategory == null)
            {
                throw new ArgumentException("SportCategory not found.");
            }

            try
            {

                await _unitOfWork.SportCategoryRepository.RemoveAsync(SportCategory);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

   

        public async Task<SportCategory> GetSportCategoryByIdAsync(Guid id)
        {
            SportCategory sportCategory = await _unitOfWork.SportCategoryRepository.GetAsNoTracking(u => u.Id == id);
           
            return sportCategory;
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

        public async Task UpdateSportCategoryAsync(SportCategory sportCategory)
        {
            if (sportCategory == null)
            {
                throw new ArgumentNullException(nameof(sportCategory));
            }

            SportCategory existingsportCategory = await _unitOfWork.SportCategoryRepository.GetAsNoTracking(
                d => d.Id == sportCategory.Id);
            if (existingsportCategory == null)
            {
                throw new ArgumentException("sportCategory not found.");
            }

            existingsportCategory.Name = sportCategory.Name;
           

            try
            {
                await _unitOfWork.SportCategoryRepository.UpdateAsync(existingsportCategory);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {

                throw new ArgumentException($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }
    }
}
