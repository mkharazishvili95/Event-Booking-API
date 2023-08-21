using Microsoft.EntityFrameworkCore;
using Event_Booking_API.Domain;
using Event_Booking_API.Models;
using Event_Booking_API.Helpers;

namespace Event_Booking_API.Data
{
    public class EventAppContext : DbContext
    {
        public EventAppContext(DbContextOptions<EventAppContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Loggs> Loggs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<User>().HasData(
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
                },
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
            );
        }
    }
}
