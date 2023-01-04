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

        [Test]
        public void TestFindDistressBeacon()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day15_BeaconExclusionZoneTests.txt").ToArray();

            // act
            var result = Day15_BeaconExclusionZone.FindDistressBeacon(input, 4000000);

            // assert              
            Assert.AreEqual(13213086906101, result);  
        }

        [Test]
        public void TestReduceListByMergingRangesThatOverlap()
        {
            // arrange and act
            var result = Day15_BeaconExclusionZone.ReduceListByMergingRangesThatOverlap(new List<SignalRange>
            {
                new SignalRange(0, 10),
                new SignalRange(5, 15),
                new SignalRange(10, 20)
            });

            // assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0, result[0].StartX);
            Assert.AreEqual(20, result[0].EndX);
        }

        [Test]
        public void TestReduceListByMergingRangesThatOverlapButWithGap()
        {
            // arrange and act
            var result = Day15_BeaconExclusionZone.ReduceListByMergingRangesThatOverlap(new List<SignalRange>
            {
                new SignalRange(0, 10),
                new SignalRange(5, 15),
                new SignalRange(10, 20),
                new SignalRange(240, 250)
            });

            // assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(0, result[0].StartX);
            Assert.AreEqual(20, result[0].EndX);
            Assert.AreEqual(240, result[1].StartX);
            Assert.AreEqual(250, result[1].EndX);
        }

        [Test]
        public void TestReduceListByMergingRangesThatOverlapButWithGapV2()
        {
            // arrange and act
            var result = Day15_BeaconExclusionZone.ReduceListByMergingRangesThatOverlap(new List<SignalRange>
            {
                new SignalRange(15, 20),
                new SignalRange(11, 13)
            });

            // assert
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void TestReduceListByMergingRangesThatOverlapButWithGapV3()
        {
            // arrange and act
            var result = Day15_BeaconExclusionZone.ReduceListByMergingRangesThatOverlap(new List<SignalRange>
            {
                new SignalRange(15, 20),
                new SignalRange(22, 24),
                new SignalRange(23, 25),
                new SignalRange(11, 14),
                new SignalRange(13, 13),
                new SignalRange(11, 13),
                new SignalRange(24, 24)
            });

            // assert
            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public void TestReduceListByMergingRangesThatOverlapButOneEnclosed()
        {
            // arrange and act
            var result = Day15_BeaconExclusionZone.ReduceListByMergingRangesThatOverlap(new List<SignalRange>
            {
                new SignalRange(0, 10),
                new SignalRange(7, 9),
                new SignalRange(10, 20)
            });

            // assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0, result[0].StartX);
            Assert.AreEqual(20, result[0].EndX);
        }

        [Test]
        public void TestReduceListByMergingRangesThatOverlapButWithLowOffset()
        {
            // arrange and act
            var result = Day15_BeaconExclusionZone.ReduceListByMergingRangesThatOverlap(new List<SignalRange>
            {
                new SignalRange(5, 10),
                new SignalRange(0, 9),
                new SignalRange(10, 20)
            });

            // assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0, result[0].StartX);
            Assert.AreEqual(20, result[0].EndX);
        }

        [Test]
        public void TestReduceListByMergingRangesThatOverlapButWithHighOffset()
        {
            // arrange and act
            var result = Day15_BeaconExclusionZone.ReduceListByMergingRangesThatOverlap(new List<SignalRange>
            {
                new SignalRange(0, 10),
                new SignalRange(9, 20),
                new SignalRange(15, 25)
            });

            // assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0, result[0].StartX);
            Assert.AreEqual(25, result[0].EndX);
        }
    }
}
