using Event_Booking_API.Domain;
using Event_Booking_API.Models;
using FluentValidation;
using System.Linq;

namespace Event_Booking_API.Validation
{
    public class UserRegisterValidator : AbstractValidator<RegisterNewUser>
    {
        private readonly IQueryable<User> _existingUsers;

        public UserRegisterValidator(IQueryable<User> existingUsers)
        {
            _existingUsers = existingUsers;

            RuleFor(userRegister => userRegister.FirstName).NotEmpty().WithMessage("Enter your FirstName!");
            RuleFor(userRegister => userRegister.LastName).NotEmpty().WithMessage("Enter your LastName!");
            RuleFor(userRegister => userRegister.Age).NotNull().NotEmpty().WithMessage("Enter your Age!")
                .GreaterThanOrEqualTo(18).WithMessage("Age must be more than 17 years!");
            RuleFor(userRegister => userRegister.UserName).NotEmpty().NotNull().WithMessage("Enter your UserName!")
                .Length(6, 15).WithMessage("UserName must be between 6 and 15 chars or numbers!")
                .Must(differentUserName).WithMessage("UserName already exists. Try another one!");
            RuleFor(userRegister => userRegister.Password).NotEmpty().NotNull().WithMessage("Enter your Password!")
                .Length(6, 15).WithMessage("Password must be between 6 and 15 chars or numbers!");
            RuleFor(userRegister => userRegister.Phone).NotEmpty().NotNull().WithMessage("Enter your Mobile number!");
        }

        private bool differentUserName(string userName)
        {
            var userNameCheck = _existingUsers.FirstOrDefault(x => x.UserName.ToUpper() == userName.ToUpper());
            return userNameCheck == null;
        }
    }
}
