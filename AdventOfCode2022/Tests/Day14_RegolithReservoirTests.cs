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

        [Test]
        public void TestDetermineNumberOfSandBeforeNoMoreDropsPossible()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day14_RegolithReservoirTests.txt").ToArray();

            // act
            var result = Day14_RegolithReservoir.DetermineNumberOfSandBeforeNoMoreDropsPossible(input);

            // assert
            Assert.AreEqual(25193, result);
        }
    }
}
