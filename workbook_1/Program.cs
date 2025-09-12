public class Calculator
{
    public static void Main(string[] args)
    {
        char user_choice;
        double memory = 0.0d;
        do
        {
            double first_number = 0.0d;
            double second_number = 0.0d;
            string user_input;
            string operation;
            double? result;

            Console.WriteLine("Welcome!\nEnter number:");
            user_input = Console.ReadLine();
            if (user_input == "MR")
            {
                first_number = memory;
            }
            else if (!double.TryParse(user_input, out first_number))
            {
                Console.WriteLine($"Incorrect number type: '{user_input}'.");
                user_choice = 'y';
                continue;
            }
            else if (first_number >= 1.7976931348623157E+308)
            {
                Console.WriteLine("Too big value.");
                user_choice = 'y';
                continue;
            }

            Console.WriteLine("Enter math operation between the number:");
            operation = Console.ReadLine();
            if ((operation == "+") || (operation == "-") || (operation == "*") || (operation == "/") || (operation == "%"))
            {
                Console.WriteLine("Enter another number:");
                user_input = Console.ReadLine();
                if (user_input == "MR")
                {
                    second_number = memory;
                }
                else if (!double.TryParse(user_input, out second_number))
                {
                    Console.WriteLine($"Incorrect number type: '{user_input}'.");
                    user_choice = 'y';
                    continue;
                }
                else if (((operation == "/") || (operation == "%")) && (second_number == 0))
                {
                    Console.WriteLine("Dividing by zero is prohibited.");
                    user_choice = 'y';
                    continue;
                }
                else if (second_number >= 1.7976931348623157E+308)
                {
                    Console.WriteLine("Too big value.");
                    user_choice = 'y';
                    continue;
                }
            }

            result = operation switch
            {
                "+" => first_number + second_number,
                "-" => first_number - second_number,
                "*" => first_number * second_number,
                "/" => first_number / second_number,
                "%" => first_number % second_number,
                "1/x" => 1 / first_number,
                "x^2" => Math.Pow(first_number, 2),
                "x^0.5" => Math.Pow(first_number, 0.5),
                "M+" => memory += first_number,
                "M-" => memory -= first_number,
                _ => null,
            };
            if (result == null)
            {
                Console.WriteLine($"Invalid mathematicial operation: '{operation}'.");
                user_choice = 'y';
                continue;
            }
            else if (result >= 1.7976931348623157E+308)
            {
                Console.WriteLine("Too big value.");
            }
            else
            {
                Console.WriteLine($"Result: {result}");
            }

            Console.Write("Do another calculations (y/n): ");
            user_choice = Convert.ToChar(Console.ReadLine());
        }
        while (user_choice == 'y' || user_choice == 'Y');
    }
}