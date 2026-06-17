namespace CoffeShopManage.Model;

public class Staff
{
    public int Id { set; get; }
    public string FullName { set; get; } = "Имя не указано";
    public decimal Salary { set; get; }
    public int Age { set; get; }
    public string Position { set; get; } = null!;

    public List<Sale> Sales { set; get; } = new();

    public Staff()
    { }

    public Staff(string fullName, decimal salary, int age, string position)
    {
        FullName = fullName;
        Salary = salary;
        Age = age;
        Position = position;
    }
}
