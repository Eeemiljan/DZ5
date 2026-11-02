using System;
using System.IO;

class Program
{
    static void CountLetters(char[] letters)
    {
        string vowels = "аеёиоуыэюяАЕЁИОУЫЭЮЯaeiouAEIOU";
        string consonants = "бвгджзйклмнпрстфхцчшщБВГДЖЗЙКЛМНПРСТФХЦЧШЩbcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ";

        int vowelCount = 0;
        int consonantCount = 0;

        foreach (char letter in letters)
        {
            foreach (char vowel in vowels)
            {
                if (letter == vowel)
                {
                    vowelCount++;
                    break;
                }
            }

            foreach (char consonant in consonants)
            {
                if (letter == consonant)
                {
                    consonantCount++;
                    break;
                }
            }
        }

        Console.WriteLine($"Гласных букв: {vowelCount}");
        Console.WriteLine($"Согласных букв: {consonantCount}");
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Упражнение 6.1. Подсчёт гласных и согласных букв в файле.");
        Console.WriteLine("Имя текстового файла передаётся при запуске программы.");

        if (args.Length != 1)
        {
            Console.WriteLine("Передайте одно имя файла после -- при запуске.");
            return;
        }

        try
        {
            string text = File.ReadAllText(args[0]);
            char[] letters = text.ToCharArray();

            CountLetters(letters);
        }
        catch (Exception error)
        {
            Console.WriteLine($"Ошибка: {error.Message}");
        }
    }
}