using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    internal class Day03_RucksackReorganizationTests
    {
        [Test]
        public void TestFindSumOfSharedItems()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day03_RucksackReorganizationTests.txt").ToArray();

            // act
            var result = Day03_RucksackReorganization.FindSumOfSharedItems(input);

            // assert
            Assert.AreEqual(7553, result);
        }

        [Test]
        public void TestFindSumOfBadges()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day03_RucksackReorganizationTests.txt").ToArray();

            // act
            var result = Day03_RucksackReorganization.FindSumOfBadges(input);

            // assert
            Assert.AreEqual(2758, result);
        }
    }
}
