using System;
using System.Collections.Generic;

internal class Program
{
    private static Company techCompany = new();
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Выберите опцию:");
            Console.WriteLine("1. Нанять сотрудника");
            Console.WriteLine("2. Изменить зарплату сотрудника");
            Console.WriteLine("3. Уволить сотрудника");
            Console.WriteLine("4. Показать всех сотрудников");
            Console.WriteLine("5. Выйти");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    HireEmployee();
                    break;
                case "2":
                    ChangeSalary();
                    break;
                case "3":
                    FireEmployee();
                    break;
                case "4":
                    techCompany.ShowAllEmployees();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                    break;
            }
        }
    }
    static void HireEmployee()
    {
        Console.WriteLine("\nВыберите должность:");
        Console.WriteLine("1. Разработчик программного обеспечения");
        Console.WriteLine("2. Менеджер проекта");
        Console.WriteLine("3. Генеральный директор");
        Console.Write("Введите ваш выбор: ");
        string positionChoice = Console.ReadLine();
        Console.Write("Введите имя: ");
        string name = Console.ReadLine();
        Console.Write("Введите возраст: ");
        if (!int.TryParse(Console.ReadLine(), out int age))
        {
            Console.WriteLine("Неправильный формат возраста.");
            return;
        }
        Console.Write("Введите зарплату: ");
        if (!double.TryParse(Console.ReadLine(), out double salary))
        {
            Console.WriteLine("Неправильный формат зарплаты.");
            return;
        }
        Employee? emp = null;
        switch (positionChoice)
        {
            case "1":
                emp = new Developer(name, age, salary);
                break;
            case "2":
                emp = new ProjectManager(name, age, salary);
                break;
            case "3":
                emp = new CEO(name, age, salary);
                break;
            default:
                Console.WriteLine("Неверная должность. Пожалуйста, попробуйте снова.");
                return;
        }

        techCompany.HireEmployee(emp);
    }
    static void ChangeSalary()
    {
        if (techCompany.Employees.Count == 0)
        {
            Console.WriteLine("Сотрудников нет для изменения зарплаты.");
            return;
        }
        Console.Write("Введите ID сотрудника, чью зарплату хотите изменить: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Неправильный формат ID.");
            return;
        }
        Console.Write("Введите новую зарплату: ");
        if (!double.TryParse(Console.ReadLine(), out double newSalary))
        {
            Console.WriteLine("Неправильный формат зарплаты.");
            return;
        }

        techCompany.ChangeSalary(id, newSalary);
    }
    static void FireEmployee()
    {
        if (techCompany.Employees.Count == 0)
        {
            Console.WriteLine("Сотрудников нет для увольнения.");
            return;
        }
        Console.Write("Введите ID сотрудника, которого хотите уволить: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Неправильный формат ID.");
            return;
        }

        techCompany.FireEmployee(id);
    }
}
public abstract class Employee
{
    private string name;
    private int age;
    protected double salary;
    private static int nextId = 1;

    public Employee(string name, int age, double salary)
    {
        Id = nextId++;
        this.name = name;
        this.age = age;
        this.salary = salary;
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public int Age
    {
        get { return age; }
        set { age = value; }
    }
    public int Id { get; private set; }

    public abstract double GetSalary();

    public abstract string Position { get; }

    public void SetSalary(double newSalary)
    {
        if (newSalary >= 0)
        {
            salary = newSalary;
        }
        else
        {
            Console.WriteLine("Зарплата не может быть отрицательной.");
        }
    }

    public override string ToString()
    {
        return $"ID: {Id}, Имя: {Name}, Возраст: {Age}, Зарплата: {GetSalary():C2}, Должность: {Position}";
    }
}
public class Developer : Employee
{
    public Developer(string name, int age, double salary)
        : base(name, age, salary)
    {
    }

    public override double GetSalary()
    {
        return salary;
    }

    public override string Position
    {
        get { return "Разработчик программного обеспечения"; }
    }
}
public class ProjectManager : Employee
{
    public ProjectManager(string name, int age, double salary)
        : base(name, age, salary)
    {
    }

    public override double GetSalary()
    {
        return salary;
    }

    public override string Position
    {
        get { return "Менеджер проекта"; }
    }
}
public class CEO : Employee
{
    public CEO(string name, int age, double salary)
        : base(name, age, salary)
    {
    }

    public override double GetSalary()
    {
        return salary;
    }

    public override string Position
    {
        get { return "Генеральный директор"; }
    }
}
public class Company
{
    public List<Employee> Employees { get; private set; }

    public Company()
    {
        Employees = new List<Employee>();
    }

    public void HireEmployee(Employee employee)
    {
        Employees.Add(employee);
        Console.WriteLine($"Сотрудник {employee.Name} успешно нанят.");
    }

    public void ChangeSalary(int id, double newSalary)
    {
        Employee emp = Employees.Find(e => e.Id == id);
        if (emp != null)
        {
            emp.SetSalary(newSalary);
            Console.WriteLine($"Зарплата сотрудника {emp.Name} изменена на {newSalary:C2}.");
        }
        else
        {
            Console.WriteLine("Сотрудник с таким ID не найден.");
        }
    }

    public void FireEmployee(int id)
    {
        Employee emp = Employees.Find(e => e.Id == id);
        if (emp != null)
        {
            Employees.Remove(emp);
            Console.WriteLine($"Сотрудник {emp.Name} успешно уволен.");
        }
        else
        {
            Console.WriteLine("Сотрудник с таким ID не найден.");
        }
    }

    public void ShowAllEmployees()
    {
        Console.WriteLine("\nСписок всех сотрудников:");
        if (Employees.Count == 0)
        {
            Console.WriteLine("Сотрудников нет.");
        }
        else
        {
            foreach (Employee emp in Employees)
            {
                Console.WriteLine(emp);
            }
        }
        Console.WriteLine();
    }
}