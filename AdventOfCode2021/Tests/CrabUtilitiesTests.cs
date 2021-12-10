using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    public class CrabUtilitiesUtilitiesTests
    {
        [Test]
        public void TestCrabUtilitiesCalc()
        {
            // arrange
            var positions = File.ReadLines("Tests/CrabPositionsV1.txt")
                .First()
                .Split(',', StringSplitOptions.TrimEntries)
                .Select(p => int.Parse(p))
                .ToArray();

            // act
            var result = Day07_CrabUtilities.FindFuelToMinimizeAlignmentPosition(positions);

            // assert
            Assert.AreEqual(348664, result);

        }

        [Test]
        public void TestCrabUtilitiesCalcWithHighFuelRate()
        {
            // arrange
            var positions = File.ReadLines("Tests/CrabPositionsV1.txt")
                .First()
                .Split(',', StringSplitOptions.TrimEntries)
                .Select(p => int.Parse(p))
                .ToArray();

            // act
            var result = Day07_CrabUtilities.FindFuelToMinimizeAlignmentPosition(positions, true);

            // assert
            Assert.AreEqual(100220525, result);

        }
    }
}
