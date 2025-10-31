using System;

namespace LuckyTicket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do {
                string userInput;
                uint number;
                ushort firstPart, secondPart;
                byte firstSum, secondSum;
                
                try
                {
                    Console.Write("Введите номер билета: ");
                    userInput = Console.ReadLine();
                    number = Convert.ToUInt32(userInput);
                    if (userInput.Length != 6 || number > 999999 || number < 100000)
                    {
                        Console.WriteLine("Число содержит более/менее 6 знаков. Введите целое 6-значное число.");
                        continue;
                    }
                    firstPart = (ushort)(number / 1000);
                    secondPart = (ushort)(number % 1000);
                    firstSum = (byte)(firstPart / 100 + firstPart / 10 % 10 + firstPart % 10);
                    secondSum = (byte)(secondPart / 100 + secondPart / 10 % 10 + secondPart % 10);
                    Console.WriteLine(firstSum == secondSum);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Недопустимый ввод для номера билета. Проверьте корректность введённого числа.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Число выходит за рамки допустимого диапазона чисел. Введите целое, неотрицательное 6-тизначное число.");
                }
            } while (true);
        }
    }
}
