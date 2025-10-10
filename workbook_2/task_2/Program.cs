using System;

namespace LuckyTicket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do {
                uint number;
                ushort firstPart, secondPart;
                byte firstSum, secondSum;

                Console.Write("Введите номер билета: ");
                number = Convert.ToUInt32(Console.ReadLine());
                firstPart = (ushort)(number / 1000);
                secondPart = (ushort)(number % 1000);
                firstSum = (byte)(firstPart / 100 + firstPart / 10 % 10+ firstPart % 10);
                secondSum = (byte)(secondPart / 100 + secondPart / 10 % 10 + secondPart % 10);
                Console.WriteLine(firstSum == secondSum);
            } while (true);
        }
    }
}
