using LondonAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LondonAPI.DataSource
{
    public class HotelApiDbContext(DbContextOptions<HotelApiDbContext> options) : DbContext(options)
    {
        public DbSet<RoomEntity> Rooms { get; set; }


        public DbSet<BookingEntity> Bookings { get; set; }
    }
}
