using Medical_Inventory.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Medical_Inventory.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>
{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().ToTable("Category");
        modelBuilder.Entity<Generic>().ToTable("Generic");
        modelBuilder.Entity<Company>().ToTable("Company");
        modelBuilder.Entity<Product>().ToTable("Product");
        modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");
        modelBuilder.Entity<PatientGroup>().ToTable("PatientGroup");
        modelBuilder.Entity<RecommandedPatient>().ToTable("RecommandedPatient");

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
    public DbSet<Company>? Company { get; set; }
    public DbSet<ApplicationUser>? ApplicationUser { get; set; }
    public DbSet<PatientGroup>? PatientGroup { get; set; }
    public DbSet<RecommandedPatient>? RecommandedPatient { get; set; }
}