using System;

namespace Fractions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int nod(int a, int b)
            {
                if (a * b == 0) { return a + b; }
                if (a < b)
                {
                    return nod(a, b % a);
                }
                else
                {
                    return nod(a % b, b);
                }
            }

            do
            {
                int m, n, numbers_nod;

                try
                {
                    Console.Write("Введите числитель: ");
                    m = Convert.ToInt32(Console.ReadLine());
                    if (m == 0)
                    {
                        Console.WriteLine("Результат: 0");
                        continue;
                    }
                    Console.Write("Введите знаменатель: ");
                    n = Convert.ToInt32(Console.ReadLine());
                    if (n == 0)
                    {
                        Console.WriteLine("Знаменатель не может равняться 0!");
                        continue;
                    }

                    numbers_nod = nod(Math.Abs(m), Math.Abs(n));
                    if (n < 0)
                    {
                        m = -m;
                        n = -n;
                    }
                    if (n / numbers_nod == 1)
                    {
                        Console.WriteLine($"Результат: {m / numbers_nod}");
                    }
                    else
                    {
                        Console.WriteLine($"Результат: {m / numbers_nod}/{n / numbers_nod}");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Недопустимый ввод. Проверьте корректность введённого числа.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Число слишком большое или слишком маленькое и не входит в диапазон допустимых значений. Попробуйте число поменьше/побольше.");
                }
            } while (true);
        }
    }
}
