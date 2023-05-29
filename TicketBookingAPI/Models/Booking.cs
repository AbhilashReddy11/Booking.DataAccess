using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingAPI.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event events  { get; set; }
        public string CustomerName { get; set; }
        public int NumberOfTickets { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
