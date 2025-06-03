using System;
using System.Globalization;

namespace Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Triangle1();
            Triangle2();
            Calendar();
        }
        static void Triangle1()
        {
            Console.Write("Введите длину боковой стороны треугольника: ");
            int N;

            // Проверка корректности введенного значения
            if (!int.TryParse(Console.ReadLine(), out N) || N <= 0)
            {
                Console.WriteLine("Введите положительное целое число.");
                return;
            }

            // Вычисление высоты треугольника
            int H = N / 2 * 4 + N % 2;

            for (int i = 1; i <= H; i++)
            {
                /// <summary> Расстояние до середины треугольника </summary>
                int Middle = ((H / 2 + N % 2) / i);
                bool MiddleBool1 = Convert.ToBoolean(Middle);
                bool MiddleBool2 = Convert.ToBoolean(Middle) ^ true;
                int MiddleInt1 = Convert.ToInt32(MiddleBool1);
                int MiddleInt2 = Convert.ToInt32(MiddleBool2);

                /// <summary> Количество звёздочек в текущей строке </summary>
                int Stars = i * MiddleInt1 + (H - (i - (H % 2))) * MiddleInt2;

                // Вывод звездочек
                for (int j = 1; j <= Stars; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }
        static void Triangle2()
        {
            Console.Write("Введите длину боковой стороны треугольника: ");
            int N;

            // Проверка корректности введенного значения
            if (!int.TryParse(Console.ReadLine(), out N) || N <= 0)
            {
                Console.WriteLine("Введите положительное целое число. ");
                return;
            }

            // Вычисление высоты треугольника
            int H = N / 2 * 4 + N % 2;

            for (int i = 1; i <= H; i++)
            {
                /// <summary> Расстояние до середины треугольника </summary>
                int Middle = ((H / 2 + N % 2) / i);
                bool MiddleBool1 = Convert.ToBoolean(Middle);
                bool MiddleBool2 = Convert.ToBoolean(Middle) ^ true;
                int MiddleInt1 = Convert.ToInt32(MiddleBool1);
                int MiddleInt2 = Convert.ToInt32(MiddleBool2);

                /// <summary> Количество звёздочек в текущей строке </summary>
                int Stars = i * MiddleInt1 + (H - (i - (H % 2))) * MiddleInt2;

                /// <summary> Количество пробелов перед звездочками </summary>
                int Spaces = N - Stars;

                // Вывод пробелов перед звездочками
                for (int j = 1; j <= Spaces; j++)
                {
                    Console.Write(" ");
                }

                // Вывод звездочек
                for (int j = 1; j <= Stars; j++)
                {
                    Console.Write("*");
                }

                // Переход к следующей строке
                Console.WriteLine();
            }
        }
        static void Calendar()
        {
            const int Year = 2025;
            const string MonthString = "Август";
            const int Month = 8;
            DisplayCalendar(Year, Month);

            static void DisplayCalendar(int Year, int Month)
            {
                DateTime FirstDayOfMonth = new DateTime(Year, Month, 1);
                int DaysInMonth = DateTime.DaysInMonth(Year, Month);

                // Вывод заголовка
                Console.WriteLine($"   {MonthString}, {Year}");
                Console.WriteLine("-----------------");

                // Создание массива для хранения дней недели
                string[] WeekDays = new string[7];

                // Cмещение первого дня месяца
                int FirstDayIndex = ((int)FirstDayOfMonth.DayOfWeek + 6) % 7;


                // Заполнение массива строками
                for (int i = 0; i < 7; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (i < FirstDayIndex)
                            {
                                WeekDays[i] = $"ПН   ";
                            }
                            else {
                                WeekDays[i] = $"ПН";
                            }
                            break;
                        case 1:
                            if (i < FirstDayIndex)
                            {
                                WeekDays[i] = $"ВТ   ";
                            }
                            else
                            {
                                WeekDays[i] = $"ВТ";
                            }
                            break;
                        case 2:
                            if (i < FirstDayIndex)
                            {
                                WeekDays[i] = $"СР   ";
                            }
                            else
                            {
                                WeekDays[i] = $"СР";
                            }
                            break;
                        case 3:
                            if (i < FirstDayIndex)
                            {
                                WeekDays[i] = $"ЧТ   ";
                            }
                            else
                            {
                                WeekDays[i] = $"ЧТ";
                            }
                            break;
                        case 4:
                            if (i < FirstDayIndex)
                            {
                                WeekDays[i] = $"ПТ   ";
                            }
                            else
                            {
                                WeekDays[i] = $"ПТ";
                            }
                            break;
                        case 5:
                            if (i < FirstDayIndex)
                            {
                                WeekDays[i] = $"СБ   ";
                            }
                            else
                            {
                                WeekDays[i] = $"СБ";
                            }
                            break;
                        case 6:
                            if (i < FirstDayIndex)
                            {
                                WeekDays[i] = $"ВС   ";
                            }
                            else
                            {
                                WeekDays[i] = $"ВС";
                            }
                            break;
                    }
                }

                // Заполнение календаря
                for (int Day = 1; Day <= DaysInMonth; Day++)
                {
                    string DayStringLong = Day.ToString();
                    int DayLenght = DayStringLong.Length;
                    int Multipler = (DayLenght - 2) * (-1);
                    string SpaceOfDayString = new string(' ', Multipler);
                    string DayString = $"{SpaceOfDayString}{DayStringLong} ";
                    WeekDays[FirstDayIndex] += DayString;
                    FirstDayIndex = (FirstDayIndex + 1) % 7;
                }

                // Вывод строк календаря
                for (int i = 0; i < 7; i++)
                {
                    Console.WriteLine($"{WeekDays[i]}");
                }
            }
        }
    }
}
