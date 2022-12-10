using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    internal class Day10_CathodeRayTubeTests
    {
        [Test]
        public void TestSumSignalStrengthAtVariousCycles()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day10_CathodeRayTubeTests.txt").ToArray();

            // act
            var result = Day10_CathodeRayTube.SumSignalStrengthAtVariousCycles(input);

            // assert
            Assert.AreEqual(14760, result);
        }

        // yes, this is a fake test
        [Test]
        public void TestDrawMessageOnCrt()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day10_CathodeRayTubeTests.txt").ToArray();

            // act
            var result = Day10_CathodeRayTube.DrawMessageOnCrt(input);

            // assert
            Assert.AreEqual("EFGERURE", result);
        }
    }
}
