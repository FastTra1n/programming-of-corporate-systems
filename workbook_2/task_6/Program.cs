using System;

namespace LabExperiment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            void doExperiment(int bacteria, int mul)
            {
                int antibiotic = 10;
                int hour = 0;

                while (bacteria > 0 && antibiotic > 0)
                {
                    bacteria *= 2;
                    bacteria -= antibiotic * mul;
                    antibiotic -= 1;
                    hour += 1;
                    Console.WriteLine($"После {hour} часа бактерий осталось: {bacteria}");
                }
            }

            int n, m;

            Console.Write("Введите количество бактерий: ");
            n = Convert.ToInt32( Console.ReadLine() );
            Console.Write("Введите количество антибиотика: ");
            m = Convert.ToInt32(Console.ReadLine());
            doExperiment(n, m);
        }
    }
}