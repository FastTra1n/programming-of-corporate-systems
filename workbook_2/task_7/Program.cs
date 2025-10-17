using System;

namespace MarsColonization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool canPlace(int quanity, int width, int height, int max_width, int max_height, int thickness)
            {
                int final_width = width + 2 * thickness;
                int final_height = height + 2 * thickness;
                int total_quanity = max_width / final_width * max_height / final_height;
                return total_quanity >= quanity;
            }

            int n, a, b, w, h;

            Console.Write("Введите n: ");
            n = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите a: ");
            a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите b: ");
            b = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите w: ");
            w = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите h: ");
            h = Convert.ToInt32(Console.ReadLine());

            int left = 0;
            int right = Math.Max(w, h);
            while (left < right)
            {
                int mid = (left + right) / 2;
                if (canPlace(n, a, b, w, h , mid))
                {
                    left = mid;
                }
                else
                {
                    right = mid - 1;
                }
            }
            Console.WriteLine($"Ответ: d = {left}");
        }
    }
}
