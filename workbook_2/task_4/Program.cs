using System;

namespace GuessNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte number, left, right, mid;
            string userAnswer;

            Console.WriteLine("Давайте сыграем в \"Угадай число\"! Загадайте число в диапазоне от 0 до 63, и я попробую угадать его.");
            left = 0;
            right = 63;

            while (left <= right)
            {
                mid = (byte) ((left + right) / 2);
                Console.Write($"Ваше число - {mid}?\nВаш ввод: ");
                userAnswer = Console.ReadLine();
                if ((userAnswer == "да") || (userAnswer == "1") || (userAnswer == "y"))
                {
                    Console.WriteLine("Отличная игра!");
                    break;

                }
                else if ((userAnswer == "нет") || (userAnswer == "0") || (userAnswer == "n"))
                {
                    Console.Write($"Хорошо, Ваше число больше {mid}?\nВаш ввод: ");
                    userAnswer = Console.ReadLine();
                    if ((userAnswer == "да") || (userAnswer == "1") || (userAnswer == "y"))
                    {
                        left = mid;
                    }
                    else if ((userAnswer == "нет") || (userAnswer == "0") || (userAnswer == "n"))
                    {
                        right = mid;
                    }
                }
            }
        }
    }
}
