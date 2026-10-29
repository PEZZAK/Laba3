using System;
using System.Text;

namespace Laba3
{
    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Program program = new Program();
            program.Run();
        }

        public void Run()
        {
            Console.WriteLine("=== Задание 1, задача 14. Заполнение массивов ===");
            DemonstrateMatrixFill();

            Console.WriteLine("\n=== Задание 2, задача 14. Вывод по Фибоначчи ===");
            DemonstrateFibonacci();

            Console.WriteLine("\n=== Задание 3, задача 14. Матричное выражение 2*A + (B - C^T) ===");
            DemonstrateMatrixExpression();

            Console.WriteLine("\n=== Задание 4, задача 14. Бинарные файлы ===");
            DemonstrateBinaryFile();

            Console.WriteLine("\n=== Задание 5, задача 14. XML и игрушки ===");
            DemonstrateXmlToys();

            Console.WriteLine("\n=== Задание 6, задача 14. Текстовый файл (1 в строке) ===");
            DemonstrateTextFileOnePerLine();

            Console.WriteLine("\n=== Задание 7, задача 14. Текстовый файл (несколько в строке) ===");
            DemonstrateTextFileSeveralPerLine();

            Console.WriteLine("\n=== Задание 8, задача 14. Текстовый файл — строки с «б» ===");
            DemonstrateLinesStartingWithB();
        }

        private void DemonstrateMatrixFill()
        {
            Console.WriteLine("Первый массив 3x4 (ввод по диагоналям, с правого верхнего угла):");
            Matrix m1 = new Matrix(3, 4, true);
            Console.WriteLine(m1);

            Console.WriteLine("\nВторой массив 4x4 (в каждом столбце убывающая последовательность):");
            Matrix m2 = new Matrix(4, new Random());
            Console.WriteLine(m2);

            Console.WriteLine("\nТретий массив 5x5 (спираль):");
            Matrix m3 = new Matrix(5, true);
            Console.WriteLine(m3);
        }

        private void DemonstrateFibonacci()
        {
            int[,] arr = new int[,]
            {
                { 1, 2, 3, 4, 5 },
                { 6, 7, 8, 9, 10 },
                { 11, 12, 13, 14, 15 },
                { 16, 17, 18, 19, 20 }
            };

            Matrix m = new Matrix(arr);
            Console.WriteLine("Исходный массив:");
            Console.WriteLine(m);
            Console.WriteLine("\nВывод по числам Фибоначчи (1, 1, 2, 3, 5, ...):");
            m.PrintByFibonacci();
        }

        private void DemonstrateMatrixExpression()
        {
            Matrix A = new Matrix(new int[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix B = new Matrix(new int[,] { { 7, 8, 9 }, { 10, 11, 12 } });
            Matrix C = new Matrix(new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });

            Console.WriteLine("A =");
            Console.WriteLine(A);
            Console.WriteLine("\nB =");
            Console.WriteLine(B);
            Console.WriteLine("\nC =");
            Console.WriteLine(C);

            Matrix result = 2 * A + (B - C.Transpose());
            Console.WriteLine("\n2*A + (B - C^T) =");
            Console.WriteLine(result);
        }

        private void DemonstrateBinaryFile()
        {
            string path = "numbers.bin";
            FileTasks.FillBinaryFile(path, 10, -50, 100);
            Console.WriteLine("Файл " + path + " заполнен 10 случайными числами.");
            FileTasks.PrintBinaryFile(path);
            Console.WriteLine("Разность первого и максимального: " + FileTasks.DifferenceFirstAndMax(path));
        }

        private void DemonstrateXmlToys()
        {
            string path = "toys.xml";
            FileTasks.FillToysXml(path);
            Console.WriteLine("Файл " + path + " заполнен игрушками (XML).");
            FileTasks.PrintTextFile(path);
            Console.WriteLine("Стоимость кукол для детей 6 лет: " + FileTasks.PriceOfDollsForSixYears(path));
        }

        private void DemonstrateTextFileOnePerLine()
        {
            string input = "numbers6.txt";
            string output = "numbers6_out.txt";

            FileTasks.FillTextFileOnePerLine(input, 10, -50, 100);
            Console.WriteLine("Исходный файл " + input + ":");
            FileTasks.PrintTextFile(input);

            FileTasks.DecreaseEachByOne(input, output);
            Console.WriteLine("Новый файл " + output + " (каждый элемент −1):");
            FileTasks.PrintTextFile(output);
        }

        private void DemonstrateTextFileSeveralPerLine()
        {
            string path = "numbers7.txt";
            FileTasks.FillTextFileSeveralPerLine(path, 3, 5, -50, 100);
            Console.WriteLine("Исходный файл " + path + ":");
            FileTasks.PrintTextFile(path);
            Console.WriteLine("Разность первого и максимального: " + FileTasks.DifferenceFirstAndMaxInTextFile(path));
        }

        private void DemonstrateLinesStartingWithB()
        {
            string input = "text8.txt";
            string output = "text8_out.txt";

            FileTasks.FillTextFileWithText(input);
            Console.WriteLine("Исходный файл " + input + ":");
            FileTasks.PrintTextFile(input);

            FileTasks.CopyLinesStartingWithB(input, output);
            Console.WriteLine("Новый файл " + output + " (строки с «б»):");
            FileTasks.PrintTextFile(output);
        }
    }
}