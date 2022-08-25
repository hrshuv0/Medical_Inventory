using Medical_Inventory.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Medical_Inventory.Data;

public class ApplicationDbContext : IdentityDbContext
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

        modelBuilder
            .Entity<Generic>()
            .HasMany(c => c.Products)
            .WithOne(e => e.Generic)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder
            .Entity<Company>()
            .HasMany(c => c.Products)
            .WithOne(e => e.Company)
            .OnDelete(DeleteBehavior.NoAction);


        base.OnModelCreating(modelBuilder);
    }


    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DbSet<Generic>? Generic { get; set; }
    public DbSet<Medical_Inventory.Models.Company>? Company { get; set; }
}