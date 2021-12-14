using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    internal class ExtendedPolymerizationTests
    {
        // Day 13
        [Test]
        public void TestFindProductOfLeastAndMostLetters()
        {
            // arrange - read grid from file and load into jagged array 
            var input = File.ReadLines("Tests/ExtendedPolymerizationInputV1.txt").ToArray();

            // act
            var result = Day14_ExtendedPolymerization.FindProductOfLeastAndMostLetters(input, 10);

            // assert
            Assert.AreEqual(1588, result);
        }

    }
}
