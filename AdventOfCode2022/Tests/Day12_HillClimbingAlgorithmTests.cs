using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    internal class Day12_HillClimbingAlgorithmTests
    {
        [Test]
        public void TestFindFewestStepsToHighSpotFromStart()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day12_HillClimbingAlgorithmTests.txt").ToArray();

            // act
            var result = Day12_HillClimbingAlgorithm.FindFewestStepsToHighSpotFromStart(input);

            // assert
            Assert.AreEqual(449, result); 
        }

        [Test]
        public void TestFindMinPathToHighSpot()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day12_HillClimbingAlgorithmTests.txt").ToArray();

            // act
            var result = Day12_HillClimbingAlgorithm.FindMinPathToHighSpot(input);

            // assert
            Assert.AreEqual(443, result);
        }
    }
}
