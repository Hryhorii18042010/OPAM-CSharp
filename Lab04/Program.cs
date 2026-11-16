using System;
using System.Text;

namespace Lab04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("=== Визначення чверті координатної площини ===");
            Console.Write("Введіть координату x: ");
            bool okX = double.TryParse(Console.ReadLine(), out double x);
            Console.Write("Введіть координату y: ");
            bool okY = double.TryParse(Console.ReadLine(), out double y);

            if (!okX || !okY)
            {
                Console.WriteLine("Помилка: введіть числові координати.");
                return;
            }

            string position;
            int quarter = 0;

            if (x == 0 && y == 0)
            {
                position = "Початок координат";
            }
            else if (y == 0)
            {
                position = "Вісь абсцис";
            }
            else if (x == 0)
            {
                position = "Вісь ординат";
            }
            else if (x > 0 && y > 0)
            {
                quarter = 1;
                position = "I чверть";
            }
            else if (x < 0 && y > 0)
            {
                quarter = 2;
                position = "II чверть";
            }
            else if (x < 0 && y < 0)
            {
                quarter = 3;
                position = "III чверть";
            }
            else
            {
                quarter = 4;
                position = "IV чверть";
            }

            string category = quarter switch
            {
                1 when x == y => "Верхня півплощина, на бісектрисі",
                >= 1 and <= 2 => "Верхня півплощина",
                >= 3 and <= 4 => "Нижня півплощина",
                _ => "Точка на осі або у початку координат"
            };

            Console.WriteLine();
            Console.WriteLine($"Координати: x = {x:F2}; y = {y:F2}");
            Console.WriteLine($"Положення точки: {position}");
            Console.WriteLine($"Категорія: {category}");
        }
    }
}
