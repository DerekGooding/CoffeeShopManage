namespace CoffeShopManage.Model;

public class Product
{
    public int Id { set; get; }
    public string Name { set; get; } = "Товар без имени";
    public decimal Price { set; get; }
    public string Category { set; get; } = null!;
    public int VAT { set; get; }
    public bool IsActive { set; get; }

    public List<Sale> Sales { set; get; } = new();

    public Product()
    { }

    public Product(string name, decimal price, string category, int vat, bool isActive)
    {
        Name = name;
        Price = price;
        Category = category;
        VAT = vat;
        IsActive = isActive;
    }
}
