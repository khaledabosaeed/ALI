using EmployeeManagementSystem.Interfaces;

namespace EmployeeManagementSystem.Models;

public abstract class Employee : IReportable
{
    private int _id;
    private string _name = string.Empty;
    private int _age;
    private string _department = string.Empty;
    private double _baseSalary;
    private DateTime _hireDate;

    public int ID
    {
        get => _id;
        set => _id = value;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public int Age
    {
        get => _age;
        set => _age = value;
    }

    public string Department
    {
        get => _department;
        set => _department = value;
    }

    public double BaseSalary
    {
        get => _baseSalary;
        set => _baseSalary = value;
    }

    public DateTime HireDate
    {
        get => _hireDate;
        set => _hireDate = value;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"  ID: {ID}, Name: {Name}, Age: {Age}");
        Console.WriteLine($"  Department: {Department}, Hire Date: {HireDate:yyyy-MM-dd}");
        Console.WriteLine($"  Base Salary: {BaseSalary:F2}, Calculated Pay: {CalculateSalary():F2}");
    }

    public abstract double CalculateSalary();

    public virtual void GenerateReport()
    {
        Console.WriteLine("--- Employee report ---");
        DisplayInfo();
    }
}
