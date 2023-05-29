using TicketBookingAPI.Models;

namespace TicketBookingAPI.Repository.IRepository
{
    public interface IBookingRepository:IRepository<Booking>
    {
        Task<Booking> UpdateAsync(Booking entity);
    }
}
