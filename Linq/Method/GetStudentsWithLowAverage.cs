using Models.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Method
{
    public class Methods
    {
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
