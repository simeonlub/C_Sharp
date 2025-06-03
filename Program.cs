using System;
using System.Xml.Linq;

namespace Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите данные для расчета индекса массы тела (BMI).");
                if (BodyMassIndex())
                {
                    Console.WriteLine("Хотите ввести новые данные?");
                    string response = Console.ReadLine().ToLower();
                    Console.WriteLine();
                    if (response != "да")
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Попробуйте снова.");
                    Console.WriteLine();
                }
            }
        }
        public class Person
        {
            public string name;
            public int age;
            public double mass;
            public double height;

            public Person(string name, int age, double mass, double height)
            {
            this.name = name;
            this.age = age;
            this.mass = mass;
            this.height = height;
            }

            public double CalculateBMI()
            {
                // Формула для расчета индекса массы тела: BMI = масса (кг) / (рост (м) ^ 2)
                if (height <= 0)
                {
                    throw new ArgumentException("Рост должен быть положительным числом.", nameof(height));
                }
                return mass / (height * height);
            }

            public string ClassifyBMI(double bmi)
            {
                if (bmi < 18.5)
                {
                    return "недостаточная масса тела. Питайтесь больше.";
                }
                else if (bmi >= 18.5 && bmi < 24.9)
                {
                    return "нормальная масса тела.\r\n                                                    .+*******.                                      \r\n                                                   .***********.                                    \r\n                                                  .*****...*****                                    \r\n                                   .*.            .*****  .*****.                                   \r\n                              .  .****.           *****. .. ....                                    \r\n                           .=**+ .****.           *****.=******.      .                             \r\n                           .*****..****          .****-.+******.  .+*****+                          \r\n                      .*.    *****.****.         .****....****.  .*********+.                       \r\n                     ..***.   .****-***.          *****.=*****..****** ..***.                       \r\n                    ******.    .********          .**********=.*****. ..****.                       \r\n                  .******-.      .******.            .....***..****...*****-.                       \r\n                    .******.      .*****+      ...            .***=..*****..                        \r\n                      .******.      -****. ..*****.            .*********..                         \r\n              .:***:.  ..******.  .*****..********.              .-***+.      ..-+.                 \r\n             .********.  ..***+    .*....***-. .**:                       ..*********.              \r\n            .***..***. ***..:..        .***..  .***.                    .+************              \r\n            ***-..**=.*****            .**=     .**.                   .******:....***=             \r\n            ********.  .***            .**: .    ***                   .***+..  :*****:.            \r\n             +************..           .*** .    =***..                 ****.********..             \r\n              ..*********..             .***.     .****.                 **********... .            \r\n    .**...         ..--...               -***.      .***.                 .****...                  \r\n     **********.                         .+***.      .****.                     ........            \r\n    .+****************:.                  .=***        .****.              .**************.         \r\n        ..+*************                   .***.        ..***+             .**************.         \r\n             ..=********                     ***.          .***.           .***.      .***.         \r\n     .******************                     =**.           .***:           .****++************.    \r\n     =************-.....                     .***            ..***.         *******************     \r\n     -*****:.... .                 ..************              .***.        ***************+:..     \r\n     ...    .                  .+*********=.......               ****.            ... .  .          \r\n                             .****..... ..                        .************.                    \r\n                            .**..       ...:-:..                    .=**********                    \r\n                           .**+. .****************.                          .**.                   \r\n                           .**=.   .****....   .:***.                         ***.                  \r\n                           .***.                ..***..                       -**.                  \r\n                             ***.            .:******.                        .**.                  \r\n                             .****=.     .-******.****=.                      .**-                  \r\n                             .***************... .. .***.                      **-                  \r\n                             =**........ .. .    ...*****.                    .**..                 \r\n                             .***.            ..********.                     .**.                  \r\n                              .*******************....****                    ***.                  \r\n                                 .***********=..     ..****.           . .  ..**..                  \r\n                                 .**..            .******..         .:**********..                  \r\n                                  ****.........******.****.        .***********. .                  \r\n                                  .:*************.    ..***+      ****.                             \r\n                                     ..***........   ..+****+ ..****..                              \r\n                                      .***=.  .. ..******....=****.                                 \r\n                                        ***********************...                                  \r\n                                          ...-===......:==-... .                                    \r\n";
                }
                else if (bmi >= 25 && bmi < 29.9)
                {
                    return "избыточная масса тела. Питайтесь меньше.";
                }
                else
                {
                    return "ожирение. Обратитесь к врачу.";
                }
            }
        }
        static bool BodyMassIndex()
        {
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Имя должно состоять из символов.");
                return false;
            }

            Console.Write("Введите возраст: ");
            if (!int.TryParse(Console.ReadLine(), out int age) || age <= 0)
            {
                Console.WriteLine("Введите положительное целое число для возраста.");
                return false;
            }

            Console.Write("Введите массу (кг): ");
            if (!int.TryParse(Console.ReadLine(), out int mass) || mass <= 0)
            {
                Console.WriteLine("Введите положительное целое число для массы.");
                return false;
            }

            Console.Write("Введите рост (см): ");
            if (!double.TryParse(Console.ReadLine(), out double height) || height <= 0)
            {
                Console.WriteLine("Введите положительное число для роста.");
                return false;
            }
            // Преобразование роста из сантиметров в метры
            height /= 100;

            Person person = new Person(name, age, mass, height);
            double bmi = person.CalculateBMI();
            string classification = person.ClassifyBMI(bmi);

            Console.WriteLine();
            Console.WriteLine($"Масса: {person.mass} кг.");
            Console.WriteLine($"Рост: {height} м.");
            Console.WriteLine($"Индекс массы тела: {bmi:F2}.");
            Console.WriteLine();
            Console.WriteLine($"У вас {classification}");

            return true;
        }
    }
}