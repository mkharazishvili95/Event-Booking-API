using System;

namespace Event_Booking_API.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
