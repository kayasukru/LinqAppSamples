using LinqAppSamples.DAL;

internal class Program
{
    private static void Main(string[] args)
    {
        // Üç adımda işlemler yapılır
        // 1. Verinin elde edilmesi (Obtain the data source)
        // 2. Sorgu oluşturma (Create the query)
        // 3. Sorgu çalıştırma (Execute the query)


        var context = new NorthwindContext();

        var employees = context.Employees;

        foreach (var item in employees)
        {
            Console.WriteLine($"{item.EmployeeId, -5} {item.FirstName, -15} {item.LastName, -15}");
        }




        // LINQ_Query_Syntax();
        // LINQ_Temel_Islemler();

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