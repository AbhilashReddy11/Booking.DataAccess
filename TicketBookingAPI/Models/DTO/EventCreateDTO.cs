namespace TicketBookingAPI.Models.DTO
{
    public class EventCreateDTO
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int AvailableSeats { get; set; }
    }
}
