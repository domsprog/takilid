using System;
using System.Collections.Generic;
using System.Linq;
using Models.Information;

namespace linq_assignment
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Initialize a list of students with their details and scores
            List<Students> students = new List<Students>
                {
                    new Students { StudentID = 101, FirstName = "Alice", LastName = "Smith", Scores = new List<int> { 85, 92, 78, 95 } },
                    new Students { StudentID = 102, FirstName = "Bob", LastName = "Jhonson", Scores = new List<int> { 76, 88, 90, 70 } },
                    new Students { StudentID = 103, FirstName = "Charlie", LastName = "Brown", Scores = new List<int> { 92, 95, 90, 98 } },
                    new Students { StudentID = 104, FirstName = "David", LastName = "Lee", Scores = new List<int> { 68, 75, 80, 70 } },
                    new Students { StudentID = 105, FirstName = "James", LastName = "Davis", Scores = new List<int> { 90, 89, 75, 80 } }
                };

            // Initialize another list of new students
            List<Students> newStudents = new List<Students>
                {
                    new Students { StudentID = 106, FirstName = "Frank", LastName = "Wilson", Scores = new List<int> { 88, 79, 91, 84 } },
                    new Students { StudentID = 107, FirstName = "Grace", LastName = "Taylor", Scores = new List<int> { 95, 89, 94, 87 } },
                    new Students { StudentID = 108, FirstName = "LasLas", LastName = "Halland", Scores = new List<int> { 95, 89, 70, 75 } },
                    new Students { StudentID = 109, FirstName = "Nico", LastName = "Hangaw", Scores = new List<int> { 80, 88, 89, 85 } },
                    new Students { StudentID = 110, FirstName = "Jan", LastName = "Gok", Scores = new List<int> { 86, 99, 90, 75 } },
                    new Students { StudentID = 111, FirstName = "Ping", LastName = "Pong", Scores = new List<int> { 75, 85, 95, 80 } },
                    new Students { StudentID = 112, FirstName = "Dom", LastName = "Nick", Scores = new List<int> { 90, 78, 89, 90 } },
                    new Students { StudentID = 113, FirstName = "Suc", LastName = "Gang", Scores = new List<int> { 81, 82, 83, 84 } },
                    new Students { StudentID = 114, FirstName = "Lea", LastName = "Cerna", Scores = new List<int> { 79, 83, 87, 89 } },
                    new Students { StudentID = 115, FirstName = "James", LastName = "Maano", Scores = new List<int> { 88, 90, 92, 95 } }
                };

            // Combine the two lists of students
            students.AddRange(newStudents);

            // Display students with an average score below 75
            Console.WriteLine("\nStudents with average score below 75:");
            var lowAvg = GetStudentsWithLowAverage(students);
            foreach (var name in lowAvg)
            {
                Console.WriteLine("- " + name);
            }

            // Display duplicate first names and their counts
            Console.WriteLine("\nDuplicate first names:");
            var duplicates = GetSameFirstNameCount(students);
            foreach (var entry in duplicates)
            {
                Console.WriteLine($"- {entry.Key}: {entry.Value} times");
            }

            // Group students by their grades for each subject and display the results
            Console.WriteLine("\nGrouped students by same grade per subject:");
            var groupedGrades = GroupStudentsBySameGradePerSubject(students);
            for (int i = 0; i < groupedGrades.Count; i++)
            {
                Console.WriteLine($"\nSubject {i + 1}:");
                foreach (var group in groupedGrades[i])
                {
                    Console.WriteLine($"Grade {group.Key}:");
                    foreach (var studentName in group.Value)
                    {
                        Console.WriteLine("- " + studentName);
                    }
                }
            }
        }

        // Filters and returns a list of students whose average score is below 75
        public static List<string> GetStudentsWithLowAverage(IEnumerable<Students> students)
        {
            return students
                .Where(s => s.Scores.Average() < 75) // Filter students with average score below 75
                .Select(s => s.FirstName) // Select their first names
                .ToList();
        }

        // Finds and counts duplicate first names among the students
        public static Dictionary<string, int> GetSameFirstNameCount(IEnumerable<Students> students)
        {
            return students
                .GroupBy(s => s.FirstName) // Group students by their first names
                .Where(g => g.Count() > 1) // Keep only groups with more than one student
                .ToDictionary(g => g.Key, g => g.Count()); // Convert to a dictionary with name and count
        }

        // Groups students by their grades for each subject
        public static List<Dictionary<int, List<string>>> GroupStudentsBySameGradePerSubject(IEnumerable<Students> students)
        {
            int subjectCount = students.First().Scores.Count; // Determine the number of subjects
            var result = new List<Dictionary<int, List<string>>>();

            for (int i = 0; i < subjectCount; i++) // Iterate through each subject
            {
                var grouped = students
                    .GroupBy(s => s.Scores[i]) // Group students by their grade in the current subject
                    .OrderBy(g => g.Key) // Order groups by grade
                    .ToDictionary(
                        g => g.Key, // Grade as the key
                        g => g.Select(s => $"{s.FirstName} {s.LastName}").ToList() // List of student names as the value
                    );

                result.Add(grouped); // Add the grouped data to the result
            }

            return result;
        }
    }
}
