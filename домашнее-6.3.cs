using System;
using System.Collections.Generic;

class Program
{
    static double[] AverageTemperatures(
        Dictionary<string, int[]> temperature,
        string[] months)
    {
        double[] averages = new double[12];

        for (int i = 0; i < 12; i++)
        {
            int sum = 0;
            int[] days = temperature[months[i]];

            for (int j = 0; j < 30; j++)
            {
                sum = sum + days[j];
            }

            averages[i] = (double)sum / 30;
        }

        return averages;
    }

    static void Main()
    {
        Console.WriteLine("Домашнее задание 6.3. Температуры через Dictionary.");
        Console.WriteLine("Температуры создаются случайно. Ввод не требуется.");

        string[] months =
        {
            "Январь", "Февраль", "Март", "Апрель",
            "Май", "Июнь", "Июль", "Август",
            "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
        };

        Dictionary<string, int[]> temperature = new Dictionary<string, int[]>();
        Random random = new Random();

        for (int i = 0; i < 12; i++)
        {
            int[] days = new int[30];

            for (int j = 0; j < 30; j++)
            {
                days[j] = random.Next(-30, 36);
            }

            temperature.Add(months[i], days);
        }

        double[] averages = AverageTemperatures(temperature, months);
        double yearSum = 0;

        Console.WriteLine("Средние температуры по месяцам:");

        for (int i = 0; i < 12; i++)
        {
            Console.WriteLine($"{months[i]}: {averages[i]:F2}");
            yearSum = yearSum + averages[i];
        }

        double yearAverage = yearSum / 12;

        Console.WriteLine($"Средняя температура за год: {yearAverage:F2}");

        for (int i = 0; i < averages.Length - 1; i++)
        {
            for (int j = 0; j < averages.Length - 1 - i; j++)
            {
                if (averages[j] > averages[j + 1])
                {
                    double temp = averages[j];
                    averages[j] = averages[j + 1];
                    averages[j + 1] = temp;
                }
            }
        }

        Console.WriteLine("Средние температуры по возрастанию:");

        foreach (double average in averages)
        {
            Console.WriteLine($"{average:F2}");
        }
    }
}