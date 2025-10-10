using System;

namespace MaclaurinSeries
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double dFactorial(int x)
            {
                if (x <= 1)
                {
                    return 1.0d;
                }
                return x * dFactorial(x - 2);
            }

            double f(int n, double x)
            {
                if (n == 0)
                {
                    return x;
                }
                return Math.Pow(-1, n) * (dFactorial(2 * n - 1) * Math.Pow(x, 2 * n + 1)) / (dFactorial(2 * n) * (2 * n + 1));
            }

            double calculateSeriesSum(int n, double x, double e)
            {
                double sum = 0.0d;
                double term;

                for (int i = 0; i <= n; i++)
                {
                    term = f(i, x);
                    if (Math.Abs(term) < e)
                    {
                        break;
                    }

                    sum += term;
                }
                return sum;
            }

            Console.WriteLine("Вычисление функции arsh(x) с помощью ряда Маклорена.");
            do
            {
                double x, e, result;
                int n;

                Console.Write("Введите значение x: ");
                x = Convert.ToDouble(Console.ReadLine());
                Console.Write("Введите номер члена ряда, который хотите вычислить (n): ");
                n = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите точность e (<0.01): ");
                e = Convert.ToDouble(Console.ReadLine());
                result = calculateSeriesSum(n, x, e);
                Console.WriteLine($"Значение {n}-го члена ряда: {result}.");
            } while (true);
        }
    }
}
