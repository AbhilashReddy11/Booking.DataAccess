
using AutoMapper;
using TicketBookingAPI.Models;
using TicketBookingAPI.Models.DTO;

namespace TicketBookingAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()

        {
           
            CreateMap<Event, EventCreateDTO>().ReverseMap();
           

           
            CreateMap<Booking, BookingCreateDTO>().ReverseMap();
            

            
           
           

          
          

        }
    }
}

