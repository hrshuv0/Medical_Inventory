using Medical_Inventory.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Medical_Inventory.Data;

public class ApplicationDbContext : DbContext
{
        public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(e => e.Category)
            .OnDelete(DeleteBehavior.NoAction);
    }


    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product>? Products { get; set; }
}