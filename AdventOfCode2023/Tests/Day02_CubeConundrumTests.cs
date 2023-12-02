
using NUnit.Framework;

namespace AdventOfCode2023.Tests
{
    [TestFixture]
    internal class Day02_CubeConundrumTests
    {
        [Test]
        public void TestSumIdsOfPossibleGames()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day02_CubeConundrumTests.txt").ToArray();

            // act
            var result = Day02_CubeConundrum.SumIdsOfPossibleGames(input);

            // assert
            Assert.AreEqual(1867, result);
        }
    }
}
