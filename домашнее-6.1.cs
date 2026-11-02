using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void CountLetters(List<char> letters)
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
        Console.WriteLine("Домашнее задание 6.1. Подсчёт букв с помощью List.");
        Console.WriteLine("Имя текстового файла передаётся при запуске программы.");

        if (args.Length != 1)
        {
            Console.WriteLine("Передайте одно имя файла после -- при запуске.");
            return;
        }

        try
        {
            string text = File.ReadAllText(args[0]);
            List<char> letters = new List<char>();

            foreach (char letter in text)
            {
                letters.Add(letter);
            }

            CountLetters(letters);
        }
        catch (Exception error)
        {
            Console.WriteLine($"Ошибка: {error.Message}");
        }
    }
}