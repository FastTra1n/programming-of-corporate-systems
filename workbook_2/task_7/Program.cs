using System;

namespace MarsColonization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool canPlace(uint quanity, uint width, uint height, uint max_width, uint max_height, uint thickness)
            {
                uint final_width = width + 2 * thickness;
                uint final_height = height + 2 * thickness;
                uint total_quanity_h = max_width / final_width * max_height / final_height;
                uint total_quanity_v = max_width / final_height * max_height / final_width;
                return (Math.Max(total_quanity_h, total_quanity_v) >= quanity);
            }

            uint n, a, b, w, h;

            try
            {
                Console.Write("Введите n: ");
                n = Convert.ToUInt32(Console.ReadLine());
                Console.Write("Введите a: ");
                a = Convert.ToUInt32(Console.ReadLine());
                Console.Write("Введите b: ");
                b = Convert.ToUInt32(Console.ReadLine());
                Console.Write("Введите w: ");
                w = Convert.ToUInt32(Console.ReadLine());
                Console.Write("Введите h: ");
                h = Convert.ToUInt32(Console.ReadLine());

                uint left = 0;
                uint right = Math.Max(w, h);
                while (left < right)
                {
                    uint mid = (left + right + 1) / 2;
                    if (canPlace(n, a, b, w, h, mid))
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
            catch (FormatException)
            {
                Console.WriteLine("Неверный ввод. Введите целое, неотрицательное число.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Слишком большое число. Введите число поменьше.");
            }
        }
    }
}
