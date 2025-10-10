using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                Console.Write("Введите числитель: ");
                m = Convert.ToInt32(Console.ReadLine());
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
                Console.WriteLine($"Результат: {m / numbers_nod}/{n / numbers_nod}");
            } while (true);
        }
    }
}
