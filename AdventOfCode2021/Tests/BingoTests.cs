using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    public class BingoTests
    {
        // Day 4
        [Test]
        public void TestBingoGame()
        {
            // arrange
            var input = File.ReadLines("Tests/BingoInputV1.txt").ToArray();

            // act
            var result = Day4_Bingo.PlayBingo(input);

            // assert
            Assert.AreEqual(39984, result);
        }

        // Day 4
        [Test]
        public void TestBingoGameWithOneRemainingCard()
        {
            // arrange
            var input = File.ReadLines("Tests/BingoInputV1.txt").ToArray();

            // act
            var result = Day4_Bingo.PlayBingoButGetScoreFromLastBoard(input);

            // assert
            Assert.AreEqual(8468, result);
        }
    }
}
