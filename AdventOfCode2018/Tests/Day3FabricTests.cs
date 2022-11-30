using AdventOfCode2018.Tests.Helpers;
using NUnit.Framework;

namespace AdventOfCode2018.Tests
{
    [TestFixture]
    public class Day3FabricTests
    {
        [Test]
        public void TestFabric_SimpleCase()
        {
            // arrange
            List<string> testCase = new List<string> { "#1 @ 1,3: 4x4", "#2 @ 3,1: 4x4", "#3 @ 5,5: 2x2" };

            // act
            var result = Day3Fabric.CountOverlappingSquareInches(testCase.ToArray());

            // assert
            Assert.AreEqual(4, result);
        }

        [Test]
        public void TestFabric_UsingInputFile()
        {
            // arrange
            string[] input = EmbeddedResource.ReadFile("AdventOfCode2018.Tests.Day3-v1.txt");

            // act
            var result = Day3Fabric.CountOverlappingSquareInches(input);

            // assert
            Assert.AreEqual(113716, result);
        }

        [Test]
        public void TestFabricForUniqueClaim_UsingInputFile()
        {
            // arrange
            string[] input = EmbeddedResource.ReadFile("AdventOfCode2018.Tests.Day3-v1.txt");

            // act
            var result = Day3Fabric.FindTheOneIdThatDoesNotOverlapping(input);

            // assert
            Assert.AreEqual("#742", result);
        }
    }
}
