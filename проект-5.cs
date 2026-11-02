using System;
using System.IO;
using System.Collections.Generic;

struct Student
{
    public string Surname;
    public string Name;
    public int Year;
    public string Exam;
    public int Score;
}

class Grandma
{
    public string Name;
    public int Age;
    public Dictionary<string, string> Diseases;
    public string HospitalName;
}

class Hospital
{
    public string Name;
    public List<string> Diseases;
    public int Capacity;
    public List<Grandma> Patients;
}

class Program
{
    static void ShowGrandmas(List<Grandma> grandmas)
    {
        Console.WriteLine("Список бабуль:");

        foreach (Grandma grandma in grandmas)
        {
            Console.WriteLine($"{grandma.Name}, возраст: {grandma.Age}");
            Console.WriteLine($"Место: {grandma.HospitalName}");

            foreach (string disease in grandma.Diseases.Keys)
            {
                Console.WriteLine($"{disease}: {grandma.Diseases[disease]}");
            }

            if (grandma.Diseases.Count == 0)
            {
                Console.WriteLine("Болезней нет, хочет только спросить.");
            }

            Console.WriteLine();
        }
    }

    static void ShowHospitals(Stack<Hospital> hospitals)
    {
        Console.WriteLine("Список больниц:");

        foreach (Hospital hospital in hospitals)
        {
            Console.WriteLine(hospital.Name);
            Console.WriteLine("Лечат болезни:");

            foreach (string disease in hospital.Diseases)
            {
                Console.WriteLine(disease);
            }

            double percent = (double)hospital.Patients.Count
                / hospital.Capacity * 100;

            Console.WriteLine($"Вместимость: {hospital.Capacity}");
            Console.WriteLine($"Бабуль в больнице: {hospital.Patients.Count}");
            Console.WriteLine($"Заполненность: {percent:F2}%");
            Console.WriteLine();
        }
    }

    static void FindPath(int[,] graph, int start, int finish)
    {
        int count = graph.GetLength(0);
        Queue<int> queue = new Queue<int>();
        bool[] visited = new bool[count];
        int[] previous = new int[count];

        for (int i = 0; i < count; i++)
        {
            previous[i] = -1;
        }

        queue.Enqueue(start);
        visited[start] = true;

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            for (int next = 0; next < count; next++)
            {
                if (graph[current, next] == 1 && !visited[next])
                {
                    visited[next] = true;
                    previous[next] = current;
                    queue.Enqueue(next);
                }
            }
        }

        if (!visited[finish])
        {
            Console.WriteLine("Путь не найден.");
            return;
        }

        Stack<int> path = new Stack<int>();
        int vertex = finish;

        while (vertex != -1)
        {
            path.Push(vertex);
            vertex = previous[vertex];
        }

        Console.WriteLine($"Длина пути: {path.Count - 1}");
        Console.WriteLine("Кратчайший путь:");

        while (path.Count > 0)
        {
            Console.Write(path.Pop());

            if (path.Count > 0)
            {
                Console.Write(" -> ");
            }
        }

        Console.WriteLine();
    }

    static void Main()
    {
        Console.WriteLine("Проект 5.");
        Console.WriteLine("1 - Перемешивание картинок");
        Console.WriteLine("2 - Студенты");
        Console.WriteLine("3 - Бабули и больницы");
        Console.WriteLine("4 - Кратчайший путь в графе");
        Console.Write("Выберите задачу от 1 до 4: ");

        try
        {
            int task = int.Parse(Console.ReadLine());

            switch (task)
            {
                case 1:
                {
                    Console.WriteLine("Задача 1. Перемешивание 32 пар картинок.");
                    Console.WriteLine("В папке Images нужны файлы от 1.jpg до 32.jpg.");

                    List<string> pictures = new List<string>();

                    for (int i = 1; i <= 32; i++)
                    {
                        string path = "Images/" + i + ".jpg";

                        if (!File.Exists(path))
                        {
                            Console.WriteLine($"Не найден файл: {path}");
                            return;
                        }

                        pictures.Add(path);
                        pictures.Add(path);
                    }

                    Console.WriteLine("Исходный список:");

                    foreach (string picture in pictures)
                    {
                        Console.Write(picture + " ");
                    }

                    Console.WriteLine();
                    Random random = new Random();

                    for (int i = 0; i < pictures.Count; i++)
                    {
                        int j = random.Next(i, pictures.Count);

                        string temp = pictures[i];
                        pictures[i] = pictures[j];
                        pictures[j] = temp;
                    }

                    Console.WriteLine("Перемешанный список:");

                    foreach (string picture in pictures)
                    {
                        Console.Write(picture + " ");
                    }

                    Console.WriteLine();
                    Console.WriteLine($"Всего элементов: {pictures.Count}");
                    break;
                }

                case 2:
                {
                    Console.WriteLine("Задача 2. Работа со студентами.");
                    Console.WriteLine("Используются вымышленные учебные данные.");
                    Console.WriteLine("Изменения действуют до завершения программы.");

                    if (!File.Exists("students.txt"))
                    {
                        string[] initialStudents =
                        {
                            "Арбузов;Миша;2008;Информатика;250",
                            "Дыньков;Саша;2008;Физика;240",
                            "Абрикосов;Дима;2008;Информатика;270",
                            "Бутербродов;Женя;2008;Физика;260",
                            "Яблоков;Артем;2008;Информатика;280",
                            "Бананов;Илья;2008;Физика;230",
                            "Лимонов;Егор;2008;Информатика;255",
                            "Персиков;Максим;2008;Физика;245",
                            "Апельсинов;Кирилл;2008;Информатика;275",
                            "Виноградов;Андрей;2008;Физика;265"
                        };

                        File.WriteAllLines("students.txt", initialStudents);
                    }

                    List<Student> students = new List<Student>();
                    Dictionary<string, Student> dictionary =
                        new Dictionary<string, Student>();

                    string[] lines = File.ReadAllLines("students.txt");

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(';');

                        if (parts.Length != 5)
                        {
                            throw new Exception("В строке файла должно быть пять полей.");
                        }

                        Student student = new Student();
                        student.Surname = parts[0];
                        student.Name = parts[1];
                        student.Year = int.Parse(parts[2]);
                        student.Exam = parts[3];
                        student.Score = int.Parse(parts[4]);

                        string key = student.Surname + " " + student.Name;

                        dictionary.Add(key, student);
                        students.Add(student);
                    }

                    string command = "";

                    while (command != "выход")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Новый студент, Удалить, Сортировать, Показать, Выход");
                        Console.Write("Введите команду: ");
                        command = Console.ReadLine().ToLower();

                        switch (command)
                        {
                            case "новый студент":
                            {
                                Student student = new Student();

                                Console.Write("Фамилия: ");
                                student.Surname = Console.ReadLine();

                                Console.Write("Имя: ");
                                student.Name = Console.ReadLine();

                                Console.Write("Год рождения: ");
                                student.Year = int.Parse(Console.ReadLine());

                                Console.Write("Вступительный экзамен: ");
                                student.Exam = Console.ReadLine();

                                Console.Write("Сумма баллов: ");
                                student.Score = int.Parse(Console.ReadLine());

                                string key = student.Surname + " " + student.Name;

                                if (dictionary.ContainsKey(key))
                                {
                                    Console.WriteLine("Такой студент уже есть.");
                                }
                                else if (student.Year <= 0 || student.Score < 0)
                                {
                                    Console.WriteLine("Неверный год рождения или баллы.");
                                }
                                else
                                {
                                    students.Add(student);
                                    dictionary.Add(key, student);
                                    Console.WriteLine("Студент добавлен.");
                                }

                                break;
                            }

                            case "удалить":
                            {
                                Console.Write("Фамилия: ");
                                string surname = Console.ReadLine();

                                Console.Write("Имя: ");
                                string name = Console.ReadLine();

                                string key = surname + " " + name;

                                if (!dictionary.ContainsKey(key))
                                {
                                    Console.WriteLine("Студент не найден.");
                                    break;
                                }

                                for (int i = 0; i < students.Count; i++)
                                {
                                    if (students[i].Surname == surname
                                        && students[i].Name == name)
                                    {
                                        students.RemoveAt(i);
                                        break;
                                    }
                                }

                                dictionary.Remove(key);
                                Console.WriteLine("Студент удалён.");
                                break;
                            }

                            case "сортировать":
                            {
                                for (int i = 0; i < students.Count - 1; i++)
                                {
                                    for (int j = 0; j < students.Count - 1 - i; j++)
                                    {
                                        if (students[j].Score > students[j + 1].Score)
                                        {
                                            Student temp = students[j];
                                            students[j] = students[j + 1];
                                            students[j + 1] = temp;
                                        }
                                    }
                                }

                                Console.WriteLine("Список отсортирован по возрастанию баллов.");
                                break;
                            }

                            case "показать":
                            {
                                foreach (Student item in students)
                                {
                                    string key = item.Surname + " " + item.Name;
                                    Student student = dictionary[key];

                                    Console.WriteLine(
                                        $"{student.Surname} {student.Name}, "
                                        + $"{student.Year}, {student.Exam}, "
                                        + $"баллы: {student.Score}");
                                }

                                break;
                            }

                            case "выход":
                                break;

                            default:
                                Console.WriteLine("Неизвестная команда.");
                                break;
                        }
                    }

                    break;
                }

                case 3:
                {
                    Console.WriteLine("Задача 3. Распределение бабуль по больницам.");
                    Console.WriteLine("Болезни и лекарства вводятся как учебные данные.");

                    Stack<Hospital> hospitals = new Stack<Hospital>();

                    Hospital first = new Hospital();
                    first.Name = "Больница №1";
                    first.Diseases = new List<string> { "грипп", "диабет" };
                    first.Capacity = 3;
                    first.Patients = new List<Grandma>();

                    Hospital second = new Hospital();
                    second.Name = "Больница №2";
                    second.Diseases = new List<string> { "гипертония", "артрит" };
                    second.Capacity = 2;
                    second.Patients = new List<Grandma>();

                    Hospital third = new Hospital();
                    third.Name = "Больница №3";
                    third.Diseases = new List<string> { "грипп", "гипертония", "артрит" };
                    third.Capacity = 4;
                    third.Patients = new List<Grandma>();

                    hospitals.Push(third);
                    hospitals.Push(second);
                    hospitals.Push(first);

                    Queue<Grandma> queue = new Queue<Grandma>();
                    List<Grandma> grandmas = new List<Grandma>();

                    ShowHospitals(hospitals);

                    string command = "";

                    while (command != "выход")
                    {
                        Console.WriteLine("Добавить, Распределить, Показать, Выход");
                        Console.Write("Введите команду: ");
                        command = Console.ReadLine().ToLower();

                        switch (command)
                        {
                            case "добавить":
                            {
                                Grandma grandma = new Grandma();

                                Console.Write("Имя бабули: ");
                                grandma.Name = Console.ReadLine();

                                Console.Write("Возраст: ");
                                grandma.Age = int.Parse(Console.ReadLine());

                                Console.Write("Количество болезней: ");
                                int count = int.Parse(Console.ReadLine());

                                if (grandma.Age <= 0 || count < 0)
                                {
                                    Console.WriteLine("Неверный возраст или количество болезней.");
                                    break;
                                }

                                grandma.Diseases = new Dictionary<string, string>();
                                grandma.HospitalName = "В очереди";

                                for (int i = 0; i < count; i++)
                                {
                                    Console.Write("Болезнь: ");
                                    string disease = Console.ReadLine().ToLower();

                                    Console.Write("Лекарство: ");
                                    string medicine = Console.ReadLine();

                                    if (grandma.Diseases.ContainsKey(disease))
                                    {
                                        Console.WriteLine("Эта болезнь уже введена. Введите другую.");
                                        i--;
                                    }
                                    else
                                    {
                                        grandma.Diseases.Add(disease, medicine);
                                    }
                                }

                                queue.Enqueue(grandma);
                                grandmas.Add(grandma);

                                Console.WriteLine("Бабуля добавлена в очередь.");
                                break;
                            }

                            case "распределить":
                            {
                                while (queue.Count > 0)
                                {
                                    Grandma grandma = queue.Dequeue();
                                    bool admitted = false;

                                    foreach (Hospital hospital in hospitals)
                                    {
                                        if (hospital.Patients.Count >= hospital.Capacity)
                                        {
                                            continue;
                                        }

                                        int matches = 0;

                                        foreach (string disease in grandma.Diseases.Keys)
                                        {
                                            foreach (string treated in hospital.Diseases)
                                            {
                                                if (disease == treated)
                                                {
                                                    matches++;
                                                    break;
                                                }
                                            }
                                        }

                                        if (grandma.Diseases.Count == 0
                                            || matches * 2 > grandma.Diseases.Count)
                                        {
                                            hospital.Patients.Add(grandma);
                                            grandma.HospitalName = hospital.Name;
                                            admitted = true;
                                            break;
                                        }
                                    }

                                    if (admitted)
                                    {
                                        Console.WriteLine(
                                            $"{grandma.Name} попала в {grandma.HospitalName}.");
                                    }
                                    else
                                    {
                                        grandma.HospitalName = "На улице";
                                        Console.WriteLine(
                                            $"{grandma.Name} осталась на улице плакать.");
                                    }
                                }

                                ShowGrandmas(grandmas);
                                ShowHospitals(hospitals);
                                break;
                            }

                            case "показать":
                                ShowGrandmas(grandmas);
                                ShowHospitals(hospitals);
                                break;

                            case "выход":
                                ShowGrandmas(grandmas);
                                ShowHospitals(hospitals);
                                break;

                            default:
                                Console.WriteLine("Неизвестная команда.");
                                break;
                        }
                    }

                    break;
                }

                case 4:
                {
                    Console.WriteLine("Задача 4. Кратчайший путь в графе.");
                    Console.WriteLine("Граф задан в коде, все рёбра имеют длину 1.");

                    int[,] graph =
                    {
                        { 0, 1, 1, 0, 0, 0, 0 },
                        { 1, 0, 0, 1, 1, 0, 0 },
                        { 1, 0, 0, 0, 1, 0, 0 },
                        { 0, 1, 0, 0, 0, 1, 0 },
                        { 0, 1, 1, 0, 0, 1, 0 },
                        { 0, 0, 0, 1, 1, 0, 1 },
                        { 0, 0, 0, 0, 0, 1, 0 }
                    };

                    Console.WriteLine("Соединения вершин:");

                    for (int i = 0; i < 7; i++)
                    {
                        for (int j = i + 1; j < 7; j++)
                        {
                            if (graph[i, j] == 1)
                            {
                                Console.WriteLine($"{i} - {j}");
                            }
                        }
                    }

                    Console.Write("Начальная вершина от 0 до 6: ");
                    int start = int.Parse(Console.ReadLine());

                    Console.Write("Конечная вершина от 0 до 6: ");
                    int finish = int.Parse(Console.ReadLine());

                    if (start < 0 || start > 6 || finish < 0 || finish > 6)
                    {
                        Console.WriteLine("Неверный номер вершины.");
                        break;
                    }

                    FindPath(graph, start, finish);
                    break;
                }

                default:
                    Console.WriteLine("Нужно выбрать задачу от 1 до 4.");
                    break;
            }
        }
        catch (Exception error)
        {
            Console.WriteLine($"Ошибка: {error.Message}");
        }
    }
}