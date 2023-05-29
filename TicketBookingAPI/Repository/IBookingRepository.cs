using TicketBookingAPI.Data;
using TicketBookingAPI.Models;
using TicketBookingAPI.Repository.IRepository;

namespace TicketBookingAPI.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {

        private readonly ApplicationDbContext _db;
        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Booking> UpdateAsync(Booking entity)
        {

            _db.Bookings.Update(entity);
            await _db.SaveChangesAsync();
            return entity;

        }
    }
}
