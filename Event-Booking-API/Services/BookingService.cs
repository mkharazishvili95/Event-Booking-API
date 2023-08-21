using System;
using System.Collections.Generic;
using System.Linq;
using Event_Booking_API.Data;
using Event_Booking_API.Models;

namespace Event_Booking_API.Services
{
    public interface IBookingService
    {
        bool BookEvent(Booking booking);
        IEnumerable<Booking> GetOwnBookingList();
    }

    public class BookingService : IBookingService
    {
        private readonly EventAppContext _appContext;

        public BookingService(EventAppContext appContext)
        {
            _appContext = appContext;
        }

        public bool BookEvent(Booking booking)
        {
            var eventId = booking.EventId;
            var existingEvent = _appContext.Events.FirstOrDefault(x => x.Id == eventId);

            if (existingEvent == null)
            {
                return false;
            }

            _appContext.Bookings.Add(booking);
            _appContext.SaveChanges();
            return true;
        }

        public IEnumerable<Booking> GetOwnBookingList()
        {
            var bookings = _appContext.Bookings.ToList();
            {
                return bookings;
            }
        }
    }
}
