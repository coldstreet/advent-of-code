using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    internal class SyntaxScoringTests
    {
        // Day 9
        [Test]
        public void TestFindingLowPoints()
        {
            // arrange - read grid from file and load into jagged array 
            var input = File.ReadLines("Tests/SyntaxChunksV1.txt").ToArray();

            // act
            var result = Day10_SyntaxScoring.CalculateScore(input);

            // assert
            Assert.AreEqual(389589, result);
        }
    }
}
