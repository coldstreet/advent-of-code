using NUnit.Framework;

namespace AdventOfCode2023.Tests
{
    [TestFixture]
    internal class Day05_IfYouGiveSeedFertilizerTests
    {
        [Test]
        public void TestDetermineLowestLocationNumber()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day05_IfYouGiveSeedFertilizerTests.txt").ToArray();

            // act
            var result = Day05_IfYouGiveSeedFertilizer.DetermineLowestLocationNumber(input);

            // assert
            Assert.AreEqual(388071289, result);
        }
    }
}
