using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    internal class Day09_RopeBridgeTests
    {
        [Test]
        public void TestCountTailVisitLocations()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day09_RopeBridgeTests.txt").ToArray();

            // act
            var result = Day09_RopeBridge.CountTailVisitLocations(input);

            // assert
            Assert.AreEqual(6470, result);
        }

        [Test]  
        public void TestCountTailVisitLocationsWithManyKnots()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day09_RopeBridgeTests.txt").ToArray();

            // act
            var result = Day09_RopeBridge.CountTailVisitLocations(input, true);

            // assert
            Assert.AreEqual(2658, result);  
        }
    }
}
