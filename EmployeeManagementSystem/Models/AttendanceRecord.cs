namespace EmployeeManagementSystem.Models;

public struct AttendanceRecord
{
    public int EmployeeID { get; set; }
    public DateTime Date { get; set; }
    public DateTime CheckInTime { get; set; }
    public DateTime CheckOutTime { get; set; }
    public double WorkingHours { get; set; }

    public static AttendanceRecord Create(int employeeId, DateTime date, DateTime checkIn, DateTime checkOut)
    {
        double hours = (checkOut - checkIn).TotalHours;
        if (hours < 0)
            hours = 0;

        return new AttendanceRecord
        {
            EmployeeID = employeeId,
            Date = date.Date,
            CheckInTime = checkIn,
            CheckOutTime = checkOut,
            WorkingHours = Math.Round(hours, 2)
        };
    }
}
