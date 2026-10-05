namespace Övning1;

public class Employee
{
    public required string GivenName
    {
        get;
        init
        {
            var trimmed = value.Trim();
            field = trimmed.Length > 0
                ? trimmed
                : throw new ArgumentException("given name can't be empty");
        }
    }

    public required string Surname
    {
        get;
        init
        {
            var trimmed = value.Trim();
            field = trimmed.Length > 0
                ? trimmed
                : throw new ArgumentException("surname can't be empty");
        }
    }

    public required decimal Salary
    {
        get;
        init => field = value >= 0
            ? value
            : throw new ArgumentOutOfRangeException(nameof(Salary), "salary can't be negative");
    }

    public static Employee Parse(string input)
    {
        var parts = input.Split(':');
        if (parts.Length != 3)
        {
            throw new ArgumentException("wrong number of :");
        }

        if (!decimal.TryParse(parts[2], out var salary))
        {
            throw new ArgumentException("couldn't parse salary");
        }

        return new Employee
        {
            GivenName = parts[0],
            Surname = parts[1],
            Salary = salary
        };
    }
}