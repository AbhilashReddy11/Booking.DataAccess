using System.ComponentModel.DataAnnotations;

namespace TicketBookingAPI.Models.DTO
{
    public class RegistrationDTO
    {
        public string UserName { get; set; }
      
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
