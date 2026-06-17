using CoffeShopManage.Model;

namespace CoffeShopManage.Service;

public class ProductService
{
    private readonly DefaultCrud<Product> _productCrud = new();

    public List<Product>? ShowAllProducts() => _productCrud.GetAll();

    public Product AddNewProduct(string name, decimal price, string category, int vat, bool isActive)
    {
        var product = new Product(name, price, category, vat, isActive);
        return _productCrud.Create(product);
    }

    public Product? GetProduct(int id) => _productCrud.Get(id);

    public string ChangePrice(int id, decimal amount)
    {
        var product = _productCrud.Get(id);
        if (product == null) { return "Товара не существует"; }
        product.Price = amount;
        _productCrud.Update(id, product);
        return $"Цена товара '{product.Name}' изменена на {product.Price} рублей";
    }

    public Product? GetMostSaledProduct() => _productCrud.GetAll().OrderByDescending(s => s.Sales.Count).FirstOrDefault();
}
