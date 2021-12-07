using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    // Day 6
    [TestFixture]
    public class LanternFishUtilitiesTests
    {
        [Test]
        public void GetCountOfFish80DaysOut()
        {
            // arrange
            var ages = File.ReadLines("Tests/LanternFishAgesV1.txt")
                .First()
                .Split(',', StringSplitOptions.TrimEntries)
                .Select(p => int.Parse(p))
                .ToArray();

            // act
            var result = Day6_LanternFishUtilities.GetNumberOfFishAfterTime(ages, 80);

            // assert
            Assert.AreEqual(372984, result);
        }

        [Test]
        public void GetCountOfFish256DaysOut()
        {
            // arrange
            var ages = File.ReadLines("Tests/LanternFishAgesV1.txt")
                .First()
                .Split(',', StringSplitOptions.TrimEntries)
                .Select(p => int.Parse(p))
                .ToArray();

            // act
            var result = Day6_LanternFishUtilities.GetNumberOfFishAfterTime(ages, 256);

            // assert
            Assert.AreEqual(1681503251694, result);
        }
    }
}
