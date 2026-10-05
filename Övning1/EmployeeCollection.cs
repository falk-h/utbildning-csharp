namespace Övning1;

public class EmployeeCollection
{
    private readonly List<Employee> _employees = [];

    public void Add(Employee employee)
    {
        _employees.Add(employee);
    }

    public IEnumerable<Employee> GetAll()
    {
        return _employees;
    }
}