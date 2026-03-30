namespace EmployeeManagementSystem.Models;

public class Manager : Employee
{
    public double Bonus { get; set; }
    public int NumberOfTeams { get; set; }

    public override double CalculateSalary()
    {
        return BaseSalary + Bonus;
    }

    public override void GenerateReport()
    {
        Console.WriteLine("--- Manager report ---");
        DisplayInfo();
        Console.WriteLine($"  Bonus: {Bonus:F2}, Teams: {NumberOfTeams}");
    }
}
