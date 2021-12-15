using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    internal class ExtendedPolymerizationTests
    {
        // Day 13
        [Test]
        public void TestFindMostMinusLeastLetters()
        {
            // arrange - read grid from file and load into jagged array 
            var input = File.ReadLines("Tests/ExtendedPolymerizationInputV1.txt").ToArray();

            // act
            var result = Day14_ExtendedPolymerization.FindMostMinusLeastLetters(input, 10);

            // assert
            Assert.AreEqual(2321, result);
        }

        [Test]
        public void TestFindMostMinusLeastLettersWithManySteps()
        {
            // arrange - read grid from file and load into jagged array 
            var input = File.ReadLines("Tests/ExtendedPolymerizationInputV1.txt").ToArray();

            // act
            var result = Day14_ExtendedPolymerization.FindMostMinusLeastLetters(input, 40);

            // assert
            Assert.AreEqual(2399822193707, result);
        }
    }
}
