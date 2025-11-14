using System;

namespace NumberReversal
{
    class Program
    {
        static uint? reverseNumber(uint number, uint reversed = 0)
        {
            if (number == 0) return reversed;
            if (number % 10 == 0) return null;

            reversed = checked(reversed * 10 + number % 10);
            return reverseNumber(number / 10, reversed);
        }

        static void Main(string[] args)
        {
            string userInput;
            uint n;
            uint? result;

            while (true)
            {
                try
                {
                    Console.Write("Ввод: ");
                    userInput = Console.ReadLine();
                    n = Convert.ToUInt32(userInput);
                    if (userInput[0] == '0' || n == 0)
                    {
                        Console.WriteLine("Введите другое, отличное от нуля число. Без ведущих нулей.");
                        continue;
                    }
                    if (userInput[0] == '0')
                    {
                        Console.WriteLine("Введите другое, отличное от нуля число.");
                        continue;
                    }

                    result = reverseNumber(n);
                    if (result == null)
                    {
                        Console.WriteLine("Использование нулей в числе запрещено. Введите целое десятичное число, не содержащее нулей.");
                        continue;
                    }
                    Console.WriteLine($"Вывод: {result}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Недопустимый ввод. Введите целое десятичное число.");
                }
                catch
                {
                    Console.WriteLine("Введено слишком длинное число или число является отрицательным. Попробуйте другое число.");
                }
            }
        }
    }
}