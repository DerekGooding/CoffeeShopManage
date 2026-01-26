using System.Security.Cryptography.X509Certificates;

public class DefaultCrud<T> where T: class 
{
    public T? Get(int id)
    {
        using var context = new AppDbContext();
        return context.Set<T>().Find(id);
    }
    public List<T> GetAll()
    {
        using var context = new AppDbContext();
        return context.Set<T>().ToList();
    }
    public T Create(T entity)
    {
        using var context = new AppDbContext();
        context.Set<T>().Add(entity);
        context.SaveChanges();
        return entity;

    }
    public bool Delete(int id)
    {
        using var context = new AppDbContext();
        var obj = context.Set<T>().Find(id);
        if (obj == null) {return false;}
        context.Set<T>().Remove(obj);
        context.SaveChanges();
        return true;
    }
    public T? Update(int id, T entity)
    {
        using var context = new AppDbContext();
        var obj = context.Set<T>().Find(id);
        if (obj == null) {return null;}
        context.Entry(obj).CurrentValues.SetValues(entity);
        context.SaveChanges();
        return obj;
    }
}   

// class ProductCrud
// {
    
// }

// class StaffCrud
// {
    
// }

// class WarehouseCrud
// {
    
// }

// class SalesCrud
// {
    
// }

// class AnalyticsCrud
// {
    
// }
