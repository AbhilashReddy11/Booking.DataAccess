

using TicketBookingAPI.Models;
using TicketBookingAPI.Models.DTO;

namespace TicketBookingAPI.Repository.IRepository
{
    public interface IUserRepository : IRepository<LocalUser>
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<LocalUser> Register(RegistrationDTO registrationDTO);
         Task<LocalUser>UpdateAsync(LocalUser entity);
        

    }
}
