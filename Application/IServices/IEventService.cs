using Domain.Entities;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IEventService
    {
        Task<List<Event>> GetEventsList();
        Task<Event> AddEventAsync(Event eventEntity);
        Task DeleteEventAsync(Guid id);
        Task<Event> GetEventByIdAsync(Guid id);
        Task UpdateEventAsync(Event Events);
    }
}
