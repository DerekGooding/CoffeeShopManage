using CoffeShopManage.Service;

namespace CoffeShopManage;

internal static class Program
{
    private static readonly MainService _mainService = new();

    private static void Main()
    {
        while (true)
        {
            Console.Clear();
            ShowMainMenu();

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1": ProductMenu(); break;
                case "2": StaffMenu(); break;
                case "3": SalesMenu(); break;
                case "4": WarehouseMenu(); break;
                case "5": ReportMenu(); break;
                case "0": return;
            }
            Console.WriteLine("\nНажмите Enter...");
            Console.ReadLine();
        }
    }

    private static void ShowMainMenu()
    {
        Console.WriteLine("=== КОФЕЙНЯ ===");
        Console.WriteLine("1. Товары");
        Console.WriteLine("2. Персонал");
        Console.WriteLine("3. Продажи");
        Console.WriteLine("4. Склад");
        Console.WriteLine("5. Отчет");
        Console.WriteLine("0. Выход");
        Console.Write(">>> ");
    }

    private static void ProductMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ТОВАРЫ ===");
            Console.WriteLine("1. Список");
            Console.WriteLine("2. Добавить");
            Console.WriteLine("3. Изменить цену");
            Console.WriteLine("0. Назад");
            Console.Write(">>> ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowProducts();
                    break;

                case "2":
                    AddProduct();
                    break;

                case "3":
                    ChangeProductPrice();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }

            Console.WriteLine("\nНажмите enter...");
            Console.ReadLine();
        }
    }

    private static void ShowProducts()
    {
        Console.WriteLine("\nСПИСОК ТОВАРОВ:");

        var products = _mainService.productService.ShowAllProducts() ?? [];
        if (products.Count == 0)
        {
            Console.WriteLine("Пусто");
            return;
        }

        foreach (var p in products)
            Console.WriteLine($"Название: {p.Name} | Цена: {p.Price} рублей | Категория: {p.Category} | НДС: {p.VAT}% | ID: {p.Id}");
    }

    private static void AddProduct()
    {
        Console.Write("Название: ");
        var name = Console.ReadLine() ?? "Без имени";

        Console.Write("Цена: ");
        var price = decimal.Parse(Console.ReadLine() ?? "0");

        Console.Write("Категория: ");
        var category = Console.ReadLine() ?? "Общее";

        Console.Write("НДС (%): ");
        var vat = int.Parse(Console.ReadLine() ?? "20");

        Console.Write("Активен? (y/n): ");
        var isActive = Console.ReadLine()?.ToLower() == "y";

        var product = _mainService.productService.AddNewProduct(name, price, category, vat, isActive);
        Console.WriteLine($"\nТовар '{product.Name}' добавлен. ID: {product.Id}");
    }

    private static void ChangeProductPrice()
    {
        Console.Write("ID товара: ");
        if (int.TryParse(Console.ReadLine(), out var id) && id > 0)
        {
            Console.Write("Новая цена: ");
            if (decimal.TryParse(Console.ReadLine(), out var newPrice))
            {
                var result = _mainService.productService.ChangePrice(id, newPrice);
                Console.WriteLine($"\n{result}");
            }
            else
            {
                Console.WriteLine("Неверная цена");
            }
        }
        else
        {
            Console.WriteLine("Неверный ID");
        }
    }

    private static void StaffMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ПЕРСОНАЛ ===");
            Console.WriteLine("1. Список");
            Console.WriteLine("2. Нанять");
            Console.WriteLine("3. Уволить");
            Console.WriteLine("0. Назад");
            Console.Write(">>> ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowAllStaff();
                    break;

                case "2":
                    AddStaff();
                    break;

                case "3":
                    FireStaff();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }

            Console.WriteLine("\nНажмите enter...");
            Console.ReadLine();
        }
    }

    private static void ShowAllStaff()
    {
        Console.WriteLine("\nСПИСОК РАБОТНИКОВ:");

        var staff = _mainService.staffService.ShowAllStaff() ?? [];
        if (staff.Count == 0)
        {
            Console.WriteLine("Пусто");
            return;
        }

        foreach (var s in staff)
            Console.WriteLine($"ФИО: {s.FullName} | ЗП: {s.Salary} | Возраст: {s.Age} | Продаж: {s.Sales.Count} | ID: {s.Id}");
    }

    private static void AddStaff()
    {
        Console.Write("ФИО: ");
        var name = Console.ReadLine() ?? "Без имени";

        Console.Write("Зарплата: ");
        var salary = decimal.Parse(Console.ReadLine() ?? "0");

        Console.Write("Возраст: ");
        var age = int.Parse(Console.ReadLine() ?? "18");

        Console.Write("Должность: ");
        var position = Console.ReadLine() ?? "Бариста";

        var staff = _mainService.staffService.AddNewStaff(name, salary, age, position);
        Console.WriteLine($"\nРаботник '{staff.FullName}' добавлен. ID: {staff.Id}");
    }

    private static void FireStaff()
    {
        Console.WriteLine("ID работника: ");
        if (int.TryParse(Console.ReadLine(), out var id) && id > 0)
        {
            Console.WriteLine(_mainService.staffService.FireStaff(id));
        }
        else
        {
            Console.WriteLine("Неверный ID");
        }
    }

    private static void SalesMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ПРОДАЖИ ===");
            Console.WriteLine("1. Список");
            Console.WriteLine("2. Добавить");
            Console.WriteLine("3. Общая выручка");
            Console.WriteLine("0. Назад");
            Console.Write(">>> ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowAllSales();
                    break;

                case "2":
                    AddSale();
                    break;

                case "3":
                    ShowRevenue();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }

            Console.WriteLine("\nНажмите enter...");
            Console.ReadLine();
        }
    }

    private static void ShowAllSales()
    {
        Console.WriteLine("\nСПИСОК ПРОДАЖ:");

        var sales = _mainService.salesService.GetAllSales() ?? [];
        if (sales.Count == 0)
        {
            Console.WriteLine("Пусто");
            return;
        }

        foreach (var sale in sales)
            Console.WriteLine($"ID: {sale.Id} | Сумма: {sale.Amount} | Кол-во: {sale.Quantity} | Дата: {sale.CreatedAt}");
    }

    private static void AddSale()
    {
        Console.Write("Кол-во товара: ");
        var qty = int.Parse(Console.ReadLine() ?? "1");

        Console.Write("Сумма: ");
        var amount = decimal.Parse(Console.ReadLine() ?? "1");

        Console.Write("ID товара: ");
        var productId = int.Parse(Console.ReadLine() ?? "1");

        Console.Write("ID работника: ");
        var staffId = int.Parse(Console.ReadLine() ?? "1");

        var sale = _mainService.salesService.AddNewSale(amount, productId, staffId, qty);
        Console.WriteLine($"\nПродажа '{sale.Id}' добавлена. ID: {sale.Id}");
    }

    private static void ShowRevenue() => Console.WriteLine($"Общая выручка: {_mainService.salesService.Revenue()} рублей");

    private static void WarehouseMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== СКЛАД ===");
            Console.WriteLine("1. Список позиций");
            Console.WriteLine("2. Добавить");
            Console.WriteLine("3. Обновить остаток");
            Console.WriteLine("0. Назад");
            Console.Write(">>> ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowAllWHItems();
                    break;

                case "2":
                    AddWHItem();
                    break;

                case "3":
                    ChangeStock();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }

            Console.WriteLine("\nНажмите enter...");
            Console.ReadLine();
        }
    }

    private static void ShowAllWHItems()
    {
        Console.WriteLine("\nСПИСОК ПОЗИЦИЙ СКЛАДА:");

        var whItems = _mainService.warehouseService.GetAllWHItems() ?? [];
        if (whItems.Count == 0)
        {
            Console.WriteLine("Пусто");
            return;
        }

        foreach (var w in whItems)
            Console.WriteLine($"ID: {w.Id} | Товар ID: {w.ProductId} | Остаток: {w.Stock} | Статус: {w.Status}");
    }

    private static void AddWHItem()
    {
        Console.Write("Кол-во товара: ");
        var qty = int.Parse(Console.ReadLine() ?? "1");

        Console.Write("ID товара: ");
        var productId = int.Parse(Console.ReadLine() ?? "1");

        Console.Write("Статус: ");
        var status = Console.ReadLine() ?? "не указан";

        var wh = _mainService.warehouseService.AddNewWHItem(productId, qty, status);
        Console.WriteLine($"\nПозиция склада '{wh}' добавлена. ID: {wh.Id}");
    }

    private static void ChangeStock()
    {
        Console.Write("ID позиции: ");
        if (int.TryParse(Console.ReadLine(), out var id) && id > 0)
        {
            Console.Write("Метод (1 - прибавить; 2 - убавить): ");
            if (int.TryParse(Console.ReadLine(), out var intMethod))
            {
                var method = intMethod == 1 ? "increase" : "decrease";
                Console.Write("Введите кол-во: ");
                if (int.TryParse(Console.ReadLine(), out var amount))
                {
                    var result = _mainService.warehouseService.ChangeStock(id, amount, method);
                    Console.WriteLine($"\n{result}");
                }
                else
                {
                    Console.WriteLine("Неверное кол-во");
                }
            }
            else
            {
                Console.WriteLine("Неверный метод");
            }
        }
        else
        {
            Console.WriteLine("Неверный ID");
        }
    }

    private static void ReportMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ОТЧЕТ ===");
            Console.WriteLine("1. Получить отчет");
            Console.WriteLine("2. Скачать отчет");
            Console.WriteLine("0. Назад");
            Console.Write(">>> ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowReport();
                    break;

                case "2":
                    DownloadReport();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }

            Console.WriteLine("\nНажмите enter...");
            Console.ReadLine();
        }
    }

    private static void ShowReport() => Console.WriteLine(_mainService.Report());

    private static void DownloadReport()
    {
        var report = _mainService.Report();
        var path = $"report_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
        File.WriteAllText(path, report);
        Console.WriteLine($"Отчет скачан в {path}");
    }
}