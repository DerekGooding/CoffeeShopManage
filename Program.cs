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
            Console.WriteLine("\nНажмите ентер...");
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
                Console.WriteLine($"Название: {p.Name} | Цена: {p.Price} рублей | Категория: {p.Category} | НДС: {p.VAT}%");

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
            
        }
    }    
    
}