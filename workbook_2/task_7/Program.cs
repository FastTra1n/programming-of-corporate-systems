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
                uint total_quanity_h = (max_width / final_width) * (max_height / final_height);
                uint total_quanity_v = (max_width / final_height) * (max_height / final_width);
                
                for (uint i = 0; i < max_width / final_width; i++)
                {
                    for (uint j = 0; j < max_width / final_width; j++)
                    {
                        uint placed = i * (max_height / final_height) + j * (max_width / final_height);
                        if (placed >= quanity)
                        {
                            return true;
                        }
                    }
                }
                return false;
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

                if (!canPlace(n, a, b, w, h, 0))
                {
                    Console.WriteLine("Размещение модулей невозможно даже без защиты (d = 0).");
                    return;
                }

                uint left = 0;
                uint right = Math.Max(w, h);
                uint answer = 0;
                while (left <= right)
                {
                    uint mid = (left + right) / 2;
                    if (canPlace(n, a, b, w, h, mid))
                    {
                        answer = mid;
                        left = mid + 1;
                    }
                    else
                    {
                        right = mid - 1;
                    }
                }
                Console.WriteLine($"Ответ: d = {answer}");
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
