using CoffeShopManage.Model;

namespace CoffeShopManage.Service;

public class StaffService
{
    private readonly DefaultCrud<Staff> _staffCrud = new();

    public string FireStaff(int id)
    {
        var success = _staffCrud.Delete(id);
        return success ? $"Сотрудник {id} уволен" : "Сотрудника c таким id не существует";
    }

    public List<Staff>? ShowAllStaff() => _staffCrud.GetAll();

    public Staff AddNewStaff(string fullName, decimal salary, int age, string position)
    {
        var staff = new Staff(fullName, salary, age, position);
        return _staffCrud.Create(staff);
    }

    public Staff? GetOneStaff(int id) => _staffCrud.Get(id);

    public Staff? GetBestWorker() => _staffCrud.GetAll().OrderByDescending(s => s.Sales.Count).FirstOrDefault();
}
