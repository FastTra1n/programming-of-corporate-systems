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

                Console.WriteLine("Введите диапазон случайных значений [a, b] для первой матрицы (будут сгенерированы целые числа).");
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

            double[,] mulMatrixes(double[,] matrix1, double[,] matrix2)
            {
                double[,] result = new double[matrix1.GetLength(0), matrix2.GetLength(1)];

                if (matrix1.GetLength(1) != matrix2.GetLength(0))
                {
                    Console.WriteLine("Матрицы невозможно перемножить: число столбцов первой матрицы не равно числу строк второй.\n");
                    return result;
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
                        result[i, j] = interim_sum;
                        Console.Write($"{interim_sum} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                return result;
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

            double[,] calculateInvMatrix(double[,] matrix)
            {
                double det = determinant(matrix);
                if (det == 0)
                {
                    Console.WriteLine("Обратная матрица не существует, так как детерминант равен нулю.\n");
                    return new double[0, 0];
                };
                
                double[,] invMatrix = new double[matrix.GetLength(0), matrix.GetLength(1)];
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        double[,] minor = getMinor(matrix, i, j);
                        invMatrix[j, i] = Math.Pow(-1, i + j) * determinant(minor) / det;
                    }
                }
                return invMatrix;
            }

            //double[,] algebraicExtension(double[,] matrix)
            //{
            //    double[,] algMatrix = new double[matrix.GetLength(0), matrix.GetLength(1)];
            //    for (int i = 0; i < algMatrix.GetLength(0); i++)
            //    {
            //        for (int j = 0; j < algMatrix.GetLength(1); j++)
            //        {
            //            matrix[i, j] = Math.Pow(-1, i + j) * matrix[i, j];
            //        }
            //    }
            //    return algMatrix;
            //}

            double[,] transposeMatrix(double[,] matrix)
            {
                double[,] transpMatrix = new double[matrix.GetLength(1), matrix.GetLength(0)];
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        transpMatrix[i, j] = matrix[j, i];
                    }
                }
                return transpMatrix;
            }

            void solveSystem(double[,] A, double[,] B)
            {
                if (B.GetLength(1) != 1 || B.GetLength(0) != A.GetLength(0))
                {
                    Console.WriteLine("Данная система не имеет однозначного решения: матрица B должна быть вектором-столбцом такой же высоты, как A.");
                    return;
                }

                double[,] invA = calculateInvMatrix(A);
                double[,] solvedSystem = mulMatrixes(invA, B);
                Console.WriteLine("Корнями системы уравнений являются:");
                for (int i = 0; i < solvedSystem.GetLength(0); i++)
                {
                    Console.WriteLine($"x{i+1} = {solvedSystem[i, 0]}");
                }
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
                            if (m1.Length == 0 && m2.Length == 0)
                            {
                                Console.WriteLine("Необходимо создать матрицы перед их ручным заполнением.\n");
                                break;
                            }

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
                            if (m1.Length == 0 && m2.Length == 0)
                            {
                                Console.WriteLine("Необходимо создать матрицы перед заполнением их случайными значениями.\n");
                                break;
                            }

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
                            if (m1.Length == 0 && m2.Length == 0)
                            {
                                Console.WriteLine("Необходимо создать матрицы перед тем, как сложить их.\n");
                                break;
                            }

                            addMatrixes(m1, m2);
                            break;

                        case '5':
                            if (m1.Length == 0 && m2.Length == 0)
                            {
                                Console.WriteLine("Необходимо создать матрицы перед тем, как перемножить их.\n");
                                break;
                            }

                            mulMatrixes(m1, m2);
                            break;

                        case '6':
                            if (m1.Length == 0 && m2.Length == 0)
                            {
                                Console.WriteLine("Необходимо создать матрицы перед тем, как вычислить их детерминант.\n");
                                break;
                            }

                            char detChoice;

                            Console.WriteLine("Выберите первую (1) или вторую (2) матрицу для вычисления детерминанта.");
                            Console.Write("Ваш выбор: ");
                            detChoice = Convert.ToChar(Console.ReadLine());

                            if (detChoice == '1')
                            {
                                calculateDeterminant(m1);
                            }
                            else if (detChoice == '2')
                            {
                                calculateDeterminant(m2);
                            }
                            else
                            {
                                Console.WriteLine("Неизвестный номер матрицы. Выберите первую (1) или вторую (2) матрицу.\n");
                            }
                            break;

                        case '7':
                            if (m1.Length == 0 && m2.Length == 0)
                            {
                                Console.WriteLine("Необходимо создать матрицы перед тем, как найти обратную.\n");
                                break;
                            }

                            char invChoice;

                            Console.WriteLine("Выберите первую (1) или вторую (2) матрицу для вычисления обратной матрицы.");
                            Console.Write("Ваш выбор: ");
                            invChoice = Convert.ToChar(Console.ReadLine());

                            if (invChoice == '1')
                            {
                                double[,] inv = calculateInvMatrix(m1);
                                if (inv.Length == 0) break;

                                Console.WriteLine("Обратная матрица вычислена.\nЗначение в ячейках обратной матрицы:");
                                for (int i = 0; i < inv.GetLength(0); i++)
                                {
                                    for (int j = 0; j < inv.GetLength(1); j++)
                                    {
                                        Console.Write($"{inv[i, j]} ");
                                    }
                                    Console.WriteLine();
                                }
                            }
                            else if (invChoice == '2')
                            {
                                double[,] inv = calculateInvMatrix(m2);
                                if (inv.Length == 0) break;

                                Console.WriteLine("Обратная матрица вычислена.\nЗначение в ячейках обратной матрицы:");
                                for (int i = 0; i < inv.GetLength(0); i++)
                                {
                                    for (int j = 0; j < inv.GetLength(1); j++)
                                    {
                                        Console.Write($"{inv[i, j]} ");
                                    }
                                    Console.WriteLine();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Неизвестный номер матрицы. Выберите первую (1) или вторую (2) матрицу.\n");
                            }
                            break;

                        case '8':
                            if (m1.Length == 0 && m2.Length == 0)
                            {
                                Console.WriteLine("Необходимо создать матрицы перед тем, как транспонировать их.\n");
                                break;
                            }

                            char transpChoice;

                            Console.WriteLine("Выберите первую (1) или вторую (2) матрицу для транспонирования.");
                            Console.Write("Ваш выбор: ");
                            transpChoice = Convert.ToChar(Console.ReadLine());

                            if (transpChoice == '1')
                            {
                                double[,] transp = transposeMatrix(m1);
                                Console.WriteLine("Матрица успешно транспонирована.\nЗначение в ячейках транспонированной матрицы:");
                                for (int i = 0; i < transp.GetLength(0); i++)
                                {
                                    for (int j = 0; j < transp.GetLength(1); j++)
                                    {
                                        Console.Write($"{transp[i, j]} ");
                                    }
                                    Console.WriteLine();
                                }
                            }
                            else if (transpChoice == '2')
                            {
                                double[,] transp = transposeMatrix(m2);
                                Console.WriteLine("Матрица успешно транспонирована.\nЗначение в ячейках транспонированной матрицы:");
                                for (int i = 0; i < transp.GetLength(0); i++)
                                {
                                    for (int j = 0; j < transp.GetLength(1); j++)
                                    {
                                        Console.Write($"{transp[i, j]} ");
                                    }
                                    Console.WriteLine();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Неизвестный номер матрицы. Выберите первую (1) или вторую (2) матрицу.\n");
                            }
                            break;

                        case '9':
                            if (m1.Length == 0 && m2.Length == 0)
                            {
                                Console.WriteLine("Необходимо создать матрицы перед тем, как решить систему уравнений.\n");
                                break;
                            }

                            solveSystem(m1, m2);
                            break;

                        default:
                            Console.WriteLine("Операция не найдена. Повторите попытку.\n");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Недопустимый ввод. Проверьте корректность введённого Вами значения.\n");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Недопустимый ввод. Введённое или полученное число слишком большое/маленькое.\n");
                }
            } while (isExit);
        }
    }
}
