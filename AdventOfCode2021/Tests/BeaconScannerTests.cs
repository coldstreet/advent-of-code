using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    internal class BeaconScannerTests
    {
        // Day 99 - This is a template class
        [Test]
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
