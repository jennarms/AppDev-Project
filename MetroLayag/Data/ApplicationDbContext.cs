using Microsoft.EntityFrameworkCore;
using MetroLayag.Models;

namespace MetroLayag.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //accounts for testing only
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Station = "Main Office", Username = "admin", PasswordHash = "admin123", Role = "MainAdmin" },
                new User { Id = 2, Station = "Escolta", Username = "escolta", PasswordHash = "escolta123", Role = "StationAdmin" },
                new User { Id = 3, Station = "Lawton", Username = "lawton", PasswordHash = "lawton123", Role = "StationAdmin" },
                new User { Id = 4, Station = "Quinta", Username = "quinta", PasswordHash = "quinta123", Role = "StationAdmin" },
                new User { Id = 5, Station = "PUP", Username = "pup", PasswordHash = "pup123", Role = "StationAdmin" },
                new User { Id = 6, Station = "Sta. Ana", Username = "staana", PasswordHash = "staana123", Role = "StationAdmin" },
                new User { Id = 7, Station = "Lambingan", Username = "lambingan", PasswordHash = "lambingan123", Role = "StationAdmin" },
                new User { Id = 8, Station = "Valenzuela", Username = "valenzuela", PasswordHash = "valenzuela123", Role = "StationAdmin" },
                new User { Id = 9, Station = "Hulo", Username = "hulo", PasswordHash = "hulo123", Role = "StationAdmin" },
                new User { Id = 10, Station = "Guadalupe", Username = "guadalupe", PasswordHash = "guadalupe123", Role = "StationAdmin" },
                new User { Id = 11, Station = "Maybunga", Username = "maybunga", PasswordHash = "maybunga123", Role = "StationAdmin" },
                new User { Id = 12, Station = "San Joaquin", Username = "sanjoaquin", PasswordHash = "sanjoaquin123", Role = "StationAdmin" },
                new User { Id = 13, Station = "Kalawaan", Username = "kalawaan", PasswordHash = "kalawaan123", Role = "StationAdmin" },
                new User { Id = 14, Station = "Pinagbuhatan", Username = "pinagbuhatan", PasswordHash = "pinagbuhatan123", Role = "StationAdmin" }
            );
        }
    }
}
