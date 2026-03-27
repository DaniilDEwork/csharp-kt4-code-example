using System;
using System.IO;

class AgeException : Exception
{
    public AgeException(string message) : base(message)
    {
    }
}

class Program
{
    static void Main()
    {
        string goodPath = "test.txt";
        string badFilePath = "missing.txt";
        string badDirectoryPath = @"NoFolder\test.txt";

        File.WriteAllText(goodPath, "Это тестовый файл.");

        Console.WriteLine("1. Пример try-catch");
        ReadFileTryCatch(goodPath);
        ReadFileTryCatch(badFilePath);
        ReadFileTryCatch(badDirectoryPath);

        Console.WriteLine();
        Console.WriteLine("2. Пример try-finally");
        ReadFileFinally(goodPath);

        Console.WriteLine();
        Console.WriteLine("3. Пример using");
        ReadFileUsing(goodPath);

        Console.WriteLine();
        Console.WriteLine("4. Пример FormatException");
        ParseNumber("25");
        ParseNumber("abc");

        Console.WriteLine();
        Console.WriteLine("5. Пример своего исключения");
        RegisterUser(18);
        RegisterUser(12);

        Console.ReadLine();
    }

    static void ReadFileTryCatch(string path)
    {
        try
        {
            string text = File.ReadAllText(path);
            Console.WriteLine("Содержимое файла: " + text);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Ошибка: файл не найден");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Ошибка: папка не найдена");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Другая ошибка: " + ex.Message);
        }
    }

    static void ReadFileFinally(string path)
    {
        StreamReader reader = null;

        try
        {
            reader = new StreamReader(path);
            string text = reader.ReadToEnd();
            Console.WriteLine("Содержимое файла: " + text);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
                Console.WriteLine("Файл закрыт в finally");
            }
        }
    }

    static void ReadFileUsing(string path)
    {
        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string text = reader.ReadToEnd();
                Console.WriteLine("Содержимое файла: " + text);
            }

            Console.WriteLine("Файл закрыт автоматически через using");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }

    static void ParseNumber(string value)
    {
        try
        {
            int number = int.Parse(value);
            Console.WriteLine("Число: " + number);
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: введено не число");
        }
    }

    static void RegisterUser(int age)
    {
        try
        {
            if (age < 14)
            {
                throw new AgeException("Регистрация запрещена для пользователей младше 14 лет");
            }

            Console.WriteLine("Пользователь зарегистрирован. Возраст: " + age);
        }
        catch (AgeException ex)
        {
            Console.WriteLine("Ошибка регистрации: " + ex.Message);
        }
    }
}