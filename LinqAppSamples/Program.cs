using LinqAppSamples.DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections;

class EmployeeDTO
{
    public int Id { get; set; }
    public string FullName { get; set; }
}
internal class Program
{
    private static void Main(string[] args)
    {
        // Üç adımda işlemler yapılır
        // 1. Verinin elde edilmesi (Obtain the data source)
        // 2. Sorgu oluşturma (Create the query)
        // 3. Sorgu çalıştırma (Execute the query)

        LINQ_join_Islemleri();
        Console.WriteLine();

        LINQ_Islemleri_3();
        Console.WriteLine();

        LINQ_Islemleri2();
        Console.WriteLine();

        LINQ_OfType();
        Console.WriteLine();

        LINQ_Where();
        Console.WriteLine();

        LINQ_Sinif_Tanimi_Ile_Anonim_Cikti_Alma();
        Console.WriteLine();

        LINQ_Anonim_Cikti_Alma();
        Console.WriteLine();

        LINQ_Coklu_Alan_Ciktisi();
        Console.WriteLine();

        LINQ_Tek_Alan_Secme();
        Console.WriteLine();

        LINQ_Temel_Sorgu();
        Console.WriteLine();

        LINQ_Logging();
        Console.WriteLine();

        LINQ_Query_Syntax();
        Console.WriteLine();

        LINQ_Temel_Islemler();
    }

    private static void LINQ_join_Islemleri()
    {
        var context = new NorthwindContext();

        // query syntax
        var query1 = from p in context.Products
                     join c in context.Categories on p.CategoryId equals c.CategoryId
                     select new
                     {
                         Product = p,
                         Category = c
                     };

        // method syntax
        var query2 = context.Products
            .Include(p => p.Category) //LEFT JOIN işlemi yapar
            .Where(p => p.CategoryId != null)
            .ToList();

        foreach (var item in query1)
        {
            Console.WriteLine($"{item.Product.ProductId,-5} {item.Product.ProductName,-35} {item.Category.CategoryId,-5} {item.Category.CategoryName}");
        }
    }

    private static void LINQ_Islemleri_3()
    {
        var context = new NorthwindContext();

        //query syntax
        var query1 = from p in context.Products
                     where p.ProductName.Contains("rt")
                     orderby p.ProductName
                     select new Product
                     {
                         ProductId = p.ProductId,
                         ProductName = p.ProductName,
                         UnitPrice = p.UnitPrice,
                         UnitsInStock = p.UnitsInStock,
                     };

        // method syntax 
        var query2 = context.Products
            .Where(p => p.ProductName.Contains("rt"))
            .OrderBy(p => p.ProductName)
            .ThenByDescending(p => p.UnitsInStock)
            .Select(p => new Product
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock
            });

        foreach (var item in query2)
        {
            Console.WriteLine($"{item.ProductId,-10} {item.ProductName,-35} {item.UnitPrice,-15} {item.UnitsInStock,-10}");
        }
    }

    private static void LINQ_Islemleri2()
    {
        // query syntax ile filtreleme
        var context = new NorthwindContext();
        var query1 = from prod in context.Products
                     where prod.UnitPrice > 50 && prod.UnitsInStock <= 45
                     select new Product
                     {
                         ProductId = prod.ProductId,
                         ProductName = prod.ProductName,
                         UnitPrice = prod.UnitPrice,
                         UnitsInStock = prod.UnitsInStock,
                     };

        // metod syntax ile filtreleme
        var query2 = context.Products
            .Where(p => p.UnitPrice > 50 && p.UnitsInStock <= 45)
            .OrderByDescending(p => p.UnitsInStock)
            .Take(5);

        foreach (var item in query2)
        {
            Console.WriteLine($"{item.ProductId,-5} {item.ProductName,-35} {item.UnitPrice,-15} {item.UnitsInStock,-12}");
        }
    }

    private static void LINQ_OfType()
    {
        var list = new ArrayList
        {
            "Şükrü", "Bilge", "Seher", "Can",
            11,25,43,55,62,78,
            true, false,
            DateTime.Now,
            DateTime.Now.AddDays(3),
            DateTime.Now.AddMonths(7)
        };
        // tüm liste elemanları
        Console.WriteLine("Tüm liste elemanları:");
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        //integer değerleri al
        Console.WriteLine("Listenin integer elemanları:");
        var filteredData1 = list.OfType<int>();
        foreach (var item in filteredData1)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        //DateTime değerleri al
        Console.WriteLine("Listenin DateTime elemanları:");
        var filteredData2 = list.OfType<DateTime>();
        foreach (var item in filteredData2)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        //Metod ile filtreleme
        Console.WriteLine("Metod ile filtreleme - boolean elemalar:");
        var filteredData3 = GenericList<bool>(list);
        foreach (var item in filteredData3)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        //Metod ile filtreleme
        Console.WriteLine("Metod ile filtreleme - string elemalar:");
        var filteredData4 = GenericList<string>(list);
        foreach (var item in filteredData4)
        {
            Console.WriteLine(item);
        }
    }

    private static void LINQ_Where()
    {
        var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        //karşılaştırma
        var filteredNumbers1 = list.Where(num => num > 8);

        //karşılaştırma (Metodla)
        var filteredNumbers2 = list.Where(num => CheckNumber(num));

        // Func ile predicate karşılaştırma
        Func<int, bool> predicate = i => i > 8;
        var filteredNumbers3 = list.Where(predicate);

        foreach (var item in filteredNumbers1)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        foreach (var item in filteredNumbers2)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        foreach (var item in filteredNumbers3)
        {
            Console.WriteLine(item);
        }
    }

    internal static List<T> GenericList<T>(IEnumerable arr)
    {
        var list = new List<T>();
        foreach (var item in arr)
        {
            if (item is T)
            {
                list.Add((T)item);
            }
        }
        return list;
    }

    private static bool CheckNumber(int number)
    {
        if (number > 8)
            return true;
        return false;
    }

    private static void LINQ_Sinif_Tanimi_Ile_Anonim_Cikti_Alma()
    {
        var context = new NorthwindContext();

        // Sınıf tanımı ile anonim çıktı alma

        //query syntax
        var query = from emp in context.Employees
                    select new EmployeeDTO
                    {
                        Id = emp.EmployeeId,
                        FullName = emp.FirstName + " " + emp.LastName
                    };

        //method syntax
        var mtdQuery = context.Employees.Select(emp => new EmployeeDTO
        {
            Id = emp.EmployeeId,
            FullName = emp.FirstName + " " + emp.LastName
        });

        foreach (var item in mtdQuery)
        {
            Console.WriteLine(item.Id + " " + item.FullName);
        }
    }

    private static void LINQ_Anonim_Cikti_Alma()
    {
        var context = new NorthwindContext();

        // anonim çıktı alma

        //query syntax
        var query = from emp in context.Employees
                    select new
                    {
                        Id = emp.EmployeeId,
                        Fullname = emp.FirstName + " " + emp.LastName
                    };

        //method syntax
        var mtdQuery = context.Employees.Select(emp => new
        {
            Id = emp.EmployeeId,
            Fullname = emp.FirstName + " " + emp.LastName
        });

        foreach (var item in query)
        {
            Console.WriteLine(item.Id + " " + item.Fullname);
        }
    }

    private static void LINQ_Coklu_Alan_Ciktisi()
    {
        var context = new NorthwindContext();

        // birden fazla alan seçerek çıktı alma
        // query syntax
        var query = from emp in context.Employees
                    select new Employee
                    {
                        EmployeeId = emp.EmployeeId,
                        FirstName = emp.FirstName,
                        LastName = emp.LastName,
                    };

        //method syntax
        //var query = context.Employees.Select(emp => new Employee
        //{
        //    EmployeeId = emp.EmployeeId,
        //    FirstName = emp.FirstName,
        //    LastName = emp.LastName
        //});


        foreach (var item in query)
        {
            Console.WriteLine($"{item.EmployeeId,-5} {item.FirstName,-15} {item.LastName,-15}");
        }
    }

    private static void LINQ_Tek_Alan_Secme()
    {
        var context = new NorthwindContext();
        // bir alan seçerek çıktı

        //query syntaxı 
        var query = from emp in context.Employees
                    select emp.LastName;

        // method syntaxı
        // var query = context.Employees.Select(emp => emp.LastName);
        foreach (var item in query)
        {
            Console.WriteLine(item);
        }
    }

    private static void LINQ_Temel_Sorgu()
    {
        var context = new NorthwindContext();

        var query = from emp in context.Employees
                    select emp;
        //Yukarıdaki query syntax'ın aynı sonucunu aşağıdaki method syntax'ı da verir
        //var query = context.Employees;

        foreach (var em in query)
        {
            Console.WriteLine($"{em.EmployeeId,-5} {em.FirstName,-15} {em.LastName,-15}");
        }
    }

    private static void LINQ_Logging()
    {
        var context = new NorthwindContext();

        var employees = context.Employees;

        foreach (var item in employees)
        {
            Console.WriteLine($"{item.EmployeeId,-5} {item.FirstName,-15} {item.LastName,-15}");
        }
    }

    private static void LINQ_Query_Syntax()
    {
        // Üç adımda işlemler yapılır
        // 1. Verinin elde edilmesi (Obtain the data source)
        // 2. Sorgu oluşturma (Create the query)
        // 3. Sorgu çalıştırma (Execute the query)


        // (1) veri
        var list = new List<string>
        {
            "Şükrü", "Mira", "Bilge", "Kağan", "Mehmet"
        };

        // (2) query creation
        // SQL : Select EmployeId, FirstName FROM Employees

        // (2.1) Query syntax
        var querySyntax = from name in list
                          where name.Contains("e") // e ifadesini içeren isimler
                          select name;

        // (2.2) Method Syntax
        var methodSyntax = list.Where(name => name.Contains("e"));

        // (2.3) Mix syntax
        var mixSyntax = (from name in list
                         select name)
                        .Where(name => name.Contains("e"))
                        .OrderBy(name => name);


        // (3) Query execute

        Console.WriteLine("Linq İle işlemler");
        Console.WriteLine();
        // query syntax
        Console.WriteLine("-query syntax");
        foreach (var item in querySyntax)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        //method syntax
        Console.WriteLine("-method syntax");
        foreach (var item in methodSyntax)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        //mix syntax
        Console.WriteLine("-mix syntax");
        foreach (var item in mixSyntax)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        Console.WriteLine("---------------------------------------");

        Console.WriteLine("Listeleme");

        LINQ_Temel_Islemler();
    }

    private static void LINQ_Temel_Islemler()
    {
        //(1) data source
        var numbers = new int[] { 1, 2, 3, 4, 5, 6, 7 };

        //(2) query creation
        var list = new List<int>();

        foreach (var item in numbers)
        {
            //çift sayılar list'e eklenir
            if (item % 2 == 0)
            {
                list.Add(item);
            }
        }
        // (3) query execution
        foreach (var item in list)
        {
            Console.Write(item.ToString() + ", ");
        }
        //foreach'in kısaltması
        Console.WriteLine();
        list.ForEach(n => Console.Write($"{n}" + ", "));

        Console.WriteLine();

        // (2) query creation LINQ ile
        var query = from num in numbers
                    select num;

        // (3) query execution
        // 1. yöntem
        query.ToList().ForEach(query => Console.Write(query.ToString() + ", "));

        Console.WriteLine();
        // 2. yöntem
        foreach (var item in query)
        {
            Console.Write($"{item,-3}");
        }
    }
}