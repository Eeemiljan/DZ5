using System;

class Program
{
    static double[] AverageTemperatures(int[,] temperature)
    {
        double[] averages = new double[12];

        for (int i = 0; i < 12; i++)
        {
            int sum = 0;

            for (int j = 0; j < 30; j++)
            {
                sum = sum + temperature[i, j];
            }

            averages[i] = (double)sum / 30;
        }

        return averages;
    }

    static void Main()
    {
        Console.WriteLine("Упражнение 6.3. Средняя температура по месяцам и за год.");
        Console.WriteLine("Температуры создаются случайно. Ввод не требуется.");

        int[,] temperature = new int[12, 30];
        Random random = new Random();

        string[] months =
        {
            "Январь", "Февраль", "Март", "Апрель",
            "Май", "Июнь", "Июль", "Август",
            "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
        };

        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                temperature[i, j] = random.Next(-30, 36);
            }
        }

        double[] averages = AverageTemperatures(temperature);
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