using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    internal class Day15_BeaconExclusionZoneTests
    {
        [Test]
        public void TestNumberOfPositionsWithoutBeacon()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day15_BeaconExclusionZoneTests.txt").ToArray();

            // act
            var result = Day15_BeaconExclusionZone.NumberOfPositionsWithoutBeacon(input, 2000000);

            // assert
            Assert.AreEqual(5240818, result);
        }
    }
}
