using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    internal class Day04_CampCleanupTests
    {
        [Test]
        public void TestFindFullyOverlappingPairs()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day04_CampCleanupTests.txt").ToArray();

            // act
            var result = Day04_CampCleanup.FindFullyOverlappingPairs(input);

            // assert
            Assert.AreEqual(498, result);
        }

        [Test]
        public void TestFindOverlappingPairs()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day04_CampCleanupTests.txt").ToArray();

            // act
            var result = Day04_CampCleanup.FindOverlappingPairs(input);

            // assert
            Assert.AreEqual(859, result);
        }
    }
}
