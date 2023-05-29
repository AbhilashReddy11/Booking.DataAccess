using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using TicketBookingAPI.Data;
using TicketBookingAPI.Models;
using TicketBookingAPI.Models.DTO;
using TicketBookingAPI.Repository.IRepository;

namespace TicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly IEventRepository _dbEvent;
        private readonly IMapper _mapper;
        protected APIResponse _response;



        public EventController(IEventRepository dbevent, IMapper mapper, ApplicationDbContext db)
        {
            _dbEvent = dbevent;
            _mapper = mapper;
            _response = new();

        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()

        {
            try
            {
                IEnumerable<Event> eventList = await _dbEvent.GetAllAsync();
                _response.Result = eventList;


                _response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);


        }
        [HttpGet("{id:int}", Name = "GetEvent")]

        public async Task<ActionResult<APIResponse>> GetEvent(int id)
        {
            try
            {

                if (id == 0)
                {

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var events = await _dbEvent.GetAsync(u => u.EventId == id);
                if (events == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);


                }
                _response.Result = events;

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
        public async Task<ActionResult<APIResponse>> CreateEvent([FromBody] EventCreateDTO createDTO)
        {
            try
            {
                if (await _dbEvent.GetAsync(u => u.EventName.ToLower() == createDTO.EventName.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Event already exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest();
                }

                Event events = _mapper.Map<Event>(createDTO);
                await _dbEvent.CreateAsync(events);


                _response.Result = _mapper.Map<Event>(events);
                _response.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("GetEvent", new { id = events.EventId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult<APIResponse>> DeleteEvent(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var events = await _dbEvent.GetAsync(u => u.EventId == id);
                if (events == null)
                {
                    return NotFound();
                }

                await _dbEvent.RemoveAsync(events);
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
        public async Task<ActionResult<APIResponse>> UpdateEvent(int id, [FromBody] Event updateDTO)
        {
            try
            {

                if (updateDTO == null || id != updateDTO.EventId)
                {
                    return BadRequest();
                }


                Event model = updateDTO;


                await _dbEvent.UpdateAsync(model);
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






