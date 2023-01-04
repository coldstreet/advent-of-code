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

        [Test, Ignore("work in progress")]
        public void TestFindDistressBeacon()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day15_BeaconExclusionZoneTests.txt").ToArray();

            // act
            var result = Day15_BeaconExclusionZone.FindDistressBeacon(input, 4000000);

            // assert
            Assert.AreEqual(1767503605, result); // too low :( 
        }

        [Test]
        public void TestReduceListByMergingRangesThatOverlap()
        {
            // arrange and act
            var result = Day15_BeaconExclusionZone.ReduceListByMergingRangesThatOverlap(new List<SignalRange>
            {
                new SignalRange(0, 10),
                new SignalRange(5, 15),
                new SignalRange(10, 20),
                new SignalRange(15, 25),
                new SignalRange(20, 30),
                new SignalRange(25, 35),
                new SignalRange(30, 40),
                new SignalRange(35, 45),
                new SignalRange(40, 50),
                new SignalRange(45, 55),
                new SignalRange(50, 60),
                new SignalRange(55, 65),
                new SignalRange(60, 70),
                new SignalRange(65, 75),
                new SignalRange(70, 80),
                new SignalRange(75, 85),
                new SignalRange(80, 90),
                new SignalRange(85, 95),
                new SignalRange(90, 100),
                new SignalRange(95, 105),
                new SignalRange(100, 110),
                new SignalRange(105, 115),
                new SignalRange(110, 120),
                new SignalRange(115, 125),
                new SignalRange(120, 130),
                new SignalRange(125, 135),
                new SignalRange(130, 140),
                new SignalRange(135, 145),
                new SignalRange(140, 150),
                new SignalRange(145, 155),
                new SignalRange(150, 160),
                new SignalRange(155, 165),
                new SignalRange(160, 170),
                new SignalRange(165, 175),
                new SignalRange(170, 180),
                new SignalRange(175, 185),
                new SignalRange(180, 190),
                new SignalRange(185, 195),
                new SignalRange(190, 200),
                new SignalRange(195, 205),
                new SignalRange(200, 210),
                new SignalRange(205, 215),
                new SignalRange(210, 220),
                new SignalRange(215, 225),
                new SignalRange(220, 230),
                new SignalRange(225, 235),
                new SignalRange(230, 240),
                new SignalRange(235, 245),
                new SignalRange(240, 250)
            });

            // assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0, result[0].StartX);
            Assert.AreEqual(250, result[0].EndX);
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
