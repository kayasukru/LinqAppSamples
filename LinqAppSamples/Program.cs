using LinqAppSamples.DAL;

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



        //LINQ_Sinif_Tanimi_Ile_Anonim_Cikti_Alma();

        //LINQ_Anonim_Cikti_Alma();

        //LINQ_Coklu_Alan_Ciktisi();

        //LINQ_Tek_Alan_Secme();

        //LINQ_Temel_Sorgu();

        //LINQ_Logging();

        //LINQ_Query_Syntax();

        //LINQ_Temel_Islemler();
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