using Event_Booking_API.Data;
using Event_Booking_API.Domain;
using Event_Booking_API.Models;
using Event_Booking_API.Services;
using Event_Booking_API.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Event_Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;   
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("CreateEvent")]
        public ActionResult<Event> CreateEvent(Event newEvent)
        {
            var validator = new EventValidator();
            var validatorResult = validator.Validate(newEvent);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }
            else
            {
                var createdEvent = _eventService.CreateEvent(newEvent);
                return Ok(new { Message = "Event has been successfully created!", Event = createdEvent });
            }
        }

        [Authorize]
        [HttpGet("GetEventByID/{id}")]
        public IActionResult GetEventByID(int id)
        {
            var foundEvent = _eventService.GetEventByID(id);
            if (foundEvent == null)
            {
                return NotFound("There is no event with this ID!");
            }
            return Ok(foundEvent);
        }

        [Authorize]
        [HttpGet("GetAllEvents")]
        public ActionResult<IEnumerable<Event>> GetAllEvents()
        {
            var events = _eventService.GetAllEvents();
            return Ok(events);
        }

        [Authorize]
        [HttpGet("FindEventByAddress")]
        public ActionResult<IEnumerable<Event>> FindEventByAddress([FromBody] Event location)
        {
            if (string.IsNullOrEmpty(location.EventLocation))
            {
                return BadRequest("Address parameter is missing or empty!");
            }
            var filteredEvents = _eventService.FindEventByAddress(location.EventLocation);
            return Ok(filteredEvents);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("UpdateEvent/{id}")]
        public IActionResult UpdateEvent(int id, [FromBody] Event updatedEvent)
        {
            var success = _eventService.UpdateEvent(id, updatedEvent);
            if (!success)
            {
                return NotFound("There is no event to update with this ID!");
            }
            return Ok("Event has been successfully updated!");
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("DeleteEvent/{id}")]
        public IActionResult DeleteEvent(int id)
        {
            var success = _eventService.DeleteEvent(id);
            if (!success)
            {
                return NotFound("There is no event to delete with this ID!");
            }
            return Ok("Event has been successfully deleted!");
        }

    }
}
