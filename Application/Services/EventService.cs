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

        public async Task<int> GetTotalEventsList()
        {
            // Utiliser le UnitOfWork pour récupérer les événements depuis le dépôt
            var events = await _unitOfWork.EventRepository.GetAllAsNoTracking();

            // Retourner la liste des événements
            return events.Count();
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

        // get by id , delete , update : 

        public async Task DeleteEventAsync(Guid id)
        {
            Event Event = await _unitOfWork.EventRepository.GetAsNoTracking(u => u.Id == id);
            if (Event == null)
            {
            throw new ArgumentException("event not found.");
        }

            try
            {

            await _unitOfWork.EventRepository.RemoveAsync(Event);
        await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
}
        }



        public async Task<Event> GetEventByIdAsync(Guid id)
        {
            Event Events = await _unitOfWork.EventRepository.GetAsNoTracking(u => u.Id == id);

            return Events;
        }

        public async Task UpdateEventAsync(Event Events)
        {




            if (Events == null)
            {
                throw new ArgumentNullException(nameof(Events));
            }

        // Use GetAsync with a filter expression to find the Event by Id
        Event existingEvent = await _unitOfWork.EventRepository.GetAsNoTracking(d => d.Id == Events.Id);
            if (existingEvent == null)
            {
                throw new ArgumentException("Event not found.");
            }

            existingEvent.Title = Events.Title;
            existingEvent.Description = Events.Description;
            existingEvent.lien = Events.lien;


            // Update image if provided
            if (Events.Image != null)
            {
                existingEvent.Image = Events.Image;
            }



            try
            {
                await _unitOfWork.EventRepository.UpdateAsync(existingEvent);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {

                throw new ArgumentException($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }



    }
}
