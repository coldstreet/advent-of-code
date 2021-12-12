using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    internal class PassagePathingTests
    {
        // Day 12
        [Test]
        public void TestCountingEasyDigits()
        {
            // arrange
            var input = File.ReadLines("Tests/PassagePathSegmentsV1.txt").ToArray();

            // act
            var result = Day12_PassagePathing.CountAllPaths(input);

            // assert
            Assert.AreEqual(10, result);
        }
    }
}
