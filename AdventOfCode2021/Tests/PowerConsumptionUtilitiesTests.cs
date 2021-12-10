using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    public class PowerConsumptionUtilitiesTests
    {
        //Day 3
        [Test]
        public void TestPowerConsumptionCalc()
        {
            // arrange
            var input = File.ReadLines("Tests/PowerConsumptionBinaryNumbersV1.txt").ToArray();

            // act
            var result = Day03_PowerConsumptionUtilities.GetPowerConsumption(input);

            // assert
            Assert.AreEqual(1131506, result);

        }

        //Day 3
        [Test]
        public void TestLifeSupportCalc()
        {
            // arrange
            var input = File.ReadLines("Tests/PowerConsumptionBinaryNumbersV1.txt").ToArray();

            // act
            var result = Day03_PowerConsumptionUtilities.GetLifeSupportRating(input);

            // assert
            Assert.AreEqual(7863147, result);

        }
    }
}
