using System;

class Program
{
    static void PrintMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }

            Console.WriteLine();
        }
    }

    static int[,] MultiplyMatrices(int[,] first, int[,] second)
    {
        if (first.GetLength(1) != second.GetLength(0))
        {
            throw new Exception("Число столбцов первой матрицы должно совпадать с числом строк второй.");
        }

        int[,] result = new int[first.GetLength(0), second.GetLength(1)];

        for (int i = 0; i < first.GetLength(0); i++)
        {
            for (int j = 0; j < second.GetLength(1); j++)
            {
                for (int k = 0; k < first.GetLength(1); k++)
                {
                    result[i, j] = result[i, j] + first[i, k] * second[k, j];
                }
            }
        }

        return result;
    }

    static void Main()
    {
        Console.WriteLine("Упражнение 6.2. Умножение двух матриц.");
        Console.WriteLine("Матрицы заданы в коде. Ввод не требуется.");

        int[,] first =
        {
            { 1, 2, 3 },
            { 4, 5, 6 }
        };

        int[,] second =
        {
            { 7, 8 },
            { 9, 10 },
            { 11, 12 }
        };

        Console.WriteLine("Первая матрица:");
        PrintMatrix(first);

        Console.WriteLine("Вторая матрица:");
        PrintMatrix(second);

        try
        {
            int[,] result = MultiplyMatrices(first, second);

            Console.WriteLine("Результат умножения:");
            PrintMatrix(result);
        }
        catch (Exception error)
        {
            Console.WriteLine($"Ошибка: {error.Message}");
        }
    }
}