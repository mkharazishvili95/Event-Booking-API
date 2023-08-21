using Xunit;
using Event_Booking_API_Tests.Fake_Services;
using Event_Booking_API.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Event_Booking_API.Data;
using FluentValidation.Results;
using System;

namespace Event_Booking_API.Tests
{
    public class EventServiceTEST
    {
        [Fact]
        public void Test_CreateEvent_ValidEvent_ReturnsEvent()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EventAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new EventAppContext(options);
            var eventService = new EventService(context);

            var newEvent = new Event
            {
                EventName = "Test Event",
                EventDate = DateTime.Now.AddDays(7),
                EventLocation = "Test Location",
                EventDescription = "Test Description"
            };

            // Act
            var result = eventService.CreateEvent(newEvent);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Event", result.EventName);
        }

        [Fact]
        public void Test_CreateEvent_InvalidEvent_ThrowsArgumentException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EventAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new EventAppContext(options);
            var eventService = new EventService(context);

            var newEvent = new Event(); // An intentionally invalid event

            // Act & Assert
            Assert.Throws<ArgumentException>(() => eventService.CreateEvent(newEvent));
        }

        [Fact]
        public void Test_GetEventByID_ValidID_ReturnsEvent()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EventAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new EventAppContext(options);
            var eventService = new EventService(context);

            var events = new List<Event>
            {
                new Event { Id = 1, EventName = "Event 1" },
                new Event { Id = 2, EventName = "Event 2" }
            };

            context.Events.AddRange(events);
            context.SaveChanges();

            // Act
            var result = eventService.GetEventByID(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Event 1", result.EventName);
        }

        [Fact]
        public void Test_GetEventByID_InvalidID_ReturnsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EventAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new EventAppContext(options);
            var eventService = new EventService(context);

            // Act
            var result = eventService.GetEventByID(999); 

            // Assert
            Assert.Null(result);
        }

      
    }
}
