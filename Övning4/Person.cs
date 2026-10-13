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
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
        this.salary = salary;
    }

    public override string ToString()
    {
        return $"{firstName} {lastName} receives {salary:0.00} dollars.";
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