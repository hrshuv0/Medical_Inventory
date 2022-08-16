using Medical_Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Medical_Inventory.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product>? Products { get; set; }
}