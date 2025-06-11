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
    }
}
