using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FinalTask
{
    [Serializable]
    public class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }


    class Program
    {
        static void Main()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string studentsDirectoryPath = Path.Combine(desktopPath, "Students");
            Directory.CreateDirectory(studentsDirectoryPath);

            Student[] students = ReadStudentsFromBinaryFile("C:\\Users\\lukya.DESKTOP-MSN502K\\Downloads\\Students.dat");

            foreach (var student in students)
            {
                string groupFilePath = Path.Combine(studentsDirectoryPath, $"{student.Group}.txt");
                using (StreamWriter writer = new StreamWriter(groupFilePath, append: true))
                {
                    writer.WriteLine($"{student.Name}, {student.DateOfBirth:dd.MM.yyyy}");
                }
            }

            Console.WriteLine("Данные успешно загружены из бинарного формата в текст.");
        }

        static Student[] ReadStudentsFromBinaryFile(string filePath)
        {
            Student[] students;

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                try
                {
                    students = (Student[])formatter.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine($"Failed to deserialize. Reason: {e.Message}");
                    throw;
                }
            }

            return students;
        }
    }
}