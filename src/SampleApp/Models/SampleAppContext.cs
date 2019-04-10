using Microsoft.EntityFrameworkCore;

namespace SampleApp.Models
{
    public class SampleAppContext : DbContext
    {
        public SampleAppContext(DbContextOptions<SampleAppContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
    }
}
