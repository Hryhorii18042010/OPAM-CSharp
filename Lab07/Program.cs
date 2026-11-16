using System;
using System.Text;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            double[] bookPrices = new double[100];
            int count = 0;
            bool running = true;

            while (running)
            {
                ShowMenu();
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
                        ShowIssueTable();
                        Pause();
                        break;
                    case "4":
                        if (count == bookPrices.Length)
                        {
                            Console.WriteLine("Масив заповнений.");
                        }
                        else
                        {
                            double value = ReadDouble("Вартість доставки, грн: ", 0, 100000);
                            AddValue(bookPrices, ref count, value);
                            Console.WriteLine($"Додано запис. Усього: {count}");
                        }
                        Pause();
                        break;
                    case "5":
                        PrintValues(bookPrices, count);
                        Pause();
                        break;
                    case "6":
                        FindMax(bookPrices, count, out double max, out int number);
                        if (number == -1)
                        {
                            Console.WriteLine("Масив порожній, найбільшого значення немає.");
                        }
                        else
                        {
                            Console.WriteLine($"Найбільша вартість: {max:F2} грн, номер: {number}");
                        }
                        Pause();
                        break;
                    case "7":
                        int position = ReadInt("Номер для видалення (1-100): ", 1, 100);
                        RemoveAt(bookPrices, ref count, position - 1);
                        Pause();
                        break;
                    case "8":
                        int oldNumber = 10;
                        double[] example = { 10, 20 };
                        Console.WriteLine($"Число до виклику: {oldNumber}");
                        ChangeNumber(oldNumber);
                        Console.WriteLine($"Число після виклику: {oldNumber}");
                        Console.WriteLine($"Масив до виклику: {example[0]}");
                        ChangeFirst(example);
                        Console.WriteLine($"Масив після виклику: {example[0]}");
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

        static void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=== Бібліотека ===");
            Console.WriteLine("1. Довідка про програму");
            Console.WriteLine("2. Вартість доставки книг");
            Console.WriteLine("3. Таблиця видач за жанрами і днями");
            Console.WriteLine("4. Додати вартість доставки");
            Console.WriteLine("5. Показати збережені значення");
            Console.WriteLine("6. Показати найбільшу вартість");
            Console.WriteLine("7. Видалити значення за номером");
            Console.WriteLine("8. Дослід зі значенням і масивом");
            Console.WriteLine("0. Вихід");
            Console.Write("Ваш вибір: ");
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
            double from = ReadDouble("Початкова вага, кг: ", 0.01, 10000);
            double to = ReadDouble("Кінцева вага, кг: ", from, 10000);
            double step = ReadDouble("Крок, кг: ", 0.000001, 10000);

            double steps = (to - from) / step;
            if (steps < 7 || steps > 99)
            {
                Console.WriteLine("Потрібно від 8 до 100 рядків таблиці.");
                return;
            }

            PrintCostTable(from, to, step);
        }

        static double CalculateDeliveryCost(double weight, double rate, double basePrice)
        {
            return basePrice + weight * rate;
        }

        static void PrintCostTable(double from, double to, double step)
        {
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

                double price = CalculateDeliveryCost(weight, 25, 40);
                Console.WriteLine($"{weight,12:F2}{price,18:F2}");
            }
        }

        static void ShowIssueTable()
        {
            Console.WriteLine("Демонстраційна таблиця видач книг");
            int rows = ReadInt("Кількість жанрів (1-10): ", 1, 10);
            int columns = ReadInt("Кількість днів (1-7): ", 1, 7);

            string[] genres = { "Роман", "Поезія", "Казка", "Наука", "Історія", "Детектив", "Фентезі", "Довідник", "Драма", "Біографія" };

            Console.WriteLine();
            Console.Write($"{"Жанр / день",-14}");
            for (int j = 1; j <= columns; j++)
            {
                Console.Write($"{j,8}");
            }
            Console.WriteLine($"{"Разом",10}");

            int all = 0;
            for (int i = 0; i < rows; i++)
            {
                Console.Write($"{genres[i],-14}");
                int rowSum = 0;
                for (int j = 1; j <= columns; j++)
                {
                    int books = CalculateIssueCount(i + 1, j);
                    Console.Write($"{books,8}");
                    rowSum += books;
                }
                Console.WriteLine($"{rowSum,10}");
                all += rowSum;
            }

            Console.Write($"{"Разом",-14}");
            for (int j = 1; j <= columns; j++)
            {
                int columnSum = 0;
                for (int i = 0; i < rows; i++)
                {
                    columnSum += CalculateIssueCount(i + 1, j);
                }
                Console.Write($"{columnSum,8}");
            }
            Console.WriteLine($"{all,10}");
        }

        static int CalculateIssueCount(int genreNumber, int day)
        {
            return genreNumber * day;
        }

        static double ReadDouble(string prompt, double min, double max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? "";

                if (!double.TryParse(input, out double value))
                {
                    Console.WriteLine("Помилка: введіть число.");
                }
                else if (value < min || value > max || double.IsNaN(value))
                {
                    Console.WriteLine($"Помилка: число має бути від {min} до {max}.");
                }
                else
                {
                    return value;
                }
            }
        }

        static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? "";

                if (!int.TryParse(input, out int value))
                {
                    Console.WriteLine("Помилка: введіть ціле число.");
                }
                else if (value < min || value > max)
                {
                    Console.WriteLine($"Помилка: число має бути від {min} до {max}.");
                }
                else
                {
                    return value;
                }
            }
        }

        static void AddValue(double[] values, ref int count, double value)
        {
            if (count >= values.Length)
            {
                Console.WriteLine("Масив заповнений.");
                return;
            }

            values[count] = value;
            count++;
        }

        static void PrintValues(double[] values, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Значень поки немає.");
                return;
            }

            Console.WriteLine($"{"Номер",8}{"Вартість, грн",18}");
            Console.WriteLine(new string('-', 26));
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{i + 1,8}{values[i],18:F2}");
            }
        }

        static void FindMax(double[] values, int count, out double max, out int number)
        {
            max = 0;
            number = -1;
            if (count == 0)
            {
                return;
            }

            max = values[0];
            number = 1;
            for (int i = 1; i < count; i++)
            {
                if (values[i] > max)
                {
                    max = values[i];
                    number = i + 1;
                }
            }
        }

        static void ChangeNumber(int number)
        {
            number = number + 100;
        }

        static void ChangeFirst(double[] values)
        {
            values[0] = values[0] + 100;
        }

        static void RemoveAt(double[] values, ref int count, int index)
        {
            if (index < 0 || index >= count)
            {
                Console.WriteLine("Неправильний номер. Нічого не видалено.");
                return;
            }

            for (int i = index; i < count - 1; i++)
            {
                values[i] = values[i + 1];
            }
            count--;
            values[count] = 0;
            Console.WriteLine("Значення видалено.");
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
