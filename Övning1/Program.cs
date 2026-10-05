using Övning1;

Usage();

EmployeeCollection employees = new();

while (true)
{
    Console.Write("> ");
    var input = Console.ReadLine();

    // ReSharper disable once ConvertIfStatementToSwitchStatement
    if (input is null or "quit")
    {
        return;
    }

    if (input is "list")
    {
        List();
    }
    else if (input.StartsWith("add ", StringComparison.InvariantCulture))
    {
        Add(input[4..]);
    }
    else
    {
        Usage();
    }
}

void Usage()
{
    Console.WriteLine("commands: list, add <FirstName>:<Surname>:<Salary>, quit, help");
}

void List()
{
    foreach (var employee in employees.GetAll().OrderBy(e => e.Surname))
    {
        Console.WriteLine($"{employee.GivenName} {employee.Surname}, {employee.Salary}kr");
    }
}

void Add(string input)
{
    try
    {
        employees.Add(Employee.Parse(input));
    }
    catch (ArgumentException e)
    {
        Console.WriteLine(e.Message);
        Usage();
    }
}