using Booky.Models;
using Microsoft.EntityFrameworkCore;

namespace Booky.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData( new Category[]{
                new Category { Id = 1,Name="Action" },
                new Category { Id = 2,Name="Novle" },
                new Category { Id = 3,Name="Histoy" },
                new Category { Id = 4,Name="fight" },
            });
        }
        public DbSet<Book>Books { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
