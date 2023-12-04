
using NUnit.Framework;

namespace AdventOfCode2023.Tests
{
    [TestFixture]
    internal class Day03_GearRatiosTests
    {
        [Test]
        public void TestSumPartNumbers()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day03_GearRatios.txt").ToArray();

            // act
            var result = Day03_GearRatios.SumPartNumbers(input);

            // assert
            Assert.AreEqual(546312, result);
        }
    }
}
