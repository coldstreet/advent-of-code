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
            var result = Day9_SmokeBasin.FindLowPoints(input);

            // assert
            Assert.AreEqual(512, result);
        }
    }
}
