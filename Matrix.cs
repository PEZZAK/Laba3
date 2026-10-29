using System;
using System.Text;

namespace Laba3
{
    /// <summary>
    /// Класс с единственным полем — двумерным массивом.
    /// Задания 1, 2, 3 (вариант 14).
    /// </summary>
    public class Matrix
    {
        private int[,] data;

        public int Rows
        {
            get { return data.GetLength(0); }
        }

        public int Columns
        {
            get { return data.GetLength(1); }
        }

        public int this[int i, int j]
        {
            get { return data[i, j]; }
            set { data[i, j] = value; }
        }

        // Пустая матрица n x m (нужна для результатов операций)
        public Matrix(int n, int m)
        {
            data = new int[n, m];
        }

        // Конструктор копирования
        public Matrix(int[,] source)
        {
            int n = source.GetLength(0);
            int m = source.GetLength(1);
            data = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    data[i, j] = source[i, j];
                }
            }
        }

        // ============================================================
        // Задание 1, задача 14. Конструкторы заполнения
        // ============================================================

        // 1. Ввод с клавиатуры по диагоналям, параллельным главной,
        //    начиная с правого верхнего угла (внутри диагонали — сверху вниз)
        public Matrix(int n, int m, bool fromKeyboard)
        {
            data = new int[n, m];

            if (!fromKeyboard)
            {
                return;
            }

            Console.WriteLine("Заполнение массива " + n + "x" + m + " по диагоналям:");

            // k = j - i: от m-1 (правый верхний угол) до -(n-1) (левый нижний угол)
            for (int k = m - 1; k >= -(n - 1); k--)
            {
                int i = k < 0 ? -k : 0;
                int j = i + k;

                for (; i < n && j < m; i++, j++)
                {
                    data[i, j] = ReadInt("arr[" + i + "," + j + "] = ");
                }
            }
        }

        // 2. Массив n x n, случайные числа, в каждом столбце убывающая последовательность
        public Matrix(int n, Random random)
        {
            data = new int[n, n];

            for (int j = 0; j < n; j++)
            {
                // старт >= 10n, шаг <= 9, поэтому значения всегда положительные
                int current = random.Next(10 * n, 10 * n + 90);
                for (int i = 0; i < n; i++)
                {
                    data[i, j] = current;
                    current -= random.Next(1, 10);
                }
            }
        }

        // 3. Спираль для n x n (как на рисунке для n=5)
        public Matrix(int n, bool spiral)
        {
            data = new int[n, n];
            if (!spiral) return;

            int top = 0, bottom = n - 1, left = 0, right = n - 1;
            int value = n * n;

            while (top <= bottom && left <= right)
            {
                for (int j = left; j <= right; j++) data[top, j] = value--;
                top++;

                for (int i = top; i <= bottom; i++) data[i, right] = value--;
                right--;

                if (top <= bottom)
                {
                    for (int j = right; j >= left; j--) data[bottom, j] = value--;
                    bottom--;
                }

                if (left <= right)
                {
                    for (int i = bottom; i >= top; i--) data[i, left] = value--;
                    left++;
                }
            }
        }

        // ============================================================
        // Задание 2, задача 14. Вывод по числам Фибоначчи
        // ============================================================
        public void PrintByFibonacci()
        {
            int total = Rows * Columns;
            int printed = 0;
            int a = 1, b = 1;

            while (printed < total)
            {
                // в строке ровно a элементов (последняя строка может быть короче)
                for (int k = 0; k < a && printed < total; k++)
                {
                    Console.Write(data[printed / Columns, printed % Columns] + " ");
                    printed++;
                }

                Console.WriteLine();

                int next = a + b;
                a = b;
                b = next;
            }
        }

        // ============================================================
        // Задание 3, задача 14. Перегрузка операторов
        // ============================================================

        private static void CheckSameSize(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
                throw new ArgumentException("Размеры матриц не совпадают.");
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            CheckSameSize(a, b);
            Matrix result = new Matrix(a.Rows, a.Columns);
            for (int i = 0; i < a.Rows; i++)
                for (int j = 0; j < a.Columns; j++)
                    result[i, j] = a[i, j] + b[i, j];
            return result;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            CheckSameSize(a, b);
            Matrix result = new Matrix(a.Rows, a.Columns);
            for (int i = 0; i < a.Rows; i++)
                for (int j = 0; j < a.Columns; j++)
                    result[i, j] = a[i, j] - b[i, j];
            return result;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.Columns != b.Rows)
                throw new ArgumentException("Число столбцов A не равно числу строк B.");

            Matrix result = new Matrix(a.Rows, b.Columns);
            for (int i = 0; i < a.Rows; i++)
                for (int j = 0; j < b.Columns; j++)
                {
                    int sum = 0;
                    for (int t = 0; t < a.Columns; t++) sum += a[i, t] * b[t, j];
                    result[i, j] = sum;
                }
            return result;
        }

        public static Matrix operator *(Matrix a, int scalar)
        {
            Matrix result = new Matrix(a.Rows, a.Columns);
            for (int i = 0; i < a.Rows; i++)
                for (int j = 0; j < a.Columns; j++)
                    result[i, j] = a[i, j] * scalar;
            return result;
        }

        public static Matrix operator *(int scalar, Matrix a)
        {
            return a * scalar;
        }

        public Matrix Transpose()
        {
            Matrix result = new Matrix(Columns, Rows);
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    result[j, i] = data[i, j];
            return result;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    sb.Append(data[i, j].ToString().PadLeft(6));
                }
                if (i < Rows - 1) sb.AppendLine();
            }
            return sb.ToString();
        }

        private int ReadInt(string message)
        {
            Console.Write(message);
            int value;
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("Некорректный ввод. " + message);
            }
            return value;
        }
    }
}