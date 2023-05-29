using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TicketBookingAPI.Data;
using TicketBookingAPI.Models.DTO;
using TicketBookingAPI.Models;
using TicketBookingAPI.Repository.IRepository;

namespace TicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private readonly IBookingRepository _dbBooking;
        private readonly IEventRepository _dbEvent;
        private readonly IMapper _mapper;
        protected APIResponse _response;



        public BookingController(IBookingRepository dbbooking, IMapper mapper, ApplicationDbContext db, IEventRepository dbEvent)
        {
            _dbBooking = dbbooking;
            _mapper = mapper;
            _response = new();
            _dbEvent = dbEvent;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()

        {
            try
            {
                IEnumerable<Booking> bookingList = await _dbBooking.GetAllAsync( includeProperties: "events");
                _response.Result = bookingList;


                _response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);


        }
        [HttpGet("{id:int}", Name = "GetBooking")]

        public async Task<ActionResult<APIResponse>> GetBooking(int id)
        {
            try
            {

                if (id == 0)
                {

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var booking = await _dbBooking.GetAsync(u => u.EventId == id,includeProperties: "events");
                if (booking == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);


                }
                _response.Result = booking;

                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }
        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateBooking([FromBody] BookingCreateDTO createDTO)
        {
            try
            {
               

                if (createDTO == null)
                {
                    return BadRequest();
                }
                if (await _dbEvent.GetAsync(u => u.EventId == createDTO.EventId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "EventId is invalid");
                    return BadRequest(ModelState);
                }

                Booking booking = _mapper.Map<Booking>(createDTO);
                await _dbBooking.CreateAsync(booking);


                _response.Result = _mapper.Map<Booking>(booking);
                _response.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("GetBooking", new { id = booking.EventId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult<APIResponse>> DeleteBooking(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var booking = await _dbBooking.GetAsync(u => u.BookingId == id);
                if (booking == null)
                {
                    return NotFound();
                }

                await _dbBooking.RemoveAsync(booking);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<APIResponse>> UpdateEvent(int id, [FromBody] Booking updateDTO)
        {
            try
            {

                if (updateDTO == null || id != updateDTO.EventId)
                {
                    return BadRequest();
                }
                if (await _dbEvent.GetAsync(u => u.EventId == updateDTO.EventId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "EventId is invalid");
                    return BadRequest(ModelState);
                }

                Booking model = updateDTO;


                await _dbBooking.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }
    }
}
