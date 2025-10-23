using System;

namespace CoffeMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            uint water, milk;
            uint aQuanity = 0, lQuanity = 0;
            uint money = 0;

            try
            {
                Console.Write("Введите количество воды в мл: ");
                water = Convert.ToUInt32(Console.ReadLine());
                Console.Write("Введите количество молока в мл: ");
                milk = Convert.ToUInt32(Console.ReadLine());

                do
                {
                    char clientChoice;
                    Console.Write("Выберите напиток (1 - американо, 2 - латте): ");
                    clientChoice = Convert.ToChar(Console.ReadLine());
                    switch (clientChoice)
                    {
                        case '1':
                            if (water < 300)
                            {
                                Console.WriteLine("Не хватает воды");
                                break;
                            }
                            water -= 300;
                            money += 150;
                            aQuanity += 1;

                            Console.WriteLine("Ваш напиток готов.");
                            break;

                        case '2':
                            if (water < 30)
                            {
                                Console.WriteLine("Не хватает воды");
                                break;
                            }
                            else if (milk < 270)
                            {
                                Console.WriteLine("Не хватает молока");
                                break;
                            }
                            water -= 30;
                            milk -= 270;
                            money += 170;
                            lQuanity += 1;

                            Console.WriteLine("Ваш напиток готов.");
                            break;

                        default:
                            Console.WriteLine("Неверный ввод");
                            break;
                    }
                } while (water >= 30 && milk >= 270);

                Console.WriteLine("*Отчёт*\nИнгредиентов осталось:");
                Console.WriteLine($"\tВода: {water} мл\n\tМолоко: {milk} мл");
                Console.WriteLine($"Кружек американо приготовлено: {aQuanity}");
                Console.WriteLine($"Кружек латте приготовлено: {lQuanity}");
                Console.WriteLine($"Итого: {money} рублей.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Недопустимый ввод. Введите корректное целое, неотрицательное число.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Число выходит за диапазон допустимых значений. Введите корректное целое, неотрицательное число.");
            }
        }
    }
}
