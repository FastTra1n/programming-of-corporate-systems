using System;

namespace MatrixCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            void createMatrixes(out double[,] matrix1, out double[,] matrix2)
            {
                uint n, m;
                
                Console.WriteLine("Создание первой матрицы.");
                Console.Write("Введите количество строк: ");
                n = Convert.ToUInt32(Console.ReadLine());
                Console.Write("Введите количество столбцов: ");
                m = Convert.ToUInt32(Console.ReadLine());
                matrix1 = new double[n, m];

                Console.WriteLine("Создание второй матрицы.");
                Console.Write("Введите количество строк: ");
                n = Convert.ToUInt32(Console.ReadLine());
                Console.Write("Введите количество столбцов: ");
                m = Convert.ToUInt32(Console.ReadLine());
                matrix2 = new double[n, m];
            }

            void fillMatrixesYsf(ref double[,] matrix1, ref double[,] matrix2)
            {
                Console.WriteLine("Последовательно введите значения каждой ячейки матрицы (заполнение идёт слева-направо, сверху-вниз).");
                Console.WriteLine("Заполнение первой матрицы:");
                for (int i = 0; i < matrix1.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix1.GetLength(1); j++)
                    {
                        matrix1[i, j] = Convert.ToDouble(Console.ReadLine());
                    }
                }
                Console.WriteLine("Заполнение второй матрицы:");
                for (int i = 0; i < matrix2.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix2.GetLength(1); j++)
                    {
                        matrix2[i, j] = Convert.ToDouble(Console.ReadLine());
                    }
                }
            }

            void fillMatrixesRnd(ref double[,] matrix1, ref double[,] matrix2)
            {
                var random = new Random();
                int a, b;

                Console.WriteLine("Введите диапазон случайных значений [a, b] для первой матрицы.");
                Console.Write("a: ");
                a = Convert.ToInt32(Console.ReadLine());
                Console.Write("b: ");
                b = Convert.ToInt32(Console.ReadLine());

                for (int i = 0; i < matrix1.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix1.GetLength(1); j++)
                    {
                        matrix1[i, j] = random.Next(a, b);
                    }
                }
                Console.WriteLine("Введите диапазон случайных значений [a, b] для второй матрицы.");
                Console.Write("a: ");
                a = Convert.ToInt32(Console.ReadLine());
                Console.Write("b: ");
                b = Convert.ToInt32(Console.ReadLine());
                for (int i = 0; i < matrix2.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix2.GetLength(1); j++)
                    {
                        matrix2[i, j] = random.Next(a, b);
                    }
                }
            }

            void addMatrixes(double[,] matrix1, double[,] matrix2)
            {
                double[,] result = new double[matrix1.GetLength(0), matrix2.GetLength(1)];

                if ((matrix1.GetLength(0) != matrix2.GetLength(0)) || (matrix1.GetLength(1) != matrix2.GetLength(1)))
                {
                    Console.WriteLine("Матрицы невозможно сложить, ввиду несоответствия размерностей матриц.\n");
                    return;
                }

                Console.WriteLine("Результатом сложения является матрица следующего вида:");
                for (int i = 0; i < matrix1.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix1.GetLength(1); j++)
                    {
                        Console.Write($"{matrix1[i, j] + matrix2[i, j]} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            void mulMatrixes(double[,] matrix1, double[,] matrix2)
            {
                double[,] result = new double[matrix1.GetLength(0), matrix2.GetLength(1)];

                if (matrix1.GetLength(1) != matrix2.GetLength(0))
                {
                    Console.WriteLine("Матрицы невозможно перемножить: число столбцов первой матрицы не равно числу строк второй.\n");
                    return;
                }

                Console.WriteLine("Результатом перемножения является матрица следующего вида:");
                double interim_sum;
                for (int i = 0; i < matrix1.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix2.GetLength(1); j++)
                    {
                        interim_sum = 0;
                        for (int k = 0; k < matrix1.GetLength(1); k++)
                        {
                            interim_sum += matrix1[i, k] * matrix2[k, j];
                        }
                        Console.Write($"{interim_sum} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            void calculateDeterminant(double[,] matrix)
            {
                if (matrix.GetLength(0) != matrix.GetLength(1))
                {
                    Console.WriteLine("Вычисление детерминанта невозможно, так как матрица должна быть квадратной.");
                    return;
                }

                double det = determinant(matrix);
                Console.WriteLine($"Детерминант выбранной матрицы равен: {det}");
            }

            double determinant(double[,] matrix)
            {
                int n = matrix.GetLength(0);
                if (n == 1) return matrix[0, 0];
                if (n == 2) return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

                double det = 0;
                for (int i = 0; i < n; i++)
                {
                    double[,] minor = getMinor(matrix, 0, i);
                    det += (i % 2 == 0 ? 1 : -1) * matrix[0, i] * determinant(minor);
                }
                return det;
            }

            double[,] getMinor(double[,] matrix, int row, int col)
            {
                int n = matrix.GetLength(0);
                double[,] minor = new double[n - 1, n - 1];

                for (int i = 0, j = 0; i < n; i++)
                {
                    if (i == row) continue;
                    for (int k = 0, c = 0; k < n; k++)
                    {
                        if (k == col) continue;
                        minor[j, c++] = matrix[i, k];
                    }
                    j++;
                }
                return minor;
            }

            char option;
            double[,] m1 = { };
            double[,] m2 = { };
            bool isExit = true;

            Console.WriteLine("Добро пожаловать в калькулятор матриц!");
            do
            {
                try
                {
                    Console.WriteLine(
                        "Для продолжения, выберите операцию, которую хотите произвести:\n" +
                        "0. Выйти из калькулятора\n" +
                        "1. Создать 2 новых матрицы\n" +
                        "2. Заполнить матрицы (ввод)\n" +
                        "3. Заполнить матрицы (случ)\n" +
                        "4. Сложить матрицы\n" +
                        "5. Умножить матрицы\n" +
                        "6. Найти детерминант матрицы\n" +
                        "7. Найти обратную матрицу\n" +
                        "8. Транспонировать матрицу\n" +
                        "9. Найти корни системы уравнений"
                    );
                    Console.Write("Ваш выбор (цифра от 1 до 9): ");
                    option = Convert.ToChar(Console.ReadLine());

                    switch (option)
                    {
                        case '0':
                            isExit = false;
                            Console.WriteLine("Завершение работы...");
                            break;

                        case '1':
                            createMatrixes(out m1, out m2);
                            Console.WriteLine("Матрицы успешно созданы.\n");
                            break;
                        
                        case '2':
                            fillMatrixesYsf(ref m1, ref m2);
                            Console.WriteLine("Матрицы успешно заполнены.\nЗначение в ячейках первой матрицы:");
                            for (int i = 0; i < m1.GetLength(0); i++)
                            {
                                for (int j = 0; j < m1.GetLength(1); j++)
                                {
                                    Console.Write($"{m1[i, j]} ");
                                }
                                Console.WriteLine();
                            }
                            Console.WriteLine("Значение в ячейках второй матрицы:");
                            for (int i = 0; i < m2.GetLength(0); i++)
                            {
                                for (int j = 0; j < m2.GetLength(1); j++)
                                {
                                    Console.Write($"{m2[i, j]} ");
                                }
                                Console.WriteLine();
                            }
                            break;

                        case '3':
                            fillMatrixesRnd(ref m1, ref m2);
                            Console.WriteLine("Матрицы успешно заполнены.\nЗначение в ячейках первой матрицы:");
                            for (int i = 0; i < m1.GetLength(0); i++)
                            {
                                for (int j = 0; j < m1.GetLength(1); j++)
                                {
                                    Console.Write($"{m1[i, j]} ");
                                }
                                Console.WriteLine();
                            }
                            Console.WriteLine("Значение в ячейках второй матрицы:");
                            for (int i = 0; i < m2.GetLength(0); i++)
                            {
                                for (int j = 0; j < m2.GetLength(1); j++)
                                {
                                    Console.Write($"{m2[i, j]} ");
                                }
                                Console.WriteLine();
                            }
                            break;

                        case '4':
                            addMatrixes(m1, m2);
                            break;

                        case '5':
                            mulMatrixes(m1, m2);
                            break;

                        case '6':
                            char choice;

                            Console.WriteLine("Выберите первую (1) или вторую (2) матрицу для вычисления детерминанта.");
                            Console.Write("Ваш выбор: ");
                            choice = Convert.ToChar(Console.ReadLine());

                            if (choice == '1')
                            {
                                calculateDeterminant(m1);
                            }
                            else if (choice == '2')
                            {
                                calculateDeterminant(m2);
                            }
                            else
                            {
                                Console.WriteLine("Неизвестный номер матрицы. Выберите первую (1) или вторую (2) матрицу.");
                            }
                            break;

                        default:
                            Console.WriteLine("Операция не найдена. Повторите попытку.\n");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Недопустимый ввод. Проверьте корректность введённого Вами значения.");
                }
            } while (isExit);
        }
    }
}
