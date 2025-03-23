using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;

namespace Stock.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Stocks { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<ItemStock> ItemStocks { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}