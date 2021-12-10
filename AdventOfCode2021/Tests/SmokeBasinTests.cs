using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    internal class SmokeBasinTests
    {
        // Day 9
        [Test]
        public void TestFindingLowPoints()
        {
            // arrange - read grid from file and load into jagged array 
            int[][] input = File.ReadAllLines("Tests/SmokeBasinGridV1.txt")
                   .Select(l => l.ToCharArray().Select(i => (int)Char.GetNumericValue(i)).ToArray())
                   .ToArray();

            // act
            var result = Day09_SmokeBasin.FindLowPoints(input);

            // assert
            Assert.AreEqual(512, result);
        }

        [Test]
        public void TestSizeOfAllBasins()
        {
            // arrange - read grid from file and load into jagged array 
            int[][] input = File.ReadAllLines("Tests/SmokeBasinGridV1.txt")
                   .Select(l => l.ToCharArray().Select(i => (int)Char.GetNumericValue(i)).ToArray())
                   .ToArray();

            // act
            var result = Day09_SmokeBasin.FindSizeOfAllBasins(input);

            // assert
            Assert.AreEqual(1600104, result);
        }
    }
}
