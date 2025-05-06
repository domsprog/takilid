using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Information;
using linq_assignment;

namespace linq_assignment.Tests
{
    [TestClass]
    public class ProgramTests
    {
        private List<Students> testStudents;

        [TestInitialize]
        public void Setup()
        {
            // Initialize test data
            testStudents = new List<Students>
            {
                new Students { StudentID = 101, FirstName = "Alice", LastName = "Smith", Scores = new List<int> { 85, 92, 78, 95 } },
                new Students { StudentID = 102, FirstName = "Bob", LastName = "Johnson", Scores = new List<int> { 70, 68, 72, 65 } }, // Average = 68.75 (below 75)
                new Students { StudentID = 103, FirstName = "Charlie", LastName = "Brown", Scores = new List<int> { 92, 95, 90, 98 } },
                new Students { StudentID = 104, FirstName = "David", LastName = "Lee", Scores = new List<int> { 68, 75, 60, 70 } },    // Average = 68.25 (below 75)
                new Students { StudentID = 105, FirstName = "Alice", LastName = "Davis", Scores = new List<int> { 90, 89, 75, 80 } },
                new Students { StudentID = 106, FirstName = "James", LastName = "Wilson", Scores = new List<int> { 88, 79, 91, 84 } },
                new Students { StudentID = 107, FirstName = "James", LastName = "Taylor", Scores = new List<int> { 95, 89, 94, 87 } }
            };
        }

        [TestMethod]
        public void TestGetStudentsWithLowAverage()
        {
            // Arrange
            var expectedStudents = new List<string> { "Bob", "David" };

            // Act
            var result = Program.GetStudentsWithLowAverage(testStudents);

            // Assert
            CollectionAssert.AreEqual(expectedStudents, result, "Should return students with average score below 75");
            Assert.AreEqual(2, result.Count, "Should return exactly 2 students with average below 75");
        }

        [TestMethod]
        public void TestGetSameFirstNameCount()
        {
            // Arrange
            var expectedDuplicates = new Dictionary<string, int>
            {
                { "Alice", 2 },
                { "James", 2 }
            };

            // Act
            var result = Program.GetSameFirstNameCount(testStudents);

            // Assert
            Assert.AreEqual(expectedDuplicates.Count, result.Count, "Should find the correct number of duplicate names");

            foreach (var key in expectedDuplicates.Keys)
            {
                Assert.IsTrue(result.ContainsKey(key), $"Result should contain key '{key}'");
                Assert.AreEqual(expectedDuplicates[key], result[key], $"Count for '{key}' should match expected value");
            }
        }

        [TestMethod]
        public void TestGroupStudentsBySameGradePerSubject()
        {
            // Act
            var result = Program.GroupStudentsBySameGradePerSubject(testStudents);

            // Assert
            Assert.AreEqual(4, result.Count, "Should have a dictionary for each of the 4 subjects");

            // Test Subject 1 (first scores)
            Dictionary<int, List<string>> subject1 = result[0];
            Assert.IsTrue(subject1.ContainsKey(68), "Subject 1 should have grade 68");
            Assert.IsTrue(subject1.ContainsKey(85), "Subject 1 should have grade 85");
            Assert.IsTrue(subject1.ContainsKey(90), "Subject 1 should have grade 90");
            Assert.IsTrue(subject1.ContainsKey(92), "Subject 1 should have grade 92");
            Assert.IsTrue(subject1.ContainsKey(95), "Subject 1 should have grade 95");

            // Test specific student placement for subject 1
            CollectionAssert.Contains(subject1[68], "David Lee", "David Lee should have grade 68 in subject 1");
            CollectionAssert.Contains(subject1[85], "Alice Smith", "Alice Smith should have grade 85 in subject 1");
            CollectionAssert.Contains(subject1[95], "James Taylor", "James Taylor should have grade 95 in subject 1");

            // Test Subject 4 (last scores)
            Dictionary<int, List<string>> subject4 = result[3];
            Assert.IsTrue(subject4.ContainsKey(65), "Subject 4 should have grade 65");
            Assert.IsTrue(subject4.ContainsKey(70), "Subject 4 should have grade 70");
            Assert.IsTrue(subject4.ContainsKey(80), "Subject 4 should have grade 80");
            Assert.IsTrue(subject4.ContainsKey(84), "Subject 4 should have grade 84");
            Assert.IsTrue(subject4.ContainsKey(87), "Subject 4 should have grade 87");
            Assert.IsTrue(subject4.ContainsKey(95), "Subject 4 should have grade 95");
            Assert.IsTrue(subject4.ContainsKey(98), "Subject 4 should have grade 98");

            // Test specific student placement for subject 4
            CollectionAssert.Contains(subject4[65], "Bob Johnson", "Bob Johnson should have grade 65 in subject 4");
            CollectionAssert.Contains(subject4[98], "Charlie Brown", "Charlie Brown should have grade 98 in subject 4");
        }

        [TestMethod]
        public void TestGetStudentsWithLowAverage_NoLowScores()
        {
            // Arrange
            var highScoringStudents = new List<Students>
            {
                new Students { StudentID = 101, FirstName = "Alice", LastName = "Smith", Scores = new List<int> { 85, 92, 78, 95 } },
                new Students { StudentID = 103, FirstName = "Charlie", LastName = "Brown", Scores = new List<int> { 82, 85, 80, 88 } }
            };

            // Act
            var result = Program.GetStudentsWithLowAverage(highScoringStudents);

            // Assert
            Assert.AreEqual(0, result.Count, "Should return an empty list when no students have average below 75");
        }

        [TestMethod]
        public void TestGetSameFirstNameCount_NoDuplicates()
        {
            // Arrange
            var uniqueNameStudents = new List<Students>
            {
                new Students { StudentID = 101, FirstName = "Alice", LastName = "Smith", Scores = new List<int> { 85, 92, 78, 95 } },
                new Students { StudentID = 102, FirstName = "Bob", LastName = "Johnson", Scores = new List<int> { 70, 68, 72, 65 } },
                new Students { StudentID = 103, FirstName = "Charlie", LastName = "Brown", Scores = new List<int> { 92, 95, 90, 98 } }
            };

            // Act
            var result = Program.GetSameFirstNameCount(uniqueNameStudents);

            // Assert
            Assert.AreEqual(0, result.Count, "Should return an empty dictionary when no duplicate names exist");
        }

        [TestMethod]
        public void TestGroupStudentsBySameGradePerSubject_EmptyList()
        {
            // Arrange
            var emptyList = new List<Students>();

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() =>
                Program.GroupStudentsBySameGradePerSubject(emptyList),
                "Should throw an exception when student list is empty");
        }
    }
}