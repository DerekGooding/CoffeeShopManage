using CoffeShopManage.Model;

namespace CoffeShopManage.Service;

public class SalesService
{
    private readonly DefaultCrud<Sale> _saleCrud = new();

    public List<Sale>? GetAllSales() => _saleCrud.GetAll();

    public Sale AddNewSale(decimal amount, int productId, int staffId, int Quantity)
    {
        var sale = new Sale
        {
            Amount = amount,
            CreatedAt = DateTime.Now,
            ProductId = productId,
            StaffId = staffId,
            Quantity = Quantity
        };
        _saleCrud.Create(sale);
        return sale;
    }

    public Sale? GetMostCostSale() => _saleCrud.GetAll().OrderByDescending(a => a.Amount).FirstOrDefault();

    public decimal Revenue() => _saleCrud.GetAll().Sum(s => s.Amount);
}
