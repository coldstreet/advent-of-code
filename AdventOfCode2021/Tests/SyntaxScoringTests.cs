using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    internal class SyntaxScoringTests
    {
        // Day 10
        [Test]
        public void TestInvalidScore()
        {
            // arrange - read grid from file and load into jagged array 
            var input = File.ReadLines("Tests/SyntaxChunksV1.txt").ToArray();

            // act
            var result = Day10_SyntaxScoring.CalculateInvalidScore(input);

            // assert
            Assert.AreEqual(389589, result);
        }

        [Test]
        public void TestIncompleteScore()
        {
            // arrange - read grid from file and load into jagged array 
            var input = File.ReadLines("Tests/SyntaxChunksV1.txt").ToArray();

            // act
            var result = Day10_SyntaxScoring.CalculateIncompleteScore(input);

            // assert
            Assert.AreEqual(389589, result);
        }
    }
}
