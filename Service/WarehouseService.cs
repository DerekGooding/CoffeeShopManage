using CoffeShopManage.Model;

namespace CoffeShopManage.Service;

public class WarehouseService
{
    private readonly DefaultCrud<Warehouse> _wHCrud = new();

    public List<Warehouse>? GetAllWHItems() => _wHCrud.GetAll();

    public Warehouse AddNewWHItem(int productId, int stock, string status)
    {
        var wh = new Warehouse
        {
            ProductId = productId,
            Stock = stock,
            Status = status
        };
        _wHCrud.Create(wh);
        return wh;
    }

    public Warehouse? GetMostInStock() => _wHCrud.GetAll().OrderByDescending(s => s.Stock).FirstOrDefault();

    public string ChangeStock(int id, int amount, string method)
    {
        var wh = _wHCrud.Get(id);
        if (wh == null) { return "На складе нет такого предмета"; }
        if (method == "increase") { wh.Stock += amount; _wHCrud.Update(id, wh); return "Кол-во успешно увеличено"; }
        else if (method == "decrease") { wh.Stock -= amount; _wHCrud.Update(id, wh); return "Кол-во успешно уменьшено"; }
        else { return "Недопустимый метод"; }
    }
}
