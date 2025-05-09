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

            return (from student in students
                    where student.Scores.Average() < 75
                    select student.FirstName).ToList();
        }


        // Finds and counts duplicate first names among the students
        public static Dictionary<string, int> GetSameFirstName(IEnumerable<Students> students)
        {
            return
                  (from s in students
                  group s by s.FirstName into g
                  where g.Count() > 1
                  select new
                  {
                      firstName = g.Key,
                      count = g.Count()
                  }).ToDictionary(x => x.firstName, x => x.count);
        }

        // Groups students by their grades for each subject
        public static List<Dictionary<int, List<string>>> GroupStudentsBySameGradePerSubject(IEnumerable<Students> students)
        {
            int subjectCount = students.First().Scores.Count; // Determine the number of subjects
            var result = new List<Dictionary<int, List<string>>>();

            for (int i = 0; i < subjectCount; i++) // Iterate through each subject
            {
                var grouped = (
                    from s in students
                    group s by s.Scores[i] into g
                    orderby g.Key
                    select new
                    {
                        Grade = g.Key,
                        Names = (from s in g select $"{s.FirstName} {s.LastName}").ToList()
                    }
                ).ToDictionary(x => x.Grade, x => x.Names);

                result.Add(grouped); // Add the grouped data to the result
            }

            return result;
        }
    }
}
