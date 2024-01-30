using Microsoft.EntityFrameworkCore;

namespace RTTest.Models
{
    public class CoffeeContext : DbContext
    {
        public CoffeeContext(DbContextOptions<CoffeeContext> options) : base(options)
        {
        }

        public DbSet<CoffeeItem> CoffeeItems { get; set; } = null!;
    }
}
