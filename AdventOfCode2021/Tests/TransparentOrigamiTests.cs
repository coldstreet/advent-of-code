using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    internal class TransparentOrigamiTests
    {
        // Day 13
        [Test]
        public void TestFoldUpAndCountDots()
        {
            // arrange - read grid from file and load into jagged array 
            var input = File.ReadLines("Tests/TransparentOrigamiInstructionsV1.txt").ToArray();

            // act
            var result = Day13_TransparentOrigami.FoldUpAndCountDots(input, true);

            // assert
            Assert.AreEqual(693, result);
        }

        [Test]
        public void TestFindCode()
        {
            // arrange - read grid from file and load into jagged array 
            var input = File.ReadLines("Tests/TransparentOrigamiInstructionsV1.txt").ToArray();

            // act
            var result = Day13_TransparentOrigami.FoldUpAndCountDots(input, false);

            // assert
            Assert.AreEqual(95, result);
        }
    }
}
