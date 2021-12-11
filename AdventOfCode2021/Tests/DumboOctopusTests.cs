using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    public class DumboOctopusTests
    {
        // Day 9
        [Test]
        public void TestFindingLowPoints()
        {
            // arrange - read grid from file and load into multidimensional array 
            var input = Helpers.TestUtilities.CreateRectangularArray(
                File.ReadAllLines("Tests/DumboOctopusEnergyLevelsV1.txt")
                   .Select(l => l.ToCharArray().Select(i => (int)Char.GetNumericValue(i)).ToArray())
                   .ToList());

            // act
            var result = Day11_DumboOctopus.CountFlashesAfterIterations(input, 100);

            // assert
            Assert.AreEqual(1656, result);
        }
    }
}
