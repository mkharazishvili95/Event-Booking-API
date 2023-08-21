using Xunit;
using Event_Booking_API_Tests;
using Event_Booking_API.Models;
using Event_Booking_API.Helpers;
using Event_Booking_API.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Event_Booking_API.Domain;

namespace Event_Booking_API.Tests
{
    public class UserServiceTEST
    {
        [Fact]
        public void Test_Login_ValidUser_ReturnsUser()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EventAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new EventAppContext(options);
            context.Users.Add(new User
            {
                Id = 1,
                UserName = "Admin123",
                Password = HashSettings.HashPassword("Admin123")
            });
            context.SaveChanges();

            var userService = new UserService(context);

            var loginModel = new LoginUser { UserName = "Admin123", Password = "Admin123" };

            // Act
            var result = userService.Login(loginModel);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Admin123", result.UserName);
        }

        [Fact]
        public void Test_Login_InvalidUser_ReturnsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EventAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new EventAppContext(options);
            var userService = new UserService(context);

            var loginModel = new LoginUser { UserName = "NonExistingUser", Password = "InvalidPassword" };

            // Act
            var result = userService.Login(loginModel);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Test_Register_ValidUser_ReturnsUser()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EventAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new EventAppContext(options);
            var userService = new UserService(context);

            var registrationModel = new RegisterNewUser
            {
                FirstName = "Test",
                LastName = "User",
                UserName = "TestUser",
                Password = "Test123",
                Phone = "123456789",
                Age = 30
            };

            // Act
            var result = userService.Register(registrationModel);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("TestUser", result.UserName);
        }
    }
}
