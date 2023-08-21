using Event_Booking_API.Data;
using Event_Booking_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Event_Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly EventAppContext _appContext;
        public BookingController(EventAppContext appContext)
        {
            _appContext = appContext;
        }

        [Authorize]
        [HttpPost("EventBooking{eventId}")]
        public ActionResult<EventBookingResponse> EventBooking(Booking booking)
        {
            var eventId = booking.EventId;

            var existingEvent = _appContext.Events.FirstOrDefault(x => x.Id == eventId);

            if (existingEvent == null)
            {
                return BadRequest("There is no event with this ID to book!");
            }
            else
            {
                var userId = int.Parse(User.Identity.Name);
                booking.UserId = userId;

                booking.BookingDate = DateTime.Now;

                _appContext.Bookings.Add(booking);
                _appContext.SaveChanges();
                return Ok(new EventBookingResponse { Message = "You have successfully booked the event!" });
            }
        }


        [Authorize]
        [HttpGet("GetOwnBookingList")]
        public ActionResult<IEnumerable<Booking>> GetOwnBookingList()
        {
            var userId = int.Parse(User.Identity.Name);

            var bookings = _appContext.Bookings
                .Where(booking => booking.UserId == userId)
                .ToList();

            if (bookings.Count == 0)
            {
                return NotFound("There is no any Event booked yet!");
            }
            else
            {
                return Ok(bookings);
            }
        }


    }

    public class EventBookingResponse
    {
        public string Message { get; set; }
    }
}
