using System.Collections;
using System.Text.RegularExpressions;

Dictionary<(int, int), int> cache = new Dictionary<(int, int), int>();

int A(int m, int n)
{
    if (m < 0 || n < 0)
    {
        throw new ArgumentOutOfRangeException();
    }
    if (cache.TryGetValue((m, n), out int cacheResult))
    {
        return cacheResult;
    }

    int result;
    if (m == 0) { result = n + 1; }
    else if (m > 0 && n == 0) { result = A(m - 1, 1); }
    else { result = A(m - 1, A(m, n - 1)); }
    cache[(m, n)] = result;
    return result;
}

while (true)
{
    string userInput;
    int result;

    Console.Write("Ввод (m=x n=y): ");
    userInput = Console.ReadLine();
    if (string.IsNullOrEmpty(userInput))
    {
        Console.WriteLine("Ввод не может быть пустым: введите значение m и n по указанному формату.");
        continue;
    }

    try
    {
        var variables = userInput.Split(' ');
        var parts = userInput.Split(' ');
        if (parts.Length != 2)
        {
            Console.WriteLine("Некорретный формат ввода: между присваиваниями значений переменным может быть только один пробел. Проверьте корректность введённой строки.");
            continue;
        }

        var mPart = parts[0].Split('=');
        var nPart = parts[1].Split('=');
        if (mPart.Length != 2 || !int.TryParse(mPart[1], out int m) || m == 0)
        {
            throw new FormatException();
        }
        if (nPart.Length != 2 || !int.TryParse(nPart[1], out int n) || n == 0)
        {
            throw new FormatException();
        }
        
        if (m > 3 || n > 10)
        {
            Console.WriteLine("Введённые значения для переменных m и n слишком велики и могут привести к непредсказуемому результату (неточность ответа/ошибка). Попробуйте другие значения.");
            continue;
        }
        result = A(m, n);
        Console.WriteLine($"Вывод: A(m, n) = {result}");
    }
    catch (FormatException)
    {
        Console.WriteLine("Некорретный ввод: введите целое неотрицальное, ненулевое число для переменной m и n соответственно в указанном формате (m=x n=y).");
    }
    catch (StackOverflowException)
    {
        Console.WriteLine("Переполнение стека: рекурсия достигла максимальной глубины. Вычисление значения невозможно.");
    }
    catch (OverflowException)
    {
        Console.WriteLine("Переполнение типа: итоговые значения переваливают диапазон допустимых значений.");
    }
    catch (ArgumentOutOfRangeException)
    {
        Console.WriteLine("Недопустимые значения для переменных: m и n должны быть неотрицательными.");
    }
}