using System;
using System.Text;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("=== Бібліотека ===");
                Console.WriteLine("1. Довідка про програму");
                Console.WriteLine("2. Вартість доставки книг");
                Console.WriteLine("3. Таблиця видач за жанрами і днями");
                Console.WriteLine("0. Вихід");
                Console.Write("Ваш вибір: ");
                string choice = Console.ReadLine() ?? "";
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowAbout();
                        Pause();
                        break;
                    case "2":
                        ShowCostTable();
                        Pause();
                        break;
                    case "3":
                        Console.WriteLine("Цей пункт ще не готовий.");
                        Pause();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("Роботу завершено.");
                        break;
                    default:
                        Console.WriteLine("Немає такого пункту меню.");
                        Pause();
                        break;
                }
            }
        }

        static void ShowAbout()
        {
            Console.WriteLine("Тема проєкту: Бібліотека");
            Console.WriteLine("Головна сутність: книга");
            Console.WriteLine("Поля книги:");
            Console.WriteLine("Інвентарний номер — int");
            Console.WriteLine("Назва — string");
            Console.WriteLine("Автор — string");
            Console.WriteLine("Рік видання — int");
            Console.WriteLine("Жанр — string");
            Console.WriteLine("Кількість примірників — int");
            Console.WriteLine("Чи видана зараз — bool");
            Console.WriteLine("Дата видачі — DateTime (для контролю 14 днів)");
            Console.WriteLine("Майбутній підсумок: книги за жанрами і прострочені видачі.");
        }

        static void ShowCostTable()
        {
            Console.WriteLine("Таблиця вартості доставки книг за вагою посилки");
            Console.Write("Початкова вага, кг: ");
            bool ok1 = double.TryParse(Console.ReadLine(), out double from);
            Console.Write("Кінцева вага, кг: ");
            bool ok2 = double.TryParse(Console.ReadLine(), out double to);
            Console.Write("Крок, кг: ");
            bool ok3 = double.TryParse(Console.ReadLine(), out double step);

            if (!ok1 || !ok2 || !ok3 || from <= 0 || to < from || step <= 0)
            {
                Console.WriteLine("Неправильні дані. Крок має бути більшим за нуль.");
                return;
            }

            double steps = (to - from) / step;
            if (steps < 7 || steps > 99)
            {
                Console.WriteLine("Потрібно від 8 до 100 рядків таблиці.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"{"Вага, кг",12}{"Вартість, грн",18}");
            Console.WriteLine(new string('-', 30));

            for (int i = 0; i <= 99; i++)
            {
                double weight = from + i * step;
                if (weight > to + 0.000001)
                {
                    break;
                }
                double price = 40 + weight * 25;
                Console.WriteLine($"{weight,12:F2}{price,18:F2}");
            }
        }

        static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Натисніть Enter, щоб повернутися в меню...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
