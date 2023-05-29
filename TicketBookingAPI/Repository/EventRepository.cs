using TicketBookingAPI.Data;
using TicketBookingAPI.Models;
using TicketBookingAPI.Repository.IRepository;

namespace TicketBookingAPI.Repository
{
    public class EventRepository : Repository<Event>, IEventRepository
    {

        private readonly ApplicationDbContext _db;
        public EventRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Event> UpdateAsync(Event entity)
        {

            _db.Events.Update(entity);
            await _db.SaveChangesAsync();
            return entity;

        }
    }
}
