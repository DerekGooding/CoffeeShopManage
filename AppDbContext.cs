using CoffeShopManage.Model;
using Microsoft.EntityFrameworkCore;

namespace CoffeShopManage;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { set; get; }
    public DbSet<Staff> Staff { set; get; }
    public DbSet<Sale> Sales { set; get; }
    public DbSet<Warehouse> WarehouseItems { set; get; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=coffeeshop.db");
}