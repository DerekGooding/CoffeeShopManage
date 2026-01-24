using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

public class Product
{
    public int Id {set; get;}
    public string Name {set; get;} = "Товар без имени";
    public decimal Price {set; get;}
    public string Category {set; get;} = null!;
    public int VAT {set; get;}
    public bool IsActive {set; get;}

    public List<Sale> Sales {set; get;} = new();

     public Product() { }

    public Product(string name, decimal price, string category, int vat, bool isActive)
    {
        Name = name;
        Price = price;
        Category = category;
        VAT = vat;
        IsActive = isActive;
        
    }
}

public class Staff
{
    public int Id {set; get;}
    public string FullName {set; get;} = "Имя не указано";
    public decimal Salary {set; get;}
    public int Age {set; get;}
    public string Position {set; get;} = null!;

    public List<Sale> Sales {set; get;} = new();

    public Staff() {}

    public Staff(string fullName, decimal salary, int age, string position)
    {
        FullName = fullName;
        Salary = salary;
        Age = age;
        Position = position;
    }
}

public class Sale
{
    public int Id {set; get;}
    public decimal Amount {set; get;}
    public DateTime CreatedAt {set; get;}

    public int ProductId {set; get;}
    public int StaffId {get; set;}
    public int Quantity {set; get;}

    public Product Product {set; get;} = null!;
    public Staff Staff {set; get;} = null!;
}

public class Warehouse
{
    public int Id {set; get;}
    public int ProductId {set; get;}
    public int Stock {set; get;}
    public string Status {set; get;} = null!;

    public Product Product {set; get;} = null!;
}


public class AppDbContext : DbContext
{
    public DbSet<Product> Products {set; get;}
    public DbSet<Staff> Staff {set; get;}
    public DbSet<Sale> Sales {set; get;}
    public DbSet<Warehouse> WarehouseItems {set; get;}

    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=coffeeshop.db");
    
} 