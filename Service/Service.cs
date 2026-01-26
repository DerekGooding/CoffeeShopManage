
using System.Reflection.Metadata.Ecma335;
using System.Runtime;
using System.Text;
using Microsoft.VisualBasic;
using SQLitePCL;


public class StaffService {
        private DefaultCrud<Staff> StaffCrud = new DefaultCrud<Staff>();
        public string FireStaff(int id)
        {   
            var success = StaffCrud.Delete(id);
            if (success) {return $"Сотрудник {id} уволен";}
            else {return "Сотрудника c таким id не существует";}
        }
        public List<Staff>? ShowAllStaff()
        {
            return StaffCrud.GetAll();
        }
        public Staff AddNewStaff(string fullName, decimal salary, int age, string position) 
        {
            var staff = new Staff(fullName, salary, age, position);
            return StaffCrud.Create(staff);
        }
        public Staff? GetOneStaff(int id)
        {
            return StaffCrud.Get(id);
        }
        public Staff? GetBestWorker()
        {
            return StaffCrud.GetAll().OrderByDescending(s => s.Sales.Count).FirstOrDefault();
        } 
    }

    public class ProductService
    {
        private DefaultCrud<Product> ProductCrud = new DefaultCrud<Product>();
        public List<Product>? ShowAllProducts()
        {
            return ProductCrud.GetAll();
        }
        public Product AddNewProduct(string name, decimal price, string category, int vat, bool isActive) 
        {   
            Console.WriteLine($"Название до создания товара: {name}");
            var product = new Product(name, price, category, vat, isActive);
            Console.WriteLine($"Название после создания товара: {product.Name}");
            return ProductCrud.Create(product);
        }
        public Product? GetProduct(int id)
        {
            return ProductCrud.Get(id);
        }
        public string ChangePrice(int id, decimal amount)
        {
            var product = ProductCrud.Get(id);
            if (product == null) {return "Товара не существует";}
            product.Price = amount;
            ProductCrud.Update(id, product);
            return $"Цена товара {id} измнена на {product.Price} рублей";
        }
        public Product? GetMostSaledProduct()
        {
            return ProductCrud.GetAll().OrderByDescending(s => s.Sales.Count).FirstOrDefault();
        }
    }
    public class SalesService
    {
        private DefaultCrud<Sale> saleCrud = new DefaultCrud<Sale>();
        public List<Sale>? GetAllSales()
        {
            return saleCrud.GetAll();
        }
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
            saleCrud.Create(sale);
            return sale;

        }
        public Sale? GetMostCostSale()
        {
            return saleCrud.GetAll().OrderByDescending(a => a.Amount).FirstOrDefault();
        }
        public decimal Revenue()
        {
            return saleCrud.GetAll().Sum(s => s.Amount);
        }
    }    
    public class WarehouseService
    {
        private DefaultCrud<Warehouse> WHCrud = new DefaultCrud<Warehouse>();
        public List<Warehouse>? GetAllWHItems()
        {
            return WHCrud.GetAll();
        }
        public Warehouse AddNewWHItem(int productId, int stock, string status)
        {
            var wh = new Warehouse
            {
                ProductId = productId,
                Stock = stock,
                Status = status
            };
            WHCrud.Create(wh);
            return wh;

        }
        public Warehouse? GetMostInStock()
        {
            return WHCrud.GetAll().OrderByDescending(s => s.Stock).FirstOrDefault();
        }
        public string ChangeStock(int id, int amount, string method)
        {
            var wh = WHCrud.Get(id);
            if (wh==null) {return "На складе нет такого предмета";}
            if (method == "increase") {wh.Stock += amount; return "Кол-во успешно увеличено";}
            else if (method == "decrease") {wh.Stock -= amount; return "Кол-во успешно уменьшено";}
            else {return "Недопустимый метод";}
        }
    }   
    



public class MainService
{
    public SalesService salesService = new SalesService();
    public StaffService staffService = new StaffService();
    public ProductService productService = new ProductService();
    public WarehouseService warehouseService = new WarehouseService();
    public string Report()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Отчет");
        sb.AppendLine($"Дата: {DateTime.Now:dd.MM.yyyy}\n");
        sb.AppendLine("Краткая статистика:");

        sb.AppendLine($"Общая выручка: {salesService.Revenue()} рублей");

        var bestWorker = staffService.GetBestWorker();
        sb.AppendLine(bestWorker != null ? $"Лучший работник: {bestWorker.FullName} ({bestWorker.Sales.Count} продаж)" : "Лучший работник: нет данных");
        
        var mostExpensiveSale = salesService.GetMostCostSale();
        sb.AppendLine(mostExpensiveSale != null ? $"Самая дорогая продажа: {mostExpensiveSale.Amount} рублей": "Самая дорогая продажа: нет данных");
        
        var mostSaledProduct = productService.GetMostSaledProduct();
        sb.AppendLine(mostSaledProduct != null ? $"Самый популярный товар: {mostSaledProduct.Name}" : "Самый популярный товар: нет данных");
        
        var mostInStock = warehouseService.GetMostInStock();
        sb.AppendLine(mostInStock?.Product != null ? $"Больше всего на складе: {mostInStock.Product.Name}" : "Склад: нет данных");


        sb.AppendLine("\nПодробная информация:");
        sb.AppendLine("Персонал:\n");
        foreach (var s in staffService.ShowAllStaff() ?? new List<Staff>())
            sb.AppendLine($"ФИО: {s.FullName} | ЗП: {s.Salary} | Возраст: {s.Age} | Продаж: {s.Sales.Count}");

        sb.AppendLine("\nТовары:\n");
        foreach (var p in productService.ShowAllProducts() ?? new List<Product>())
            sb.AppendLine($"Название: {p.Name} | Цена: {p.Price} рублей | Категория: {p.Category} | НДС: {p.VAT}%");

        sb.AppendLine("\nПродажи:\n");
        foreach (var sale in salesService.GetAllSales() ?? new List<Sale>())
            sb.AppendLine($"ID: {sale.Id} | Сумма: {sale.Amount} | Кол-во: {sale.Quantity} | Дата: {sale.CreatedAt}");

        sb.AppendLine("\nСклад:\n");
        foreach (var w in warehouseService.GetAllWHItems() ?? new List<Warehouse>())
            sb.AppendLine($"ID: {w.Id} | Товар ID: {w.ProductId} | Остаток: {w.Stock} | Статус: {w.Status}");

        return sb.ToString();  
    }
    public string DailyReport(DateTime date)
    {
        var todaySales = salesService.GetAllSales()
            ?.Where(s => s.CreatedAt.Date == date.Date)
            ?.Sum(s => s.Amount) ?? 0;
        return $"Выручка за {date:dd.MM.yyyy}: {todaySales} рублей";
    }
    public string LowStockAlert()
    {
        var lowStock = warehouseService.GetAllWHItems()
            ?.Where(w => w.Stock < 10)
            ?.Select(w => w.Product.Name)
            ?.ToList() ?? new();
        return lowStock.Any() 
            ? $"Нехватка: {string.Join(", ", lowStock)}"
            : "Склад в норме";
    }

} 

