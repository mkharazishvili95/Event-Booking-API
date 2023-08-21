using System;
using System.Collections.Generic;
using System.Linq;
using Event_Booking_API.Data;
using Event_Booking_API.Models;
using Event_Booking_API.Validation;

namespace Event_Booking_API.Services
{
    public interface IEventService
    {
        Event CreateEvent(Event newEvent);
        Event GetEventByID(int id);
        IEnumerable<Event> GetAllEvents();
        IEnumerable<Event> FindEventByAddress(string address);
        bool UpdateEvent(int id, Event updatedEvent);
        bool DeleteEvent(int id);
    }

    public class EventService : IEventService
    {
        private readonly EventAppContext _appContext;

        public EventService(EventAppContext appContext)
        {
            _appContext = appContext;
        }

        public Event CreateEvent(Event newEvent)
        {
            var validator = new EventValidator();
            var validatorResult = validator.Validate(newEvent);

            if (!validatorResult.IsValid)
            {
                throw new ArgumentException("Invalid event data.");
            }

            _appContext.Events.Add(newEvent);
            _appContext.SaveChanges();
            return newEvent;
        }

        public Event GetEventByID(int id)
        {
            var newEvent = _appContext.Events.FirstOrDefault(x => x.Id == id);
            return newEvent;
        }

        public IEnumerable<Event> GetAllEvents()
        {
            var events = _appContext.Events.ToList();
            return events;
        }

        public IEnumerable<Event> FindEventByAddress(string address)
        {
            var filtered = _appContext.Events
                .Where(x => x.EventLocation != null && x.EventLocation.ToUpper().Contains(address.ToUpper()))
                .ToList();
            return filtered;
        }

        public bool UpdateEvent(int id, Event updatedEvent)
        {
            var existingEvent = _appContext.Events.SingleOrDefault(x => x.Id == id);

            if (existingEvent == null)
            {
                return false;
            }

            var validator = new EventValidator();
            var validatorResult = validator.Validate(updatedEvent);

            if (!validatorResult.IsValid)
            {
                return false; 
            }
            existingEvent.EventName = updatedEvent.EventName;
            existingEvent.EventDate = updatedEvent.EventDate;
            existingEvent.EventDescription = updatedEvent.EventDescription;
            existingEvent.EventLocation = updatedEvent.EventLocation;

            _appContext.Events.Update(existingEvent);
            _appContext.SaveChanges();
            return true;
        }


        public bool DeleteEvent(int id)
        {
            var existingEvent = _appContext.Events.FirstOrDefault(x => x.Id == id);

            if (existingEvent == null)
            {
                return false;
            }

            var associatedBookings = _appContext.Bookings.Where(booking => booking.EventId == id);
            _appContext.Bookings.RemoveRange(associatedBookings);
            _appContext.Events.Remove(existingEvent);
            _appContext.SaveChanges();

            return true;
        }

    }
}
