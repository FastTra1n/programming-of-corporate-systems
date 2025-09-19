using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                byte startDay, monthDay, dayOfWeek;
                bool isWeekend;

                Console.WriteLine("Enter day of the week number: ");
                startDay = Convert.ToByte(Console.ReadLine());
                if ((startDay < 1) || (startDay > 7))
                {
                    Console.WriteLine("Incorrect day input: day of the week number should be from 1 to 7.");
                    continue;
                }
                Console.WriteLine("Enter day of the month number: ");
                monthDay = Convert.ToByte(Console.ReadLine());
                if ((monthDay < 1) || (monthDay > 31))
                {
                    Console.WriteLine("Incorrect day input: day of the month number should be from 1 to 31.");
                    continue;
                }

                dayOfWeek = (byte)((startDay + monthDay - 2) % 7 + 1);
                isWeekend = ((dayOfWeek > 0) && (dayOfWeek < 6)) || (dayOfWeek < 6 || (dayOfWeek > 7 && dayOfWeek < 11)) ? true : false;
                Console.WriteLine($"{monthDay} мая - {(isWeekend ? "выходной" : "рабочий")} день");
            } while (true);
        }
    }
}
