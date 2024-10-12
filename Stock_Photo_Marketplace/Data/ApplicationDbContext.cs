using Microsoft.EntityFrameworkCore;
using Stock_Photo_Marketplace.Models;

namespace Stock_Photo_Marketplace.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } //table name
        public DbSet<Photo> Photos { get; set; } //table name
        public DbSet<Category> Categories { get; set; } //table name

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

    }
}