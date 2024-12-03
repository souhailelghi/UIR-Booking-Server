using Application.IServices;
using Application.IUnitOfWorks;
using AutoMapper;
using Domain.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Event> AddEventAsync(Event eventEntity)
        {
            if (eventEntity == null)
            {
                throw new ArgumentNullException(nameof(eventEntity), "Event entity cannot be null.");
            }

            // Convert the uploaded image (IFormFile) to a byte array if present
            if (eventEntity.ImageUpload != null)
            {
                using (var ms = new MemoryStream())
                {
                    await eventEntity.ImageUpload.CopyToAsync(ms);
                    eventEntity.Image = ms.ToArray(); // Convert to byte[] and store
                }
            }

            eventEntity.Id = Guid.NewGuid(); // Generate a new unique ID

            // Add the event entity to the repository
            await _unitOfWork.EventRepository.AddAsync(eventEntity);

            // Commit changes to the database
            await _unitOfWork.CommitAsync();

            return eventEntity; // Return the saved entity
        }

        public async Task<List<Event>> GetEventsList()
        {
            // Utiliser le UnitOfWork pour récupérer les événements depuis le dépôt
            var events = await _unitOfWork.EventRepository.GetAllAsNoTracking();

            // Retourner la liste des événements
            return events;
        }
    }
}
