using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    internal class Day14_RegolithReservoirTests
    {
        [Test]
        public void TestDetermineNumberOfSandBeforeInfiniteDrop()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day14_RegolithReservoirTests.txt").ToArray();

            // act
            var result = Day14_RegolithReservoir.DetermineNumberOfSandBeforeInfiniteDrop(input);

            // assert
            Assert.AreEqual(625, result);
        }
    }
}
