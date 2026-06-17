namespace CoffeShopManage.Model;

public class Warehouse
{
    public int Id { set; get; }
    public int ProductId { set; get; }
    public int Stock { set; get; }
    public string Status { set; get; } = null!;

    public Product Product { set; get; } = null!;
}
