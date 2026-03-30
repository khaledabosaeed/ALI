using EmployeeManagementSystem.Interfaces;

namespace EmployeeManagementSystem.Models;

public class Department : IReportable
{
    public int DepartmentID { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public int ManagerID { get; set; }

    private List<Employee> _employees = new List<Employee>();

    public List<Employee> Employees => _employees;

    public void AddEmployee(Employee employee)
    {
        if (!_employees.Contains(employee))
            _employees.Add(employee);
    }

    public bool RemoveEmployee(int employeeId)
    {
        Employee? found = _employees.FirstOrDefault(e => e.ID == employeeId);
        if (found == null)
            return false;
        _employees.Remove(found);
        return true;
    }

    public void ListEmployees()
    {
        Console.WriteLine($"Department: {DepartmentName} (ID {DepartmentID})");
        if (_employees.Count == 0)
        {
            Console.WriteLine("  (no employees assigned)");
            return;
        }

        foreach (Employee e in _employees)
        {
            e.DisplayInfo();
            Console.WriteLine();
        }
    }

    public void GenerateReport()
    {
        Console.WriteLine($"[Department Report] {DepartmentName} | Manager ID: {ManagerID} | Staff count: {_employees.Count}");
    }
}
