using Event_Booking_API.Data;
using Event_Booking_API.Domain;
using Event_Booking_API.Helpers;
using Event_Booking_API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Event_Booking_API.Services
{
    public interface IUserService
    {
        User Login(LoginUser model);
        User Register(RegisterNewUser registrationModel);
    }
    public class UserService : IUserService
    {
        public UserService(EventAppContext context)
        {
            _context = context;
        }
        private readonly EventAppContext _context;
        private readonly List<User> _users = new()
    {
            new User
            {
                Id = 2,
                FirstName = "User",
                LastName = "User",
                UserName = "User123",
                Password = HashSettings.HashPassword("User123"),
                Phone = "7777777",
                Age = 25,
                Role = Roles.User
            }
        };
        private readonly List<User> _admins = new()
        {
            new User
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "Admin123",
                Password = HashSettings.HashPassword("Admin123"),
                Phone = "9999999",
                Age = 28,
                Role = Roles.Admin
                
            }

        };


        public User Login(LoginUser loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.UserName) || string.IsNullOrEmpty(loginModel.Password))
            {
                return null;
            }
            var user = _context.Users.FirstOrDefault(x => x.UserName == loginModel.UserName);
            if (user == null)
            {
                return null;
            }
            if (HashSettings.HashPassword(loginModel.Password) != user.Password)
            {
                return null;
            }
            _context.SaveChanges();
            return user;
        }
        public User Register(RegisterNewUser registrationModel)
        {
            var newUser = new User
            {
                FirstName = registrationModel.FirstName,
                LastName = registrationModel.LastName,
                Phone = registrationModel.Phone,
                Age = registrationModel.Age,
                UserName = registrationModel.UserName,
                Password = HashSettings.HashPassword(registrationModel.Password),
                Role = Roles.User
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

    }
}
