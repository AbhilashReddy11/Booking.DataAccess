namespace TicketBookingAPI.Models.DTO
{
    public class LoginResponseDTO
    {
        public LocalUser User { get; set; }
        public string Role { get; set; }
        public string Token { get; internal set; }
    }
}
