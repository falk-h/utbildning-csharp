namespace Övning4;

public class Person
{
    public string FirstName
    {
        get;
        private set => field = value.Length >= 3
            ? value
            : throw new ArgumentException("First name cannot contain fewer than 3 symbols!");
    }

    public string LastName
    {
        get;
        private set => field = value.Length >= 3
            ? value
            : throw new ArgumentException("Last name cannot contain fewer than 3 symbols!");
    }

    public int Age
    {
        get;
        private set => field = value > 0
            ? value
            : throw new ArgumentException("Age cannot be zero or a negative integer!");
    }

    public decimal Salary
    {
        get;
        private set => field = value >= 460
            ? value
            : throw new ArgumentException("Salary cannot be less than 460 dollars!");
    }

    public Person(string firstName, string lastName, int age, decimal salary)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Salary = salary;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName} gets {Salary:0.00} dollars.";
    }

    public void IncreaseSalary(decimal percentage)
    {
        if (Age < 30)
        {
            percentage /= 2;
        }

        Salary += Salary * percentage / 100;
    }
}