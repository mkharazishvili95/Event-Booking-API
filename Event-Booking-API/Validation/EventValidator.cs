using Event_Booking_API.Data;
using Event_Booking_API.Models;
using FluentValidation;
using System;

namespace Event_Booking_API.Validation
{
    public class EventValidator : AbstractValidator<Event>
    {
        public EventValidator()
        {
            RuleFor(makeEvent => makeEvent.EventName).NotEmpty().WithMessage("Event Name must not be empty!");
            RuleFor(makeEvent => makeEvent.EventLocation).NotEmpty().WithMessage("Event Location must not be empty!");
            RuleFor(makeEvent => makeEvent.EventDescription).NotEmpty().WithMessage("Event Description must not be empty!");
            RuleFor(makeEvent => makeEvent.EventDate).NotEmpty().NotNull().GreaterThan(DateTime.Now);
        }
    }
}
