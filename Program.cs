using System.Threading.Tasks.Dataflow;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualBasic;

class Program
{
    static MainService mainService = new MainService();

    static void Main()
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
    static void ShowMainMenu()
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
    static void ProductMenu()
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

    static void ShowProducts()
    {
        Console.WriteLine("\nСПИСОК ТОВАРОВ:");

        var products = mainService.productService.ShowAllProducts() ?? new();
        if (!products.Any())
        {
            Console.WriteLine("Пусто");
            return;
        }

        foreach (var p in products)
            Console.WriteLine($"Название: {p.Name} | Цена: {p.Price} рублей | Категория: {p.Category} | НДС: {p.VAT}% | ID: {p.Id}");

    }

    static void AddProduct()
    {
        Console.Write("Название: ");
        string name = Console.ReadLine() ?? "Без имени";

        Console.Write("Цена: ");
        decimal price = decimal.Parse(Console.ReadLine() ?? "0");

        Console.Write("Категория: ");
        string category = Console.ReadLine() ?? "Общее";

        Console.Write("НДС (%): ");
        int vat = int.Parse(Console.ReadLine() ?? "20");

        Console.Write("Активен? (y/n): ");
        bool isActive = Console.ReadLine()?.ToLower() == "y";


        var product = mainService.productService.AddNewProduct(name, price, category, vat, isActive);
        Console.WriteLine($"\nТовар '{product.Name}' добавлен. ID: {product.Id}");
    }

    static void ChangeProductPrice()
    {
        Console.Write("ID товара: ");
        if (int.TryParse(Console.ReadLine(), out int id) && id > 0)
        {
            Console.Write("Новая цена: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
            {
                string result = mainService.productService.ChangePrice(id, newPrice);
                Console.WriteLine($"\n{result}");
            }
            else Console.WriteLine("Неверная цена");
        }
        else Console.WriteLine("Неверный ID");
    }
    static void StaffMenu()
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
    static void ShowAllStaff()
    {
        Console.WriteLine("\nСПИСОК РАБОТНИКОВ:");

        var staff = mainService.staffService.ShowAllStaff() ?? new();
        if (!staff.Any())
        {
            Console.WriteLine("Пусто");
            return;
        }

        foreach (var s in staff)
            Console.WriteLine($"ФИО: {s.FullName} | ЗП: {s.Salary} | Возраст: {s.Age} | Продаж: {s.Sales.Count} | ID: {s.Id}");

    }
    static void AddStaff()
    {
        Console.Write("ФИО: ");
        string name = Console.ReadLine() ?? "Без имени";

        Console.Write("Зарплата: ");
        decimal salary = decimal.Parse(Console.ReadLine() ?? "0");

        Console.Write("Возраст: ");
        int age = int.Parse(Console.ReadLine() ?? "18");

        Console.Write("Должность: ");
        string position = Console.ReadLine() ?? "Бариста";

        var staff = mainService.staffService.AddNewStaff(name, salary, age, position);
        Console.WriteLine($"\nРаботник '{staff.FullName}' добавлен. ID: {staff.Id}");
    }
    static void FireStaff()
    {
        Console.WriteLine("ID работника: ");
        if (int.TryParse(Console.ReadLine(), out int id) && id > 0)
        {
            Console.WriteLine(mainService.staffService.FireStaff(id));
        }
        else Console.WriteLine("Неверный ID");
    }
    static void SalesMenu()
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
    static void ShowAllSales()
    {
        Console.WriteLine("\nСПИСОК ПРОДАЖ:");

        var sales = mainService.salesService.GetAllSales() ?? new();
        if (!sales.Any())
        {
            Console.WriteLine("Пусто");
            return;
        }

        foreach (var sale in sales)
            Console.WriteLine($"ID: {sale.Id} | Сумма: {sale.Amount} | Кол-во: {sale.Quantity} | Дата: {sale.CreatedAt}");

    }
    static void AddSale()
    {
        Console.Write("Кол-во товара: ");
        int qty = int.Parse(Console.ReadLine() ?? "1");

        Console.Write("Сумма: ");
        decimal amount = decimal.Parse(Console.ReadLine() ?? "1");

        Console.Write("ID товара: ");
        int productId = int.Parse(Console.ReadLine() ?? "1");

        Console.Write("ID работника: ");
        int staffId = int.Parse(Console.ReadLine() ?? "1");

        var sale = mainService.salesService.AddNewSale(amount, productId, staffId, qty);
        Console.WriteLine($"\nПродажа '{sale.Id}' добавлена. ID: {sale.Id}");
    }
    static void ShowRevenue()
    {
        Console.WriteLine($"Общая выручка: {mainService.salesService.Revenue()} рублей");

    }
    static void WarehouseMenu()
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
    static void ShowAllWHItems()
    {
        Console.WriteLine("\nСПИСОК ПОЗИЦИЙ СКЛАДА:");

        var whItems = mainService.warehouseService.GetAllWHItems() ?? new();
        if (!whItems.Any())
        {
            Console.WriteLine("Пусто");
            return;
        }

        foreach (var w in whItems)
            Console.WriteLine($"ID: {w.Id} | Товар ID: {w.ProductId} | Остаток: {w.Stock} | Статус: {w.Status}");

    }
    static void AddWHItem()
    {
        Console.Write("Кол-во товара: ");
        int qty = int.Parse(Console.ReadLine() ?? "1");

        Console.Write("ID товара: ");
        int productId = int.Parse(Console.ReadLine() ?? "1");

        Console.Write("Статус: ");
        string status = Console.ReadLine() ?? "не указан";

        var wh = mainService.warehouseService.AddNewWHItem(productId, qty, status);
        Console.WriteLine($"\nПозиция склада '{wh}' добавлена. ID: {wh.Id}");
    }
    static void ChangeStock()
    {
        Console.Write("ID позиции: ");
        if (int.TryParse(Console.ReadLine(), out int id) && id > 0)
        {
            Console.Write("Метод (1 - прибавить; 2 - убавить): ");
            if (int.TryParse(Console.ReadLine(), out int intMethod))
            {   
                var method = "increase";
                if (intMethod == 1) {method = "increase";} else {method = "decrease";}
                Console.Write("Введите кол-во: ");
                if (int.TryParse(Console.ReadLine(), out int amount)) {
                string result = mainService.warehouseService.ChangeStock(id, amount, method);
                Console.WriteLine($"\n{result}");
                }
                else Console.WriteLine("Неверное кол-во");
            }
            else Console.WriteLine("Неверный метод");
        }
        else Console.WriteLine("Неверный ID");
    }

    static void ReportMenu()
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
    static void ShowReport()
    {
        Console.WriteLine(mainService.Report());
    }
    static void DownloadReport()
    {   
        var report = mainService.Report();
        var path = $"report_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
        File.WriteAllText(path, report);
        Console.WriteLine($"Отчет скачан в {path}");
    }

}
