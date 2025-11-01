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
                    userInput = Console.ReadLine().Trim();
                    number = Convert.ToUInt32(userInput);
                    if (userInput.Length > 1 && userInput[0] == '0')
                    {
                        if (userInput.Length != 6)
                        {
                            Console.WriteLine("Число содержит более или менее 6 знаков. Введите целое 6-значное число.");
                            continue;
                        }

                        firstPart = (ushort)(number / 1000);
                        secondPart = (ushort)(number % 1000);
                        firstSum = (byte)(firstPart / 100 + firstPart / 10 % 10 + firstPart % 10);
                        secondSum = (byte)(secondPart / 100 + secondPart / 10 % 10 + secondPart % 10);
                        Console.WriteLine(firstSum == secondSum);
                    }
                    else
                    {
                        if (userInput[0] == '+')
                        {
                            Console.WriteLine("Введите 6-значное целое число без указания знака числа.");
                            continue;
                        }
                        if (number > 999999)
                        {
                            Console.WriteLine("Число содержит более 6 знаков. Введите целое 6-значное число.");
                            continue;
                        }

                        if (number < 100000)
                        {
                            Console.WriteLine("Число содержит менее 6 знаков. Введите целое 6-значное число.");
                            continue;
                        }

                        firstPart = (ushort)(number / 1000);
                        secondPart = (ushort)(number % 1000);
                        firstSum = (byte)(firstPart / 100 + firstPart / 10 % 10 + firstPart % 10);
                        secondSum = (byte)(secondPart / 100 + secondPart / 10 % 10 + secondPart % 10);
                        Console.WriteLine(firstSum == secondSum);
                    }
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
