using System;
using System.Globalization;
using System.Text;

namespace Lab06
{
    internal class Program
    {
        const int Capacity = 100;

        static int[] ids = new int[Capacity];
        static string[] titles = new string[Capacity];
        static string[] authors = new string[Capacity];
        static int[] years = new int[Capacity];
        static string[] genres = new string[Capacity];
        static int[] copies = new int[Capacity];
        static bool[] issued = new bool[Capacity];
        static DateTime[] issueDates = new DateTime[Capacity];

        static int count = 0;
        static int nextId = 1;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Seed();

            bool running = true;
            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("=== Бібліотека ===");
                Console.WriteLine("1 - Додати книгу");
                Console.WriteLine("2 - Показати всі книги");
                Console.WriteLine("3 - Знайти книгу за номером");
                Console.WriteLine("4 - Видалити книгу");
                Console.WriteLine("0 - Вихід");
                Console.Write("Ваш вибір: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1": AddBook(); break;
                    case "2": PrintAll(); break;
                    case "3": FindBook(); break;
                    case "4": DeleteBook(); break;
                    case "0": running = false; break;
                    default: Console.WriteLine("Немає такого пункту."); break;
                }
            }

            Console.WriteLine("Роботу завершено.");
        }

        static void SaveBook(string title, string author, int year, string genre,
                             int amount, bool isIssued, DateTime issueDate)
        {
            ids[count] = nextId;
            titles[count] = title;
            authors[count] = author;
            years[count] = year;
            genres[count] = genre;
            copies[count] = amount;
            issued[count] = isIssued;
            issueDates[count] = issueDate;
            count++;
            nextId++;
        }

        static void Seed()
        {
            SaveBook("Кобзар", "Т. Шевченко", 1840, "Поезія", 3, false, default(DateTime));
            SaveBook("Лісова пісня", "Леся Українка", 1911, "Драма", 2, true, new DateTime(2026, 10, 20));
            SaveBook("Тигролови", "І. Багряний", 1944, "Роман", 1, true, new DateTime(2026, 11, 10));
        }

        static void AddBook()
        {
            if (count >= Capacity)
            {
                Console.WriteLine("Сховище заповнене.");
                return;
            }

            Console.Write("Назва книги: ");
            string title = (Console.ReadLine() ?? "").Trim();
            if (title == "")
            {
                Console.WriteLine("Назву не введено.");
                return;
            }

            Console.Write("Автор: ");
            string author = (Console.ReadLine() ?? "").Trim();
            if (author == "")
            {
                Console.WriteLine("Автора не введено.");
                return;
            }

            Console.Write("Рік видання: ");
            if (!int.TryParse(Console.ReadLine(), out int year) || year < 1 || year > DateTime.Today.Year)
            {
                Console.WriteLine("Неправильний рік видання.");
                return;
            }

            Console.Write("Жанр: ");
            string genre = (Console.ReadLine() ?? "").Trim();
            if (genre == "")
            {
                Console.WriteLine("Жанр не введено.");
                return;
            }

            Console.Write("Кількість примірників: ");
            if (!int.TryParse(Console.ReadLine(), out int amount) || amount < 1)
            {
                Console.WriteLine("Неправильна кількість примірників.");
                return;
            }

            Console.Write("Книга видана (так/ні): ");
            string answer = (Console.ReadLine() ?? "").Trim().ToLower();
            if (answer != "так" && answer != "ні")
            {
                Console.WriteLine("Введіть так або ні.");
                return;
            }

            bool isIssued = answer == "так";
            DateTime issueDate = default(DateTime);
            if (isIssued)
            {
                Console.Write("Дата видачі (дд.мм.рррр): ");
                bool ok = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy",
                                                  CultureInfo.InvariantCulture,
                                                  DateTimeStyles.None, out issueDate);
                if (!ok || issueDate > DateTime.Today)
                {
                    Console.WriteLine("Неправильна дата видачі.");
                    return;
                }
            }

            SaveBook(title, author, year, genre, amount, isIssued, issueDate);
            Console.WriteLine($"Книгу додано. Інвентарний номер: {ids[count - 1]}");
        }

        static void PrintAll()
        {
            if (count == 0)
            {
                Console.WriteLine("Книг немає.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"{"№",3} | {"Назва",-21} | {"Автор",-17} | {"Рік",4} | {"Жанр",-12} | {"Прим.",5} | Стан");
            Console.WriteLine(new string('-', 88));

            int totalCopies = 0;
            for (int i = 0; i < count; i++)
            {
                string state = issued[i] ? "видана" : "в бібліотеці";
                Console.WriteLine($"{ids[i],3} | {titles[i],-21} | {authors[i],-17} | {years[i],4} | {genres[i],-12} | {copies[i],5} | {state}");
                totalCopies += copies[i];
            }

            Console.WriteLine($"Усього назв: {count}. Примірників: {totalCopies}.");
            Console.WriteLine("Книги за жанрами:");

            for (int i = 0; i < count; i++)
            {
                bool foundBefore = false;
                for (int j = 0; j < i; j++)
                {
                    if (genres[j] == genres[i])
                    {
                        foundBefore = true;
                    }
                }

                if (!foundBefore)
                {
                    int bookCount = 0;
                    int copyCount = 0;
                    for (int j = 0; j < count; j++)
                    {
                        if (genres[j] == genres[i])
                        {
                            bookCount++;
                            copyCount += copies[j];
                        }
                    }
                    Console.WriteLine($"{genres[i]}: {bookCount} назв, {copyCount} примірників");
                }
            }

            Console.WriteLine("Книги, видані понад 14 днів:");
            bool foundOverdue = false;
            for (int i = 0; i < count; i++)
            {
                if (issued[i] && (DateTime.Today - issueDates[i]).Days > 14)
                {
                    Console.WriteLine($"№{ids[i]} {titles[i]} - видана {issueDates[i]:dd.MM.yyyy}");
                    foundOverdue = true;
                }
            }
            if (!foundOverdue)
            {
                Console.WriteLine("Таких книг немає.");
            }
        }

        static int IndexOfId(int id)
        {
            for (int i = 0; i < count; i++)
            {
                if (ids[i] == id)
                {
                    return i;
                }
            }
            return -1;
        }

        static void FindBook()
        {
            Console.Write("Введіть інвентарний номер: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Номер має бути цілим числом.");
                return;
            }

            int index = IndexOfId(id);
            if (index == -1)
            {
                Console.WriteLine("Книгу не знайдено.");
                return;
            }

            Console.WriteLine($"Позиція в масивах: {index}");
            Console.WriteLine($"Номер: {ids[index]}");
            Console.WriteLine($"Назва: {titles[index]}");
            Console.WriteLine($"Автор: {authors[index]}");
            Console.WriteLine($"Рік видання: {years[index]}");
            Console.WriteLine($"Жанр: {genres[index]}");
            Console.WriteLine($"Примірників: {copies[index]}");
            Console.WriteLine($"Видана: {(issued[index] ? "так" : "ні")}");
            if (issued[index])
            {
                Console.WriteLine($"Дата видачі: {issueDates[index]:dd.MM.yyyy}");
            }
        }

        static void DeleteBook()
        {
            Console.Write("Введіть номер книги для видалення: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Номер має бути цілим числом.");
                return;
            }

            int index = IndexOfId(id);
            if (index == -1)
            {
                Console.WriteLine("Книгу не знайдено.");
                return;
            }

            for (int i = index; i < count - 1; i++)
            {
                ids[i] = ids[i + 1];
                titles[i] = titles[i + 1];
                authors[i] = authors[i + 1];
                years[i] = years[i + 1];
                genres[i] = genres[i + 1];
                copies[i] = copies[i + 1];
                issued[i] = issued[i + 1];
                issueDates[i] = issueDates[i + 1];
            }

            count--;
            ids[count] = 0;
            titles[count] = "";
            authors[count] = "";
            years[count] = 0;
            genres[count] = "";
            copies[count] = 0;
            issued[count] = false;
            issueDates[count] = default(DateTime);

            Console.WriteLine($"Книгу №{id} видалено. Залишилося: {count}.");
            PrintAll();
        }
    }
}
