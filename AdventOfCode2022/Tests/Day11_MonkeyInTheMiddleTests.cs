using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    internal class Day11_MonkeyInTheMiddleTests
    {
        [Test]
        public void TestDetermineLevelOfMonkeyBusinessWithLowWorry()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day11_MonkeyInTheMiddleTests.txt").ToArray();

            // act
            var result = Day11_MonkeyInTheMiddle.DetermineLevelOfMonkeyBusiness(input, 20);

            // assert
            Assert.AreEqual(117624, result);
        }

        [Test]
        public void TestDetermineLevelOfMonkeyBusinessWithHighWorry()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day11_MonkeyInTheMiddleTests.txt").ToArray();

            // act
            var result = Day11_MonkeyInTheMiddle.DetermineLevelOfMonkeyBusiness(input, 10000, false);

            // assert
            Assert.AreEqual(16792940265, result);
        }
    }
}
