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

            // Seed Main Admin user
            var passwordHash = "$2a$11$fJ8yUYDZQsMZVc/j8JNV.e7ijmTc0S3IAYtbnmvfRMyEvN6VvFHBu";
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Station = "Main Office",
                    Username = "admin",
                    PasswordHash = passwordHash,
                    Role = "MainAdmin"
                }
            );
        }
    }
}
