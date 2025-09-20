using System;

namespace ATM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do {
                int money;
                int atm5000, atm2000, atm1000, atm500, atm200, atm100;

                Console.WriteLine("Введите сумму, которую хотите снять: ");
                money = Convert.ToInt32(Console.ReadLine());
                if (money > 150000)
                {
                    Console.WriteLine("Банкомат не может выдавать за одну операцию более 150 тыс. рублей");
                    continue;
                }
                else if (money % 100  != 0)
                {
                    Console.WriteLine("Невозможно выдать указанную сумму из-за отсутствия соответствующего номинала");
                    continue;
                }

                atm5000 = money / 5000;
                atm2000 = (money % 5000) / 2000;
                atm1000 = (money % 5000 % 2000) / 1000;
                atm500 = (money % 5000 % 2000 % 1000) / 500;
                atm200 = (money % 5000 % 2000 % 1000 % 500) / 200;
                atm100 = (money % 5000 % 2000 % 1000 % 500 % 200) / 100;
                Console.WriteLine($"Снимаю со счёта {atm5000} по 5000, {atm2000} по 2000, {atm1000} по 1000, {atm500} по 500, {atm200} по 200 и {atm100} по 100.");
            } while (true);
        }
    }
}