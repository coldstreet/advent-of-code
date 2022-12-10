using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    internal class Day10_CathodeRayTubeTests
    {
        [Test]
        public void TestSomeAction()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day10_CathodeRayTubeTests.txt").ToArray();

            // act
            var result = Day10_CathodeRayTube.SumSignalStrengthAtVariousCycles(input);

            // assert
            Assert.AreEqual(0, result);
        }
    }
}
