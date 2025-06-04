using System;
using System.Collections.Generic;
using System.Linq;

// Абстрактный базовый класс Employee
public abstract class Employee
{
    // Инкапсулированные поля
    private string name;
    private int age;
    protected double salary; // Защищенное поле, доступное в производных классах

    // Статическое поле для уникального ID
    private static int nextId = 1;

    // Конструктор
    public Employee(string name, int age, double salary)
    {
        Id = nextId++;
        this.Name = name;
        this.Age = age;
        this.salary = salary;
    }

    // Свойства для доступа к данным (инкапсуляция)
    public string Name
    {
        get => name;
        set => name = value;
    }

    public int Age
    {
        get => age;
        set => age = value;
    }

    public int Id { get; private set; }

    // Абстрактный метод для получения зарплаты
    public abstract double GetSalary();

    public override string ToString()
    {
        return $"ID: {Id}, Имя: {Name}, Возраст: {Age}, Зарплата: {GetSalary():C2}, Должность: {Position}";
    }

    // Свойство Position для указания должности
    public abstract string Position { get; }

    // Метод для изменения зарплаты
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
}

// Производный класс RegularEmployee (регулярный сотрудник)
public class RegularEmployee : Employee
{
    public RegularEmployee(string name, int age, double salary)
        : base(name, age, salary)
    {
    }

    public override double GetSalary()
    {
        // Возвращаем зарплату обычного сотрудника без изменений
        return base.salary;
    }

    public override string Position => "Регулярный сотрудник";
}

// Производный класс Manager (менеджер)
public class Manager : Employee
{
    protected double bonus; // Дополнительное поле для менеджера

    public Manager(string name, int age, double salary, double bonus)
        : base(name, age, salary)
    {
        this.bonus = bonus;
    }

    public override double GetSalary()
    {
        // Переопределяем метод для возврата зарплаты менеджера с бонусом
        return base.salary + bonus;
    }

    public override string Position => "Менеджер";

    public double Bonus
    {
        get => bonus;
        set
        {
            if (value >= 0)
            {
                bonus = value;
            }
            else
            {
                Console.WriteLine("Бонус не может быть отрицательным.");
            }
        }
    }
}

// Производный класс Director (директор)
public class Director : Manager
{
    protected double stockOptions; // Дополнительное поле для директора

    public Director(string name, int age, double salary, double bonus, double stockOptions)
        : base(name, age, salary, bonus)
    {
        this.stockOptions = stockOptions;
    }

    public override double GetSalary()
    {
        // Переопределяем метод для возврата зарплаты директора с учётом стоков
        return base.GetSalary() + stockOptions;
    }

    public override string Position => "Директор";

    public double StockOptions
    {
        get => stockOptions;
        set
        {
            if (value >= 0)
            {
                stockOptions = value;
            }
            else
            {
                Console.WriteLine("Права на приобретение акций не могут быть отрицательными.");
            }
        }
    }
}

// Класс Company представляет компанию игрока
public class Company
{
    private static Random random = new Random();
    private int companyRevenue;

    public List<Employee> Employees { get; private set; }
    public double Revenue { get; private set; }
    public double Expenses { get; private set; }
    public double Profit { get; private set; }

    // Конструктор
    public Company(double initialRevenue, double initialExpenses)
    {
        Employees = new List<Employee>();
        Revenue = initialRevenue;
        Expenses = initialExpenses;
        Profit = CalculateProfit();
    }

    // Метод для расчета прибыли
    private double CalculateProfit()
    {
        double totalSalary = Employees.Sum(emp => emp.GetSalary());
        return Revenue - totalSalary - Expenses;
    }

    // Метод для найма сотрудника
    public void HireEmployee(Employee employee)
    {
        Employees.Add(employee);
        Console.WriteLine($"Сотрудник {employee.Name} успешно нанят.");
    }

    // Метод для увольнения сотрудника
    public void FireEmployee(int id)
    {
        Employee emp = Employees.Find(e => e.Id == id);
        if (emp != null)
        {
            Employees.Remove(emp);
            Console.WriteLine($"Сотрудник {emp.Name} успешно уволен.");
            UpdateProfit();
        }
        else
        {
            Console.WriteLine("Сотрудник с таким ID не найден.");
        }
    }

    // Метод для изменения зарплаты сотрудника
    public void ChangeSalary(int id, double newSalary)
    {
        Employee emp = Employees.Find(e => e.Id == id);
        if (emp != null)
        {
            emp.SetSalary(newSalary);
            Console.WriteLine($"Зарплата сотрудника {emp.Name} изменена на {newSalary:C2}.");
            UpdateProfit();
        }
        else
        {
            Console.WriteLine("Сотрудник с таким ID не найден.");
        }
    }

    // Метод для изменения бонуса менеджера
    public void ChangeBonusForManager(int id, double newBonus)
    {
        Manager manager = Employees.Find(e => e is Manager && e.Id == id) as Manager;
        if (manager != null)
        {
            manager.Bonus = newBonus;
            Console.WriteLine($"Бонус менеджера {manager.Name} изменен на {newBonus:C2}.");
            UpdateProfit();
        }
        else
        {
            Console.WriteLine("Менеджер с таким ID не найден.");
        }
    }

    // Метод для изменения прав на приобретение акций директора
    public void ChangeStockOptionsForDirector(int id, double newStockOptions)
    {
        Director director = Employees.Find(e => e is Director && e.Id == id) as Director;
        if (director != null)
        {
            director.StockOptions = newStockOptions;
            Console.WriteLine($"Права на приобретение акций директора {director.Name} изменены на {newStockOptions:C2}.");
            UpdateProfit();
        }
        else
        {
            Console.WriteLine("Директор с таким ID не найден.");
        }
    }

    // Метод для симулирования ежемесячных событий
    public void SimulateMonthlyEvents()
    {
        Console.WriteLine("\nСимулирую ежемесячные события...");

        // Случайное изменение выручки компании от -10% до +10%
        double revenueChangeFactor = 1 + (random.NextDouble() * 0.2 - 0.1);
        double newRevenue = Revenue * revenueChangeFactor;
        double revenueChange = newRevenue - Revenue;
        Revenue = newRevenue;

        // Случайное изменение зарплаты каждого сотрудника от -5% до +5%
        foreach (Employee emp in Employees)
        {
            double salaryChangeFactor = 1 + (random.NextDouble() * 0.1 - 0.05);
            double newSalary = emp.GetSalary() * salaryChangeFactor;
            double salaryChange = newSalary - emp.GetSalary();

            emp.SetSalary(newSalary);
            Console.WriteLine($"{emp.Name}'s зарплата изменилась на {salaryChange:+#.00;-#.00;0}. Новая зарплата: {emp.GetSalary():C2}");
        }

        // Случайные события для менеджеров и директоров
        foreach (Employee emp in Employees)
        {
            if (emp is Manager manager)
            {
                bool performanceBoost = random.Next(2) == 0;
                if (performanceBoost)
                {
                    manager.Bonus *= 1.2;
                    Console.WriteLine($"{manager.Name} получил повышение бонуса. Новый бонус: {manager.Bonus:C2}");
                }
                else
                {
                    manager.Bonus *= 0.9;
                    Console.WriteLine($"{manager.Name} получил снижение бонуса. Новый бонус: {manager.Bonus:C2}");
                }

                bool salaryCut = random.Next(2) == 0;
                if (salaryCut)
                {
                    manager.SetSalary(manager.GetSalary() * 0.9);
                    Console.WriteLine($"{manager.Name} получил снижение зарплаты. Новая зарплата: {manager.GetSalary():C2}");
                }
                else
                {
                    manager.SetSalary(manager.GetSalary() * 1.05);
                    Console.WriteLine($"{manager.Name} получил повышение зарплаты. Новая зарплата: {manager.GetSalary():C2}");
                }
            }

            if (emp is Director director)
            {
                bool stockBonusReceived = random.Next(2) == 0;
                if (stockBonusReceived)
                {
                    director.StockOptions *= 1.2;
                    Console.WriteLine($"{director.Name} получил награду по правам на акции. Новые права: {director.StockOptions:C2}");
                }
                else
                {
                    director.StockOptions *= 0.9;
                    Console.WriteLine($"{director.Name} потерял часть прав на акции. Новые права: {director.StockOptions:C2}");
                }

                bool salaryCut = random.Next(2) == 0;
                if (salaryCut)
                {
                    director.SetSalary(director.GetSalary() * 0.9);
                    Console.WriteLine($"{director.Name} получил снижение зарплаты. Новая зарплата: {director.GetSalary():C2}");
                }
                else
                {
                    director.SetSalary(director.GetSalary() * 1.05);
                    Console.WriteLine($"{director.Name} получил повышение зарплаты. Новая зарплата: {director.GetSalary():C2}");
                }
            }
        }

        // Повышение зарплаты случайному регулярному сотруднику
        var regularEmployees = Employees.FindAll(e => e is RegularEmployee);
        if (regularEmployees.Count > 0)
        {
            int randomIndex = random.Next(regularEmployees.Count);
            RegularEmployee randomEmployee = regularEmployees[randomIndex] as RegularEmployee;

            double randomRaiseFactor = 1 + random.NextDouble() * 0.1;
            double newSalary = randomEmployee.GetSalary() * randomRaiseFactor;
            double salaryChange = newSalary - randomEmployee.GetSalary();

            randomEmployee.SetSalary(newSalary);
            Console.WriteLine($"{randomEmployee.Name} получил повышение зарплаты. Зарплата увеличилась на {salaryChange:C2}. Новая зарплата: {randomEmployee.GetSalary():C2}");
        }

        UpdateProfit();
    }

    // Метод для демонстрации списка всех сотрудников
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

    // Метод для получения текущей прибыли компании
    public void ShowProfit()
    {
        Console.WriteLine($"\nТекущая прибыль компании: {Profit:C2}");
    }

    // Обновление прибыли после изменения выручки или расходов
    private void UpdateProfit()
    {
        Profit = CalculateProfit();
        if (Profit < 0)
        {
            Console.WriteLine("\nВнимание! Ваша компания работает с убытками.");
        }
    }

    // Проверка состояния компании на банкротство
    public void CheckBankruptcy()
    {
        if (companyRevenue < 0 || Profit < 0)
        {
            Console.WriteLine("\nК сожалению, ваша компания ушла в банкротство. Игра окончена.");
            Environment.Exit(0);
        }
    }
}

// Класс для представления конкурентной компании
public class Competitor
{
    private static Random random = new Random();
    public string Name { get; private set; }
    public double Revenue { get; private set; }
    public double Expenses { get; private set; }
    public double Profit { get; private set; }

    // Конструктор
    public Competitor(string name, double initialRevenue, double initialExpenses)
    {
        Name = name;
        Revenue = initialRevenue;
        Expenses = initialExpenses;
        Profit = CalculateProfit();
    }

    // Метод для расчета прибыли
    private double CalculateProfit()
    {
        double totalSalary = random.Next(50000, 200000); // Случайные операционные затраты на зарплату
        double variableExpenses = random.Next(10000, 50000); // Случайные переменные операционные расходы
        return Revenue - totalSalary - variableExpenses - Expenses;
    }

    // Метод для симулирования ежемесячных событий конкурента
    public void SimulateMonthlyEvents()
    {
        // Случайное изменение выручки компании от -10% до +10%
        double revenueChangeFactor = 1 + (random.NextDouble() * 0.2 - 0.1);
        double newRevenue = Revenue * revenueChangeFactor;
        Revenue = newRevenue;

        // Пересчёт прибыли
        Profit = CalculateProfit();

        // Проверка на банкротство компании
        if (Profit < 0)
        {
            Console.WriteLine($"\nКонкурент {Name} ушел в банкротство.");
            Name += " (банкнот)";
        }

        Console.WriteLine($"\nКомпания {Name}:");
        Console.WriteLine($"Выручка компании изменилась на {(newRevenue - Revenue):+#.00;-#.00;0}. Новая выручка: {Revenue:C2}");
        Console.WriteLine($"Операционные расходы изменены. Новая операционная зарплата сотрудников: {random.Next(50000, 200000):C2}");
        Console.WriteLine($"Переменные операционные расходы изменены: {random.Next(10000, 50000):C2}");
        Console.WriteLine($"Чистая прибыль: {Profit:C2}");
    }

    // Получение информации о конкуренте
    public string GetInfo()
    {
        return $"Компания: {Name}, Выручка: {Revenue:C2}, Прибыль: {Profit:C2}";
    }
}

class GameMenu
{
    private static Company playerCompany;
    private static List<Competitor> competitors = new List<Competitor>();
    private static Random random = new Random();

    public static void Main(string[] args)
    {
        Console.WriteLine("Добро пожаловать в игру управления компанией!");

        // Инициализация игрока и конкурентов
        playerCompany = new Company(initialRevenue: 100000, initialExpenses: 50000);
        InitCompetitors();

        while (true)
        {
            Console.WriteLine("\nВыберите опцию:");
            Console.WriteLine("1. Нанять регулярного сотрудника");
            Console.WriteLine("2. Нанять менеджера");
            Console.WriteLine("3. Нанять директора");
            Console.WriteLine("4. Показать всех сотрудников");
            Console.WriteLine("5. Изменить зарплату");
            Console.WriteLine("6. Изменить бонус для менеджера");
            Console.WriteLine("7. Изменить права на приобретение акций для директора");
            Console.WriteLine("8. Уволить сотрудника");
            Console.WriteLine("9. Симулировать ежемесячные события");
            Console.WriteLine("10. Показать текущую прибыль компании");
            Console.WriteLine("11. Показать информацию о конкурентах");
            Console.WriteLine("12. Выйти");
            Console.Write("Введите ваш выбор: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    HireRegularEmployee();
                    break;
                case "2":
                    HireManager();
                    break;
                case "3":
                    HireDirector();
                    break;
                case "4":
                    playerCompany.ShowAllEmployees();
                    break;
                case "5":
                    ChangeSalary();
                    break;
                case "6":
                    ChangeBonusForManager();
                    break;
                case "7":
                    ChangeStockOptionsForDirector();
                    break;
                case "8":
                    FireEmployee();
                    break;
                case "9":
                    SimulateMonthlyEvents();
                    break;
                case "10":
                    playerCompany.ShowProfit();
                    break;
                case "11":
                    ShowCompetitorsInfo();
                    break;
                case "12":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                    break;
            }
        }
    }

    static void InitCompetitors()
    {
        competitors.Add(new Competitor("Компания А", initialRevenue: 120000, initialExpenses: 40000));
        competitors.Add(new Competitor("Компания Б", initialRevenue: 110000, initialExpenses: 45000));
        competitors.Add(new Competitor("Компания В", initialRevenue: 90000, initialExpenses: 55000));
    }

    static void HireRegularEmployee()
    {
        Console.Write("Введите имя: ");
        string name = Console.ReadLine();
        Console.Write("Введите возраст: ");
        int age = int.Parse(Console.ReadLine());
        Console.Write("Введите зарплату: ");
        double salary = double.Parse(Console.ReadLine());

        Employee emp = new RegularEmployee(name, age, salary);
        playerCompany.HireEmployee(emp);
    }

    static void HireManager()
    {
        Console.Write("Введите имя: ");
        string name = Console.ReadLine();
        Console.Write("Введите возраст: ");
        int age = int.Parse(Console.ReadLine());
        Console.Write("Введите зарплату: ");
        double salary = double.Parse(Console.ReadLine());
        Console.Write("Введите бонус: ");
        double bonus = double.Parse(Console.ReadLine());

        Employee emp = new Manager(name, age, salary, bonus);
        playerCompany.HireEmployee(emp);
    }

    static void HireDirector()
    {
        Console.Write("Введите имя: ");
        string name = Console.ReadLine();
        Console.Write("Введите возраст: ");
        int age = int.Parse(Console.ReadLine());
        Console.Write("Введите зарплату: ");
        double salary = double.Parse(Console.ReadLine());
        Console.Write("Введите бонус: ");
        double bonus = double.Parse(Console.ReadLine());
        Console.Write("Введите права на приобретение акций: ");
        double stockOptions = double.Parse(Console.ReadLine());

        Employee emp = new Director(name, age, salary, bonus, stockOptions);
        playerCompany.HireEmployee(emp);
    }

    static void ChangeSalary()
    {
        if (playerCompany.Employees.Count == 0)
        {
            Console.WriteLine("Сотрудников нет для изменения зарплаты.");
            return;
        }

        playerCompany.ShowAllEmployees();
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

        playerCompany.ChangeSalary(id, newSalary);
    }

    static void ChangeBonusForManager()
    {
        if (playerCompany.Employees.Count == 0)
        {
            Console.WriteLine("Менеджеров нет для изменения бонуса.");
            return;
        }

        var managers = playerCompany.Employees.FindAll(e => e is Manager);
        if (managers.Count == 0)
        {
            Console.WriteLine("Менеджеров нет.");
            return;
        }

        Console.WriteLine("\nСписок менеджеров:");
        foreach (var m in managers)
        {
            Console.WriteLine(m);
        }

        Console.Write("Введите ID менеджера, чей бонус хотите изменить: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Неправильный формат ID.");
            return;
        }

        Console.Write("Введите новый бонус: ");
        if (!double.TryParse(Console.ReadLine(), out double newBonus))
        {
            Console.WriteLine("Неправильный формат бонуса.");
            return;
        }

        playerCompany.ChangeBonusForManager(id, newBonus);
    }

    static void ChangeStockOptionsForDirector()
    {
        if (playerCompany.Employees.Count == 0)
        {
            Console.WriteLine("Директоров нет для изменения прав на приобретение акций.");
            return;
        }

        var directors = playerCompany.Employees.FindAll(e => e is Director);
        if (directors.Count == 0)
        {
            Console.WriteLine("Директоров нет.");
            return;
        }

        Console.WriteLine("\nСписок директоров:");
        foreach (var d in directors)
        {
            Console.WriteLine(d);
        }

        Console.Write("Введите ID директора, чьи права на приобретение акций хотите изменить: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Неправильный формат ID.");
            return;
        }

        Console.Write("Введите новые права на приобретение акций: ");
        if (!double.TryParse(Console.ReadLine(), out double newStockOptions))
        {
            Console.WriteLine("Неправильный формат прав на акции.");
            return;
        }

        playerCompany.ChangeStockOptionsForDirector(id, newStockOptions);
    }

    static void FireEmployee()
    {
        if (playerCompany.Employees.Count == 0)
        {
            Console.WriteLine("Сотрудников нет для увольнения.");
            return;
        }

        playerCompany.ShowAllEmployees();
        Console.Write("Введите ID сотрудника, которого хотите уволить: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Неправильный формат ID.");
            return;
        }

        playerCompany.FireEmployee(id);
    }

    static void SimulateMonthlyEvents()
    {
        playerCompany.SimulateMonthlyEvents();

        foreach (Competitor competitor in competitors)
        {
            competitor.SimulateMonthlyEvents();
        }

        playerCompany.CheckBankruptcy();
    }

    static void ShowCompetitorsInfo()
    {
        Console.WriteLine("\nИнформация о конкурентах:");
        foreach (Competitor competitor in competitors)
        {
            Console.WriteLine(competitor.GetInfo());
        }
        Console.WriteLine();
    }
}