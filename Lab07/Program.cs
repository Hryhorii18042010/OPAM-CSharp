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
                        Console.WriteLine("Цей пункт ще не готовий.");
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

        static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Натисніть Enter, щоб повернутися в меню...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
