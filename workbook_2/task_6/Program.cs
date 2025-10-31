using System;

namespace LabExperiment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            void doExperiment(uint bacteria, uint mul)
            {
                uint antibiotic = 10;
                uint hour = 0;

                while (bacteria > 0 && antibiotic > 0)
                {
                    bacteria *= 2;
                    if (bacteria < antibiotic * mul)
                    {
                        bacteria = 0;
                    }
                    else
                    {
                        bacteria -= antibiotic * mul;
                    }
                    antibiotic -= 1;
                    hour += 1;
                    Console.WriteLine($"После {hour} часа бактерий осталось: {bacteria}");
                }
            }

            uint n, m;

            try
            {
                Console.Write("Введите количество бактерий: ");
                n = Convert.ToUInt32(Console.ReadLine());
                Console.Write("Введите количество антибиотика: ");
                m = Convert.ToUInt32(Console.ReadLine());
                doExperiment(n, m);
            }
            catch (FormatException)
            {
                Console.WriteLine("Недопустимый ввод. Перепроверьте введённое число.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Введённое число слишком больше/маленькое. Введите число поменьше/побольше.");
            }
        }
    }
}