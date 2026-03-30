using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem;

class Program
{
    static void Main(string[] args)
    {
        var company = new Company();
        bool running = true;

        Console.WriteLine("Employee Management System");
        Console.WriteLine("---------------------------");

        while (running)
        {
            PrintMenu();
            Console.Write("Choose option: ");

            try
            {
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    continue;

                int choice = int.Parse(input.Trim());

                switch (choice)
                {
                    case 1:
                        AddEmployeeMenu(company);
                        break;
                    case 2:
                        company.DisplayEmployees();
                        break;
                    case 3:
                        SearchEmployeeMenu(company);
                        break;
                    case 4:
                        DeleteEmployeeMenu(company);
                        break;
                    case 5:
                        AddDepartmentMenu(company);
                        break;
                    case 6:
                        AssignMenu(company);
                        break;
                    case 7:
                        AttendanceMenu(company);
                        break;
                    case 8:
                        company.CalculateAllSalaries();
                        break;
                    case 9:
                        company.GenerateCompanyReport();
                        break;
                    case 10:
                        running = false;
                        Console.WriteLine("Goodbye.");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Pick 1–10.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid number. Try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine();
        }
    }

    static void PrintMenu()
    {
        Console.WriteLine("1. Add Employee");
        Console.WriteLine("2. View Employees");
        Console.WriteLine("3. Search Employee");
        Console.WriteLine("4. Delete Employee");
        Console.WriteLine("5. Add Department");
        Console.WriteLine("6. Assign Employee To Department");
        Console.WriteLine("7. Record Attendance");
        Console.WriteLine("8. Calculate Salaries");
        Console.WriteLine("9. Generate Report");
        Console.WriteLine("10. Exit");
    }

    static void AddEmployeeMenu(Company company)
    {
        try
        {
            Console.WriteLine("Employee type: 1=Manager, 2=Developer, 3=Intern");
            Console.Write("Type: ");
            int type = int.Parse(Console.ReadLine()!.Trim());

            Console.Write("Name: ");
            string name = Console.ReadLine()!.Trim();

            Console.Write("Age: ");
            int age = int.Parse(Console.ReadLine()!.Trim());

            Console.Write("Department name (text): ");
            string dept = Console.ReadLine()!.Trim();

            Console.Write("Base salary: ");
            double baseSalary = double.Parse(Console.ReadLine()!.Trim());

            Console.Write("Hire date (yyyy-MM-dd): ");
            DateTime hire = DateTime.Parse(Console.ReadLine()!.Trim());

            Employee? emp = null;

            if (type == 1)
            {
                Console.Write("Bonus: ");
                double bonus = double.Parse(Console.ReadLine()!.Trim());
                Console.Write("Number of teams: ");
                int teams = int.Parse(Console.ReadLine()!.Trim());
                emp = new Manager
                {
                    Name = name,
                    Age = age,
                    Department = dept,
                    BaseSalary = baseSalary,
                    HireDate = hire,
                    Bonus = bonus,
                    NumberOfTeams = teams
                };
            }
            else if (type == 2)
            {
                Console.Write("Programming language: ");
                string lang = Console.ReadLine()!.Trim();
                Console.Write("Overtime hours: ");
                double otH = double.Parse(Console.ReadLine()!.Trim());
                Console.Write("Overtime rate: ");
                double otR = double.Parse(Console.ReadLine()!.Trim());
                emp = new Developer
                {
                    Name = name,
                    Age = age,
                    Department = dept,
                    BaseSalary = baseSalary,
                    HireDate = hire,
                    ProgrammingLanguage = lang,
                    OvertimeHours = otH,
                    OvertimeRate = otR
                };
            }
            else if (type == 3)
            {
                Console.Write("University: ");
                string uni = Console.ReadLine()!.Trim();
                Console.Write("Training duration (weeks): ");
                int weeks = int.Parse(Console.ReadLine()!.Trim());
                Console.Write("Fixed allowance: ");
                double allowance = double.Parse(Console.ReadLine()!.Trim());
                emp = new Intern
                {
                    Name = name,
                    Age = age,
                    Department = dept,
                    BaseSalary = baseSalary,
                    HireDate = hire,
                    University = uni,
                    TrainingDuration = weeks,
                    FixedAllowance = allowance
                };
            }
            else
            {
                Console.WriteLine("Unknown type.");
                return;
            }

            company.AddEmployee(emp);
            Console.WriteLine("Employee added.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input while adding employee. Use numbers/dates as shown.");
        }
    }

    static void SearchEmployeeMenu(Company company)
    {
        try
        {
            Console.Write("Employee ID: ");
            int id = int.Parse(Console.ReadLine()!.Trim());
            Employee? e = company.SearchEmployee(id);
            if (e == null)
                Console.WriteLine("Not found.");
            else
                e.DisplayInfo();
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    static void DeleteEmployeeMenu(Company company)
    {
        try
        {
            Console.Write("Employee ID to delete: ");
            int id = int.Parse(Console.ReadLine()!.Trim());
            if (company.RemoveEmployee(id))
                Console.WriteLine("Removed.");
            else
                Console.WriteLine("Employee not found.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    static void AddDepartmentMenu(Company company)
    {
        try
        {
            Console.Write("Department ID: ");
            int did = int.Parse(Console.ReadLine()!.Trim());
            Console.Write("Department name: ");
            string dname = Console.ReadLine()!.Trim();
            Console.Write("Manager employee ID (reference): ");
            int mid = int.Parse(Console.ReadLine()!.Trim());

            var d = new Department
            {
                DepartmentID = did,
                DepartmentName = dname,
                ManagerID = mid
            };
            company.AddDepartment(d);
            Console.WriteLine("Department added.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid number for ID.");
        }
    }

    static void AssignMenu(Company company)
    {
        try
        {
            Console.Write("Employee ID: ");
            int eid = int.Parse(Console.ReadLine()!.Trim());
            Console.Write("Department ID: ");
            int did = int.Parse(Console.ReadLine()!.Trim());
            if (company.AssignEmployeeToDepartment(eid, did))
                Console.WriteLine("Assigned.");
            else
                Console.WriteLine("Could not assign (check IDs).");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    static void AttendanceMenu(Company company)
    {
        try
        {
            Console.Write("Employee ID: ");
            int id = int.Parse(Console.ReadLine()!.Trim());
            company.RecordAttendance(id);
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid ID.");
        }
    }
}
