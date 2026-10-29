using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Laba3
{
    public static class FileTasks
    {
        // ============================================================
        // ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ДЛЯ ВЫВОДА В КОНСОЛЬ
        // ============================================================

        public static void PrintTextFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Файл не найден: " + path);
                return;
            }
            Console.WriteLine("--- Содержимое " + path + " ---");
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines) Console.WriteLine(line);
            Console.WriteLine("-----------------------------");
        }

        public static void PrintBinaryFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Файл не найден: " + path);
                return;
            }
            Console.WriteLine("--- Содержимое " + path + " ---");
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    Console.Write(reader.ReadInt32() + " ");
                }
            }
            Console.WriteLine("\n-----------------------------");
        }

        // ============================================================
        // Задание 4, задача 14. Бинарные файлы
        // ============================================================

        public static void FillBinaryFile(string path, int count, int min, int max)
        {
            Random random = new Random();
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                for (int i = 0; i < count; i++) writer.Write(random.Next(min, max));
            }
        }

        public static int DifferenceFirstAndMax(string path)
        {
            int first = 0, max = int.MinValue;
            bool isFirst = true;

            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int value = reader.ReadInt32();
                    if (isFirst) { first = value; max = value; isFirst = false; }
                    else if (value > max) max = value;
                }
            }

            if (isFirst) throw new InvalidOperationException("Файл пуст.");
            return first - max;
        }

        // ============================================================
        // Задание 5, задача 14. XML-сериализация
        // ============================================================

        public static void FillToysXml(string path)
        {
            List<Toy> toys = new List<Toy>
            {
                new Toy { Name = "Кукла Барби", Price = 1500.0, MinAge = 5, MaxAge = 8 },
                new Toy { Name = "Кукла Маша", Price = 800.0, MinAge = 3, MaxAge = 6 },
                new Toy { Name = "Кукла Оля", Price = 1200.0, MinAge = 6, MaxAge = 10 },
                new Toy { Name = "Конструктор", Price = 2000.0, MinAge = 6, MaxAge = 12 },
                new Toy { Name = "Кубики", Price = 500.0, MinAge = 2, MaxAge = 5 },
                new Toy { Name = "Мяч", Price = 300.0, MinAge = 3, MaxAge = 10 }
            };

            XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(fs, toys);
            }
        }

        public static double PriceOfDollsForSixYears(string path)
        {
            List<Toy> toys;
            XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                toys = (List<Toy>)serializer.Deserialize(fs);
            }

            double sum = 0;
            foreach (Toy toy in toys)
            {
                if (toy.Name.StartsWith("Кукла", StringComparison.Ordinal)
                    && toy.MinAge <= 6 && toy.MaxAge >= 6)
                    sum += toy.Price;
            }
            return sum;
        }

        // ============================================================
        // Задание 6, задача 14. Текстовый файл (1 в строке)
        // ============================================================

        public static void FillTextFileOnePerLine(string path, int count, int min, int max)
        {
            Random random = new Random();
            using (StreamWriter writer = new StreamWriter(path))
            {
                for (int i = 0; i < count; i++) writer.WriteLine(random.Next(min, max));
            }
        }

        public static void DecreaseEachByOne(string inputPath, string outputPath)
        {
            using (StreamReader reader = new StreamReader(inputPath))
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    int value;
                    if (int.TryParse(line, out value)) writer.WriteLine(value - 1);
                }
            }
        }

        // ============================================================
        // Задание 7, задача 14. Текстовый файл (несколько в строке)
        // ============================================================

        public static void FillTextFileSeveralPerLine(string path, int rows, int cols, int min, int max)
        {
            Random random = new Random();
            using (StreamWriter writer = new StreamWriter(path))
            {
                for (int i = 0; i < rows; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j < cols; j++)
                    {
                        sb.Append(random.Next(min, max));
                        if (j < cols - 1) sb.Append(" ");
                    }
                    writer.WriteLine(sb.ToString());
                }
            }
        }

        public static int DifferenceFirstAndMaxInTextFile(string path)
        {
            int first = 0, max = int.MinValue;
            bool isFirst = true;

            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string part in parts)
                    {
                        int value;
                        if (int.TryParse(part, out value))
                        {
                            if (isFirst) { first = value; max = value; isFirst = false; }
                            else if (value > max) max = value;
                        }
                    }
                }
            }

            if (isFirst) throw new InvalidOperationException("Файл не содержит чисел.");
            return first - max;
        }

        // ============================================================
        // Задание 8, задача 14. Текстовый файл (строки с «б»)
        // ============================================================

        public static void FillTextFileWithText(string path)
        {
            string[] lines = {
                "Барсик любит рыбу.",
                "большой дом стоит у реки.",
                "Бабушка испекла пирог.",
                "Кот спит на диване.",
                "белый снег кружится.",
                "Мышка убежала в норку.",
                "борщ — вкусное блюдо."
            };

            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (string line in lines) writer.WriteLine(line);
            }
        }

        public static void CopyLinesStartingWithB(string inputPath, string outputPath)
        {
            using (StreamReader reader = new StreamReader(inputPath))
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Length > 0)
                    {
                        char first = line[0];
                        if (first == 'б' || first == 'Б') writer.WriteLine(line);
                    }
                }
            }
        }
    }
}