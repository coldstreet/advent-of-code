using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    internal class BeaconScannerTests
    {
        // Day 19
        [Test]
        [Ignore("Day 19 is not done")]
        public void TestGettingBeaconCount()
        {
            // arrange 
            var input = File.ReadLines("Tests/BeaconScannerInputV1.txt").ToArray();

            // act
            var result = Day19_BeaconScanner.DetermineBeaconCount(input);

            // assert
            Assert.AreEqual(79, result);
        }
    }
}
