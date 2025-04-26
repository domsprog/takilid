using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using linq_assignment;
using Models.Information;
using System.Linq;

namespace LinqAssignmentTests
{
    [TestClass]
    public class ProgramTests
    {
        private List<Students> students = new();

        [TestInitialize]
        public void Setup()
        {
            students = new List<Students>
            {
                new Students { StudentID = 101, FirstName = "Alice", LastName = "Smith", Scores = new List<int> { 85, 92, 78, 95 } },
                new Students { StudentID = 102, FirstName = "Bob", LastName = "Johnson", Scores = new List<int> { 76, 88, 90, 70 } },
                new Students { StudentID = 103, FirstName = "Charlie", LastName = "Brown", Scores = new List<int> { 92, 95, 90, 98 } },
                new Students { StudentID = 104, FirstName = "David", LastName = "Lee", Scores = new List<int> { 68, 75, 80, 70 } },
                new Students { StudentID = 105, FirstName = "James", LastName = "Davis", Scores = new List<int> { 90, 89, 75, 80 } }
            };
        }

        [TestMethod]
        public void TestGetStudentsWithLowAverage()
        {
            var result = Program.GetStudentsWithLowAverage(students);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("David", result[0]);
        }

        [TestMethod]
        public void TestGetSameFirstNameCount()
        {
            var result = Program.GetSameFirstNameCount(students);
            Assert.AreEqual(0, result.Count); // No duplicated first names
        }

        [TestMethod]
        public void TestGroupStudentsBySameGradePerSubject()
        {
            var result = Program.GroupStudentsBySameGradePerSubject(students);

            Assert.AreEqual(4, result.Count); // 4 subjects
            Assert.IsTrue(result[1].Any(g => g.Value.Contains("Alice Smith")));  // Check Alice in subject 2 grades
        }
    }
}
