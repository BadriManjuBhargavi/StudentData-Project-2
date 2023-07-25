using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Student
{
    public string Name { get; set; }
    public string Class { get; set; }
}

class Program
{
    static void Main()
    {
        string filePath = "C:\\Users\\kalpa\\OneDrive\\Documents\\Student data 2.txt";
        List<Student> students = ReadStudentData(filePath);

        if (students.Count == 0)
        {
            Console.WriteLine("No student data found.");
            return;
        }

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("1. Display all students");
            Console.WriteLine("2. Sort students by name");
            Console.WriteLine("3. Search for a student by name");
            Console.WriteLine("4. Exit");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    DisplayStudents(students);
                    break;
                case 2:
                    SortStudentsByName(students);
                    break;
                case 3:
                    SearchStudentByName(students);
                    break;
                case 4:
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static List<Student> ReadStudentData(string filePath)
    {
        List<Student> students = new List<Student>();

        try
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] data = line.Split(',');
                if (data.Length == 2)
                {
                    Student student = new Student
                    {
                        Name = data[0].Trim(),
                        Class = data[1].Trim()
                    };
                    students.Add(student);
                }
            }
        }
        catch (FileNotFoundException)
        {
            // File not found, no data to read
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error while reading data: " + ex.Message);
        }

        return students;
    }

    static void DisplayStudents(List<Student> students)
    {
        if (students.Count == 0)
        {
            Console.WriteLine("No student data available.");
            return;
        }

        Console.WriteLine("Student List:");
        foreach (var student in students)
        {
            Console.WriteLine($"Name: {student.Name}, Class: {student.Class}");
        }
    }

    static void SortStudentsByName(List<Student> students)
    {
        students.Sort((s1, s2) => s1.Name.CompareTo(s2.Name));
        Console.WriteLine("Students sorted by name:");
        DisplayStudents(students);
    }

    static void SearchStudentByName(List<Student> students)
    {
        Console.Write("Enter the student name to search: ");
        string searchName = Console.ReadLine().Trim();

        List<Student> searchResults = students.FindAll(s => s.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

        if (searchResults.Count > 0)
        {
            Console.WriteLine($"Found {searchResults.Count} student(s) with the name '{searchName}':");
            DisplayStudents(searchResults);
        }
        else
        {
            Console.WriteLine($"No student found with the name '{searchName}'.");
        }
    }
}

