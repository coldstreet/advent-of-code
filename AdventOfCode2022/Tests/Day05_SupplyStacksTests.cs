using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    internal class Day05_SupplyStacksTests
    {
        [Test]
        public void TestMoveCratesOneAtATime()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day05_SupplyStacksTests.txt").ToArray();

            // act
            var result = Day05_SupplyStacks.MoveCratesOneAtATime(input);

            // assert
            Assert.AreEqual("RTGWZTHLD", result);
        }
    }
}
