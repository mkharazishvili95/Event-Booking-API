using Xunit;
using Event_Booking_API_Tests.Fake_Services;
using Event_Booking_API.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Event_Booking_API.Data;

namespace Event_Booking_API.Tests
{
    public class BookingServiceTEST
    {
        [Fact]
        public void Test_BookEvent_ValidEvent_ReturnsTrue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EventAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new EventAppContext(options);
            var bookingService = new BookingService(context);

            var event1 = new Event { Id = 1 };
            context.Events.Add(event1);
            context.SaveChanges();

            var booking = new Booking { EventId = 1 };

            // Act
            var result = bookingService.BookEvent(booking);

            // Assert
            Assert.True(result);
        }


        [Fact]
        public void Test_BookEvent_InvalidEvent_ReturnsFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EventAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new EventAppContext(options);
            var bookingService = new BookingService(context);

            var booking = new Booking { EventId = 999 };

            // Act
            var result = bookingService.BookEvent(booking);

            // Assert
            Assert.False(result);
        }


        [Fact]
        public void Test_GetOwnBookingList_ReturnsBookings()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EventAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new EventAppContext(options);
            var bookingService = new BookingService(context);

            var bookings = new List<Booking>
            {
                new Booking { Id = 1 },
                new Booking { Id = 2 },
                new Booking { Id = 3 }
            };

            context.Bookings.AddRange(bookings);
            context.SaveChanges();

            // Act
            var result = bookingService.GetOwnBookingList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookings.Count, result.Count());
        }
    }
}
