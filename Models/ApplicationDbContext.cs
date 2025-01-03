using Microsoft.EntityFrameworkCore;

namespace expense_tracker.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Transaction>? Transactions { get; set; }
    }
}
