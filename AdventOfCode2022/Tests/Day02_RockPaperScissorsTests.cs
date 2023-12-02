using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    internal class Day02_RockPaperScissorsTests
    {
        [Test]
        public void TestDetermineP2ScoreVersion1()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day02_RockPaperScissors.txt").ToArray();

            // act
            var result = Day02_RockPaperScissors.DetermineP2ScoreVersion1(input);

            // assert
            Assert.AreEqual(11841, result);
        }

        [Test]
        public void TestDetermineP2ScoreVersion2()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day02_RockPaperScissors.txt").ToArray();

            // act
            var result = Day02_RockPaperScissors.DetermineP2ScoreVersion2(input);

            // assert
            Assert.AreEqual(13022, result);
        }
    }
}
