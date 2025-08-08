using EZHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace EZHotel.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        } 
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>()
                .Property(r => r.Price)
                .HasPrecision(10, 2);
        }
    }
}
