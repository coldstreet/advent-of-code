using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    internal class Day12_HillClimbingAlgorithmTests
    {
        [Test]
        public void TestFindFewestStepsToHighSpot()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day12_HillClimbingAlgorithmTests.txt").ToArray();

            // act
            var result = Day12_HillClimbingAlgorithm.FindFewestStepsToHighSpot(input);

            // assert
            Assert.AreEqual(449, result); 
        }
    }
}
