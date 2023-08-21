using Event_Booking_API.Helpers;
using Event_Booking_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Event_Booking_API.Data;
using Event_Booking_API.Models;
using Event_Booking_API.Domain;
using Event_Booking_API.Validation;

namespace Event_Booking_API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;
        private readonly EventAppContext _context;

        public UserController(IUserService userService, IOptions<AppSettings> appSettings, EventAppContext personContext)
        {
            _context = personContext;
            _userService = userService;
            _appSettings = appSettings.Value;
        }


        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginUser user)
        {
            var userLogin = _userService.Login(user);
            if (userLogin == null)
            {
                return BadRequest(new { message = "Username or Password is incorrect!" });
            }
            Loggs log = new Loggs
            {
                Values = $"User {userLogin.UserName} logged in.",
                Created = DateTime.Now
            };
            _context.Loggs.Add(log);
            _context.SaveChanges();

            var tokenString = GenerateToken(userLogin);

            return Ok(new
            {
                Id = userLogin.Id,
                UserName = userLogin.UserName,
                FirstName = userLogin.FirstName,
                LastName = userLogin.LastName,
                Role = userLogin.Role,
                Token = tokenString
            });
        }


        [HttpPost("Register")]
        public IActionResult Register(RegisterNewUser registrationModel)
        {
            var existingUsers = _context.Users.AsQueryable(); 

            var uservalidator = new UserRegisterValidator(existingUsers);
            var validatorResult = uservalidator.Validate(registrationModel);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }
            else
            {
                var newUser = _userService.Register(registrationModel);
                _context.SaveChanges();
                return Ok(newUser);
            }
        }



        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.UserName.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(365),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
