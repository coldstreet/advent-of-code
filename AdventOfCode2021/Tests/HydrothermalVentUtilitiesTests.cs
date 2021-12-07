using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    public class HydrothermalVentUtilitiesTests
    {
        //Day 5
        [Test]
        public void TestGettingGridCountToAvoid()
        {
            // arrange
            var input = File.ReadLines("Tests/HydrothermalVentLinesV1.txt").ToArray();

            // act
            var result = Day5_HydrothermalVentUtilities.GetCountOfGridPointsToAvoid(input);

            // assert
            Assert.AreEqual(5145, result);

        }

        //Day 5
        [Test]
        public void TestGettingGridCountToAvoidWithDiagnols()
        {
            // arrange
            var input = File.ReadLines("Tests/HydrothermalVentLinesV1.txt").ToArray();

            // act
            var result = Day5_HydrothermalVentUtilities.GetCountOfGridPointsToAvoidWithDiagnols(input);

            // assert
            Assert.AreEqual(16518, result);

        }
    }
}
