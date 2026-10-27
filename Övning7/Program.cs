namespace LinqOvningar;

public class Student
{
    public int Id { get; set; }
    public string Namn { get; set; } = "";
    public int Alder { get; set; }
    public string Klass { get; set; } = "";
    public int Poang { get; set; }

    public override string ToString() => $"{Id}: {Namn}, {Alder} år, klass {Klass}, {Poang} Poäng";
}

static class Program
{
    static void Main(string[] args)
    {
        // Exempeldata
        List<Student> students = new List<Student>
        {
            new() { Id = 1, Namn = "Anna", Alder = 19, Klass = "A", Poang = 85 },
            new() { Id = 2, Namn = "Bertil", Alder = 22, Klass = "B", Poang = 72 },
            new() { Id = 3, Namn = "Cecilia", Alder = 20, Klass = "A", Poang = 91 },
            new() { Id = 4, Namn = "David", Alder = 24, Klass = "C", Poang = 64 },
            new() { Id = 5, Namn = "Eva", Alder = 19, Klass = "B", Poang = 85 },
            new() { Id = 6, Namn = "Felix", Alder = 21, Klass = "A", Poang = 77 },
            new() { Id = 7, Namn = "Gustav", Alder = 23, Klass = "C", Poang = 58 },
            new() { Id = 8, Namn = "Hanna", Alder = 20, Klass = "B", Poang = 95 }
        };
        Console.WriteLine("LINQ-ÖVNINGAR MED STUDENTER\n");

        Console.WriteLine("alla studenter som är äldre än 20:");
        Print(students.Where(s => s.Alder > 20));
        Console.WriteLine("alla studenter som har mer än 80 poäng:");
        Print(students.Where(s => s.Poang > 80));

        Console.WriteLine("bara namnen på alla studenter:");
        Print(students.Select(s => s.Namn));
        Console.WriteLine("namn och poäng i en ny anonym typ:");
        Print(students.Select(s => (s.Namn, s.Poang)));

        Console.WriteLine("studenter efter ålder:");
        Print(students.OrderBy(s => s.Alder));
        Console.WriteLine("studenter efter poäng, högst först:");
        Print(students.OrderByDescending(s => s.Poang));

        Console.WriteLine($"{students.Count()} studenter finns");
        Console.WriteLine($"{students.Count(s => s.Klass == "A")} studenter går i klass A");

        Console.WriteLine("De tre första studenterna:");
        Print(students.Take(3));
        Console.WriteLine("De två bästa poängen efter sortering:");
        Print(students.Select(s => s.Poang).OrderDescending().Take(2));

        Console.WriteLine("alla unika klassnamn:");
        Print(students.Select(s => s.Klass).Distinct());
        Console.WriteLine("alla unika åldrar:");
        Print(students.Select(s => s.Alder).Distinct());

        Console.WriteLine($"någon student med 100 poäng? {students.Any(s => s.Poang == 100)}");
        Console.WriteLine($"har alla studenter minst 50 poäng? {students.All(s => s.Poang >= 50)}");

        Console.WriteLine("första studenten i klass B:");
        Console.WriteLine(students.FirstOrDefault(s => s.Klass == "B"));
        Console.WriteLine("sista studenten i listan:");
        Console.WriteLine(students.Last());
        Console.WriteLine("studenten med ID 1");
        Console.WriteLine(students.Single(s => s.Id == 1));

        Console.WriteLine("studenter per klass:");
        Print(students.GroupBy(s => s.Klass).SelectMany(g => g));
        Console.WriteLine("klass och namnen i gruppen:");
        foreach (var grouping in students.GroupBy(s => s.Klass))
        {
            Console.WriteLine($"{grouping.Key}:");
            foreach (var name in grouping.Select(s => s.Namn))
            {
                Console.WriteLine($"  {name}");
            }
        }

        Console.WriteLine($"\nalla poäng: {students.Sum(s => s.Poang)}");
        Console.WriteLine($"medelpoäng: {students.Average(s => s.Poang)}");
        Console.WriteLine($"lägsta poäng: {students.Min(s => s.Poang)}");
        Console.WriteLine($"högsta poäng: {students.Max(s => s.Poang)}");
    }

    static void Print<T>(IEnumerable<T> things)
    {
        foreach (var thing in things)
        {
            Console.WriteLine(thing);
        }

        Console.WriteLine();
    }
}