using System;
using System.Text;

namespace Lab05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.Write("Введіть натуральне число n: ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n < 1)
            {
                Console.WriteLine("Помилка: потрібно ввести натуральне число.");
                return;
            }

            double sum = 0;
            int firstOverThree = 0;

            for (int i = 1; i <= n; i++)
            {
                sum += 1.0 / i;

                if (i % 5 == 0)
                {
                    Console.WriteLine($"Проміжна сума після {i} доданків: {sum:F4}");
                }

                if (sum > 3 && firstOverThree == 0)
                {
                    firstOverThree = i;
                }
            }

            Console.WriteLine($"Сума ряду: {sum:F6}");

            if (firstOverThree > 0)
            {
                Console.WriteLine($"Сума перевищила 3 після {firstOverThree}-го доданка.");
            }
            else
            {
                Console.WriteLine("Сума ще не перевищила 3.");
            }

            Console.WriteLine();
            Console.Write("Введіть розмір квадрата від 3 до 20: ");
            if (!int.TryParse(Console.ReadLine(), out int size) || size < 3 || size > 20)
            {
                Console.WriteLine("Помилка: розмір має бути від 3 до 20.");
                return;
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j || i + j == size - 1)
                    {
                        Console.Write("#");
                    }
                    else if (i == 0 || i == size - 1 || j == 0 || j == size - 1)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
