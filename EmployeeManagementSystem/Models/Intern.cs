namespace EmployeeManagementSystem.Models;

public class Intern : Employee
{
    public string University { get; set; } = string.Empty;
    public int TrainingDuration { get; set; }
    public double FixedAllowance { get; set; }

    public override double CalculateSalary()
    {
        return FixedAllowance;
    }

    public override void GenerateReport()
    {
        Console.WriteLine("--- Intern report ---");
        DisplayInfo();
        Console.WriteLine($"  University: {University}, Training (weeks): {TrainingDuration}");
        Console.WriteLine($"  Fixed Allowance: {FixedAllowance:F2}");
    }
}
