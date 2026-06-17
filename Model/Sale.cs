namespace CoffeShopManage.Model;

public class Sale
{
    public int Id { set; get; }
    public decimal Amount { set; get; }
    public DateTime CreatedAt { set; get; }

    public int ProductId { set; get; }
    public int StaffId { get; set; }
    public int Quantity { set; get; }

    public Product Product { set; get; } = null!;
    public Staff Staff { set; get; } = null!;
}
