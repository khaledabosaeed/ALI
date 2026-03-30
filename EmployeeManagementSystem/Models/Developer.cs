namespace EmployeeManagementSystem.Models;

public class Developer : Employee
{
    public string ProgrammingLanguage { get; set; } = string.Empty;
    public double OvertimeHours { get; set; }
    public double OvertimeRate { get; set; }

    public override double CalculateSalary()
    {
        return BaseSalary + (OvertimeHours * OvertimeRate);
    }

    public override void GenerateReport()
    {
        Console.WriteLine("--- Developer report ---");
        DisplayInfo();
        Console.WriteLine($"  Language: {ProgrammingLanguage}, Overtime: {OvertimeHours} h @ {OvertimeRate:F2}/h");
    }
}
