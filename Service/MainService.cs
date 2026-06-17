using System.Text;

namespace CoffeShopManage.Service;

public class MainService
{
    public SalesService salesService = new();
    public StaffService staffService = new();
    public ProductService productService = new();
    public WarehouseService warehouseService = new();

    public string Report()
    {
        var sb = new StringBuilder();
        sb.AppendLine("========== ОТЧЕТ ==========");
        sb.AppendLine($"Дата: {DateTime.Now:dd.MM.yyyy}\n");
        sb.AppendLine("Краткая статистика:");

        sb.AppendLine($"Общая выручка: {salesService.Revenue()} рублей");

        var bestWorker = staffService.GetBestWorker();
        sb.AppendLine(bestWorker != null ? $"Лучший работник: {bestWorker.FullName} ({bestWorker.Sales.Count} продаж)" : "Лучший работник: нет данных");

        var mostExpensiveSale = salesService.GetMostCostSale();
        sb.AppendLine(mostExpensiveSale != null ? $"Самая дорогая продажа: {mostExpensiveSale.Amount} рублей" : "Самая дорогая продажа: нет данных");

        var mostSaledProduct = productService.GetMostSaledProduct();
        sb.AppendLine(mostSaledProduct != null ? $"Самый популярный товар: {mostSaledProduct.Name}" : "Самый популярный товар: нет данных");

        var mostInStock = warehouseService.GetMostInStock();
        sb.AppendLine(mostInStock?.Product != null ? $"Больше всего на складе: {mostInStock.Product.Name}" : "Склад: нет данных");

        sb.AppendLine("\nПодробная информация:");
        sb.AppendLine("Персонал:\n");
        foreach (var s in staffService.ShowAllStaff() ?? [])
            sb.AppendLine($"ФИО: {s.FullName} | ЗП: {s.Salary}| Должность: {s.Position} | Возраст: {s.Age} | Продаж: {s.Sales.Count}");

        sb.AppendLine("\nТовары:\n");
        foreach (var p in productService.ShowAllProducts() ?? [])
            sb.AppendLine($"Название: {p.Name} | Цена: {p.Price} рублей | Категория: {p.Category} | НДС: {p.VAT}%");

        sb.AppendLine("\nПродажи:\n");
        foreach (var sale in salesService.GetAllSales() ?? [])
            sb.AppendLine($"ID: {sale.Id} | Сумма: {sale.Amount} | Кол-во: {sale.Quantity} | Дата: {sale.CreatedAt}");

        sb.AppendLine("\nСклад:\n");
        foreach (var w in warehouseService.GetAllWHItems() ?? [])
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
            ?.ToList() ?? [];
        return lowStock.Count != 0
            ? $"Нехватка: {string.Join(", ", lowStock)}"
            : "Склад в норме";
    }
}