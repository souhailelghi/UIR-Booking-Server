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
            byte[] imageData = null;

            // Convert the uploaded image (IFormFile) to a byte array if present
            if (sportCategory.ImageUpload != null)
            {
                using (var ms = new MemoryStream())
                {
                    await sportCategory.ImageUpload.CopyToAsync(ms);
                    imageData = ms.ToArray();  // Convert to byte[]
                }
            }


            // Map the sport command to the sport entity
            var sportEntity = _mapper.Map<SportCategory>(sportCategory);
            sportEntity.Id = Guid.NewGuid(); // Generate a new unique ID
            sportEntity.Image = imageData; // Store the byte array image
            sportEntity.DateCreation = DateTime.UtcNow;

            // Save the new sport entity using UnitOfWork
            await _unitOfWork.SportCategoryRepository.CreateAsync(sportEntity);
            await _unitOfWork.CommitAsync();

            return sportEntity;
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

        public async Task UpdateSportCategoryAsync(SportCategory sport)
        {

          


            if (sport == null)
            {
                throw new ArgumentNullException(nameof(sport));
            }

            // Use GetAsync with a filter expression to find the sport by Id
             SportCategory existingSport = await _unitOfWork.SportCategoryRepository.GetAsNoTracking( d => d.Id == sport.Id);
            if (existingSport == null)
            {
                throw new ArgumentException("Sport not found.");
            }

            existingSport.Name = sport.Name;
          

            // Update image if provided
            if (sport.Image != null)
            {
                existingSport.Image = sport.Image;
            }

         

            try
            {
                await _unitOfWork.SportCategoryRepository.UpdateAsync(existingSport);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {

                throw new ArgumentException($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
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
