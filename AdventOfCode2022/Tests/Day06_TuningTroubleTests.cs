using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    internal class Day06_TuningTroubleTests
    {
        [Test]
        public void TestFindCharCountAtEndOfFirstMarkerV1()
        {
            // arrange 
            var input = File.ReadAllText("Tests/Day06_TuningTroubleTests.txt");

            // act
            var result = Day06_TuningTrouble.FindCharCountAtEndOfFirstMarker(input, 4);

            // assert
            Assert.AreEqual(1920, result);
        }

        [Test]
        public void TestFindCharCountAtEndOfFirstMarkerV2()
        {
            // arrange 
            var input = File.ReadAllText("Tests/Day06_TuningTroubleTests.txt");

            // act
            var result = Day06_TuningTrouble.FindCharCountAtEndOfFirstMarker(input, 14);

            // assert
            Assert.AreEqual(2334, result);
        }
    }
}
