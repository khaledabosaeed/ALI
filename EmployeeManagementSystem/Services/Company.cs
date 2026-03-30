using System.Collections;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Services;

public class Company
{
    private List<Employee> _employees = new List<Employee>();
    private List<Department> _departments = new List<Department>();
    private List<AttendanceRecord> _attendance = new List<AttendanceRecord>();
    private ArrayList _activityLog = new ArrayList();
    private int _nextEmployeeId = 1;

    public IReadOnlyList<Employee> Employees => _employees;
    public IReadOnlyList<Department> Departments => _departments;
    public IReadOnlyList<AttendanceRecord> Attendance => _attendance;

    public void AddEmployee(Employee employee)
    {
        if (employee.ID <= 0)
            employee.ID = _nextEmployeeId++;

        _employees.Add(employee);
        _activityLog.Add($"{DateTime.Now:yyyy-MM-dd HH:mm} — Added employee: {employee.Name} (ID {employee.ID})");
    }

    public bool RemoveEmployee(int id)
    {
        Employee? emp = SearchEmployee(id);
        if (emp == null)
            return false;

        _employees.Remove(emp);
        foreach (Department d in _departments)
            d.RemoveEmployee(id);

        _activityLog.Add($"{DateTime.Now:yyyy-MM-dd HH:mm} — Removed employee ID {id}");
        return true;
    }

    public Employee? SearchEmployee(int id)
    {
        foreach (Employee e in _employees)
        {
            if (e.ID == id)
                return e;
        }

        return null;
    }

    public void DisplayEmployees()
    {
        if (_employees.Count == 0)
        {
            Console.WriteLine("No employees yet.");
            return;
        }

        foreach (Employee e in _employees)
        {
            e.DisplayInfo();
            Console.WriteLine();
        }
    }

    public void CalculateAllSalaries()
    {
        if (_employees.Count == 0)
        {
            Console.WriteLine("No employees to calculate.");
            return;
        }

        double total = 0;
        foreach (Employee e in _employees)
        {
            double pay = e.CalculateSalary();
            Console.WriteLine($"{e.Name} (ID {e.ID}): {pay:F2}");
            total += pay;
        }

        Console.WriteLine($"Total payroll: {total:F2}");
    }

    public void AddDepartment(Department department)
    {
        _departments.Add(department);
        _activityLog.Add($"{DateTime.Now:yyyy-MM-dd HH:mm} — Added department: {department.DepartmentName}");
    }

    public bool AssignEmployeeToDepartment(int employeeId, int departmentId)
    {
        Employee? emp = SearchEmployee(employeeId);
        Department? dept = null;
        foreach (Department d in _departments)
        {
            if (d.DepartmentID == departmentId)
            {
                dept = d;
                break;
            }
        }

        if (emp == null || dept == null)
            return false;

        emp.Department = dept.DepartmentName;
        dept.AddEmployee(emp);
        _activityLog.Add($"{DateTime.Now:yyyy-MM-dd HH:mm} — Assigned employee {employeeId} to {dept.DepartmentName}");
        return true;
    }

    public void RecordAttendance(int employeeId)
    {
        if (SearchEmployee(employeeId) == null)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        try
        {
            var manager = new AttendanceManager();
            manager.CheckIn();
            manager.CheckOut();

            AttendanceRecord record = AttendanceRecord.Create(
                employeeId,
                DateTime.Today,
                manager.CheckInTime,
                manager.CheckOutTime);

            _attendance.Add(record);
            _activityLog.Add($"{DateTime.Now:yyyy-MM-dd HH:mm} — Attendance recorded for employee {employeeId}, {record.WorkingHours} h");
            Console.WriteLine($"Recorded: {record.WorkingHours} working hours on {record.Date:yyyy-MM-dd}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Could not record attendance: " + ex.Message);
        }
    }

    public void GenerateCompanyReport()
    {
        Console.WriteLine("========== COMPANY REPORT ==========");
        Console.WriteLine($"Total employees: {_employees.Count}");
        Console.WriteLine($"Total departments: {_departments.Count}");
        Console.WriteLine($"Attendance records: {_attendance.Count}");
        Console.WriteLine();

        Console.WriteLine("--- Departments (IReportable) ---");
        foreach (Department d in _departments)
            d.GenerateReport();

        Console.WriteLine();
        Console.WriteLine("--- Sample employee reports (IReportable) ---");
        foreach (Employee e in _employees)
            e.GenerateReport();

        Console.WriteLine();
        Console.WriteLine("--- Recent activity log (ArrayList) ---");
        int start = Math.Max(0, _activityLog.Count - 10);
        for (int i = start; i < _activityLog.Count; i++)
            Console.WriteLine($"  {_activityLog[i]}");
    }
}
