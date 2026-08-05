using CafeManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Infrastructure;

public class AppDbContext : DbContext
{

    public AppDbContext()
    {
        Database.EnsureCreated();
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..","..",".."));
        var dbPath = Path.Combine(projectRoot, "CafeManagement.Main", "cafe_management.db");

        Console.WriteLine($"DB PATH: {dbPath}");

        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
}
