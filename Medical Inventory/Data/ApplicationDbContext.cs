using Microsoft.EntityFrameworkCore;

namespace Medical_Inventory.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    
}