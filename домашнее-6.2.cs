using System;
using System.Collections.Generic;

class Program
{
    static void PrintMatrix(LinkedList<LinkedList<int>> matrix)
    {
        foreach (LinkedList<int> row in matrix)
        {
            foreach (int number in row)
            {
                Console.Write(number + "\t");
            }

            Console.WriteLine();
        }
    }

    static LinkedList<LinkedList<int>> MultiplyMatrices(
        LinkedList<LinkedList<int>> first,
        LinkedList<LinkedList<int>> second)
    {
        if (first.Count == 0 || second.Count == 0)
        {
            throw new Exception("Матрицы не должны быть пустыми.");
        }

        int columns = first.First.Value.Count;

        foreach (LinkedList<int> row in first)
        {
            if (row.Count != columns)
            {
                throw new Exception("Строки первой матрицы должны иметь одинаковую длину.");
            }
        }

        int secondColumns = second.First.Value.Count;

        foreach (LinkedList<int> row in second)
        {
            if (row.Count != secondColumns)
            {
                throw new Exception("Строки второй матрицы должны иметь одинаковую длину.");
            }
        }

        if (columns != second.Count)
        {
            throw new Exception("Число столбцов первой матрицы должно совпадать с числом строк второй.");
        }

        LinkedList<LinkedList<int>> result = new LinkedList<LinkedList<int>>();

        foreach (LinkedList<int> firstRow in first)
        {
            LinkedList<int> resultRow = new LinkedList<int>();

            for (int j = 0; j < secondColumns; j++)
            {
                int sum = 0;
                LinkedListNode<int> firstNumber = firstRow.First;

                foreach (LinkedList<int> secondRow in second)
                {
                    LinkedListNode<int> secondNumber = secondRow.First;

                    for (int k = 0; k < j; k++)
                    {
                        secondNumber = secondNumber.Next;
                    }

                    sum = sum + firstNumber.Value * secondNumber.Value;
                    firstNumber = firstNumber.Next;
                }

                resultRow.AddLast(sum);
            }

            result.AddLast(resultRow);
        }

        return result;
    }

    static void Main()
    {
        Console.WriteLine("Домашнее задание 6.2. Умножение матриц через LinkedList.");
        Console.WriteLine("Матрицы заданы в коде. Ввод не требуется.");

        LinkedList<LinkedList<int>> first = new LinkedList<LinkedList<int>>();

        LinkedList<int> row1 = new LinkedList<int>();
        row1.AddLast(1);
        row1.AddLast(2);
        row1.AddLast(3);
        first.AddLast(row1);

        LinkedList<int> row2 = new LinkedList<int>();
        row2.AddLast(4);
        row2.AddLast(5);
        row2.AddLast(6);
        first.AddLast(row2);

        LinkedList<LinkedList<int>> second = new LinkedList<LinkedList<int>>();

        LinkedList<int> row3 = new LinkedList<int>();
        row3.AddLast(7);
        row3.AddLast(8);
        second.AddLast(row3);

        LinkedList<int> row4 = new LinkedList<int>();
        row4.AddLast(9);
        row4.AddLast(10);
        second.AddLast(row4);

        LinkedList<int> row5 = new LinkedList<int>();
        row5.AddLast(11);
        row5.AddLast(12);
        second.AddLast(row5);

        Console.WriteLine("Первая матрица:");
        PrintMatrix(first);

        Console.WriteLine("Вторая матрица:");
        PrintMatrix(second);

        try
        {
            LinkedList<LinkedList<int>> result = MultiplyMatrices(first, second);

            Console.WriteLine("Результат умножения:");
            PrintMatrix(result);
        }
        catch (Exception error)
        {
            Console.WriteLine($"Ошибка: {error.Message}");
        }
    }
}