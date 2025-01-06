
using Microsoft.EntityFrameworkCore;
using Shary.Core.Entities;
using Shary.Core.Entities.Order_Aggregate;
using System.Reflection;

namespace Shary.Repository.Data;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options)
        : base(options)
    { }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> Brands { get; set; }
    public DbSet<ProductCategory> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
