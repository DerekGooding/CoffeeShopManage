
using System.Reflection.Metadata.Ecma335;
using System.Runtime;
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
            var product = new Product(name, price, category, vat, isActive);
            return ProductCrud.Create(product);
        }
        public Product? GetProduct(int id)
        {
            return ProductCrud.Get(id);
        }
        public string ChangePrice(int id, int amount)
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
        public Sale AddNewSale(decimal amount, DateTime createdAt, int productId, int staffId, int Quantity)
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
