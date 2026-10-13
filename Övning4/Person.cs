namespace Övning4;

public class Person
{
    private string firstName;
    private string lastName;
    private int age;
    private decimal salary;

    public string FirstName => firstName;
    public string LastName => lastName;
    public int Age => age;
    public decimal Salary => salary;

    public Person(string firstName, string lastName, int age, decimal salary)
    {
        if (firstName.Length < 3)
        {
            throw new ArgumentException("First name cannot contain fewer than 3 symbols!");
        }

        if (lastName.Length < 3)
        {
            throw new ArgumentException("Last name cannot contain fewer than 3 symbols!");
        }

        if (age <= 0)
        {
            throw new ArgumentException("Age cannot be zero or a negative integer!");
        }

        if (salary < 460)
        {
            throw new ArgumentException("Salary cannot be less than 460 dollars!");
        }

        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
        this.salary = salary;
    }

    public override string ToString()
    {
        return $"{firstName} {lastName} gets {salary:0.00} dollars.";
    }

    public void IncreaseSalary(decimal percentage)
    {
        if (age < 30)
        {
            percentage /= 2;
        }

        salary += salary * percentage / 100;
    }
}