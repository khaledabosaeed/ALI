using EmployeeManagementSystem.Interfaces;

namespace EmployeeManagementSystem.Services;

public class AttendanceManager : IAttendance
{
    public DateTime CheckInTime { get; private set; }
    public DateTime CheckOutTime { get; private set; }

    public void CheckIn()
    {
        Console.Write("Enter check-in time (HH:mm, 24h): ");
        string? line = Console.ReadLine();
        CheckInTime = ParseTimeToday(line);
    }

    public void CheckOut()
    {
        Console.Write("Enter check-out time (HH:mm, 24h): ");
        string? line = Console.ReadLine();
        CheckOutTime = ParseTimeToday(line);
    }

    private static DateTime ParseTimeToday(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return DateTime.Today;

        if (TimeSpan.TryParse(input, out TimeSpan ts))
            return DateTime.Today.Add(ts);

        if (DateTime.TryParse(input, out DateTime dt))
            return dt;

        return DateTime.Today;
    }
}
