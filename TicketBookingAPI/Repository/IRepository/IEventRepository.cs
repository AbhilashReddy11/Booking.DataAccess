using TicketBookingAPI.Models;

namespace TicketBookingAPI.Repository.IRepository
{
    public interface IEventRepository:IRepository<Event>
    {
        Task<Event> UpdateAsync(Event entity);
    }
}
