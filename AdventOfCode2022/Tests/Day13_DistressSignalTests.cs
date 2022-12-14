using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    internal class Day13_DistressSignalTests
    {
        [Test]
        public void TestSumIndicesWithCorrectOrder()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day13_DistressSignalTests.txt").ToArray();

            // act
            var result = Day13_DistressSignal.SumIndicesWithCorrectOrder(input);

            // assert
            Assert.AreEqual(0, result);
        }
    }
}
