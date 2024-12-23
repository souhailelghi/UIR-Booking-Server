﻿using Application.IServices;
using Application.IUnitOfWorks;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class SportService : ISportService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task UpdateSportAsync(Sport sport)
        {
            if (sport == null)
            {
                throw new ArgumentNullException(nameof(sport));
            }

            // Use GetAsync with a filter expression to find the sport by Id
            Sport existingSport = await _unitOfWork.SportRepository.GetAsync(s => s.Id == sport.Id);
            if (existingSport == null)
            {
                throw new ArgumentException("Sport not found.");
            }

            existingSport.Name = sport.Name;
            existingSport.ReferenceSport = sport.ReferenceSport;
            existingSport.NbPlayer = sport.NbPlayer;
            existingSport.Daysoff = sport.Daysoff;
            existingSport.Conditions = sport.Conditions;
            existingSport.Description = sport.Description;

            // Update image if provided
            if (sport.Image != null)
            {
                existingSport.Image = sport.Image;
            }

            // Save the updated sport entity
            await _unitOfWork.SportRepository.UpdateAsync(existingSport);
            await _unitOfWork.CommitAsync();
        }


        public async Task<Sport> AddSportAsync(Sport sport)
        {
            byte[] imageData = null;

            // Convert the uploaded image (IFormFile) to a byte array if present
            if (sport.ImageUpload != null)
            {
                using (var ms = new MemoryStream())
                {
                    await sport.ImageUpload.CopyToAsync(ms);
                    imageData = ms.ToArray();  // Convert to byte[]
                }
            }

            // Map the sport command to the sport entity
            var sportEntity = _mapper.Map<Sport>(sport);
            sportEntity.Id = Guid.NewGuid(); // Generate a new unique ID
            sportEntity.Image = imageData; // Store the byte array image
            sportEntity.DateCreation = DateTime.UtcNow;

            // Save the new sport entity using UnitOfWork
            await _unitOfWork.SportRepository.CreateAsync(sportEntity);
            await _unitOfWork.CommitAsync();

            return sportEntity;
        }


      
        public async Task DeleteSportAsync(Guid id)
        {
            Sport sport = await _unitOfWork.SportRepository.GetAsNoTracking(u => u.Id == id);
            if (sport == null)
            {
                throw new ArgumentException("Sport not found.");
            }

            try
            {

                await _unitOfWork.SportRepository.RemoveAsync(sport);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        public async Task<Sport> GetSportByIdAsync(Guid id)
        {
            Sport sport = await _unitOfWork.SportRepository.GetAsNoTracking(u => u.Id == id);

            return sport;
        }

        public async Task<List<Sport>> GetSportsListAsync()
        {
            List<Sport> sportsList = await _unitOfWork.SportRepository.GetAllAsNoTracking();
            return sportsList;
        }

    


        public async Task<List<Sport>> GetAllSportByCategorieIdAsync(Guid categorieId)
        {
            // Fetch the sports from the repository based on the CategorieId
            List<Sport> sportsList = await _unitOfWork.SportRepository.GetAllAsNoTracking(s => s.CategorieId == categorieId);

            return sportsList;
        }

    
        public async Task<int> GetTotalCourtsAsync()
        {
            List<Sport> ListCourt = await _unitOfWork.SportRepository.GetAllAsNoTracking();
            return ListCourt.Count();
        }



        public async Task<int> FetchTotalCourtsBysportIdAsync(Guid categorieId)
        {
            // Fetch the sports from the repository based on the CategorieId
            List<Sport> sportsList = await _unitOfWork.SportRepository.GetAllAsNoTracking(s => s.CategorieId == categorieId);

            return sportsList.Count();
        }

    }
}
