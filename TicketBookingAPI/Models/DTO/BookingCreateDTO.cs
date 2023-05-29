namespace TicketBookingAPI.Models.DTO
{
    public class BookingCreateDTO
    {
        public int EventId { get; set; }
       
        public string CustomerName { get; set; }

        public int NumberOfTickets { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
