using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    internal class Day01_CalorieCountingTests
    {
        [Test]
        public void TestFindMaxCalorieForAnElf()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day01_CalorieCountingTests.txt").ToArray();

            // act
            var result = Day01_CalorieCounting.FindMaxCalorieForAnElf(input);

            // assert
            Assert.AreEqual(68923, result);
        }

        [Test]
        public void FindMaxCalorieForTopThreeElves()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day01_CalorieCountingTests.txt").ToArray();

            // act
            var result = Day01_CalorieCounting.FindMaxCalorieForTopThreeElves(input);

            // assert
            Assert.AreEqual(200044, result);
        }
    }
}
