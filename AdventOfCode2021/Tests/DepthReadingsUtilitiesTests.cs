using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    internal class DepthReadingsUtilitiesTests
    {
        // Day 1
        [Test]
        public void TestCountingDepthIncreases()
        {
            // arrange
            var input = File.ReadLines("Tests/DepthReadingsV1.txt")
                .Select(x => int.Parse(x))
                .ToArray();

            // act
            var result = Day1_DepthReadingUtilities.CountDepthIncreases(input);

            // assert
            Assert.AreEqual(1374, result);
        }

        // Day 1
        [Test]
        public void TestCountingDepthIncreasesByWindow()
        {
            // arrange
            var input = File.ReadLines("Tests/DepthReadingsV1.txt")
                .Select(x => int.Parse(x))
                .ToArray();

            // act
            var result = Day1_DepthReadingUtilities.CountDepthIncreasesByWindow(input);

            // assert
            Assert.AreEqual(1418, result);
        }
    }
}
