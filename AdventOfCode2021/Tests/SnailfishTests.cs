using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    internal class SnailfishTests
    {
        // Day 18  
        [Test]
        public void TestSumAllNumbers()
        {
            // arrange 
            var input = File.ReadLines("Tests/SnailfishInputV1.txt").ToArray();

            // act
            var result = Day18_Snailfish.SumAllNumbers(input);

            // assert
            Assert.AreEqual(0, result);
        }
    }
}
