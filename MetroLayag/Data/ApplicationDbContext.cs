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

            var passwordHash = "$2a$11$fJ8yUYDZQsMZVc/j8JNV.e7ijmTc0S3IAYtbnmvfRMyEvN6VvFHBu"; // hashed "pass123"

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Station = "Main Office", Username = "admin", PasswordHash = passwordHash, Role = "MainAdmin" },
                new User { Id = 2, Station = "Escolta", Username = "escolta", PasswordHash = passwordHash, Role = "StationAdmin" },
                new User { Id = 3, Station = "Lawton", Username = "lawton", PasswordHash = passwordHash, Role = "StationAdmin" },
                new User { Id = 4, Station = "Quinta", Username = "quinta", PasswordHash = passwordHash, Role = "StationAdmin" },
                new User { Id = 5, Station = "PUP", Username = "pup", PasswordHash = passwordHash, Role = "StationAdmin" },
                new User { Id = 6, Station = "Sta. Ana", Username = "staana", PasswordHash = passwordHash, Role = "StationAdmin" },
                new User { Id = 7, Station = "Lambingan", Username = "lambingan", PasswordHash = passwordHash, Role = "StationAdmin" },
                new User { Id = 8, Station = "Valenzuela", Username = "valenzuela", PasswordHash = passwordHash, Role = "StationAdmin" },
                new User { Id = 9, Station = "Hulo", Username = "hulo", PasswordHash = passwordHash, Role = "StationAdmin" },
                new User { Id = 10, Station = "Guadalupe", Username = "guadalupe", PasswordHash = passwordHash, Role = "StationAdmin" },
                new User { Id = 11, Station = "Maybunga", Username = "maybunga", PasswordHash = passwordHash, Role = "StationAdmin" },
                new User { Id = 12, Station = "San Joaquin", Username = "sanjoaquin", PasswordHash = passwordHash, Role = "StationAdmin" },
                new User { Id = 13, Station = "Kalawaan", Username = "kalawaan", PasswordHash = passwordHash, Role = "StationAdmin" },
                new User { Id = 14, Station = "Pinagbuhatan", Username = "pinagbuhatan", PasswordHash = passwordHash, Role = "StationAdmin" }
            );
        }
    }
}
