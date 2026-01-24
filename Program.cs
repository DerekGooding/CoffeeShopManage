using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

class Product
{
    public int Id {set; get;}
    public string Name {set; get;}
    public decimal Price {set; get;}
    public string Category {set; get;}
    public int VAT {set; get;}
    public bool IsActive {set; get;}
}

class Staff
{
    public int Id {set; get;}
    public string FullName {set; get;}
    public decimal Salary {set; get;}
    public int Age {set; get;}
    public string Position {set; get;}
}

class Sale
{
    public int Id {set; get;}
    public decimal Amount {set; get;}
    public DateAndTime CreatedAt {set; get;}

    public int ProductId {set; get;}
    public int Quantity {set; get;}

    public List<Product> Products {set; get;} = new();
}
