using NUnit.Framework;

namespace AdventOfCode2017.Tests
{
    [TestFixture]
    public class SpiralMemoryTests
    {
        [TestCase(13, 4)]
        [TestCase(23, 2)]
        [TestCase(1024, 31)]
        [TestCase(277678, 475)]
        public void TestSpiralMemoryV1(int number, int expectedResult)
        {
            // arrange

            // act
            var result = SpiralMemory.CountMovesV1(number);

            // assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(10, 26)]
        [TestCase(11, 54)]
        [TestCase(12, 57)]
        [TestCase(13, 59)]
        [TestCase(14, 122)]
        [TestCase(15, 133)]
        [TestCase(23, 806)]
        [TestCase(24, 880)]
        [TestCase(25, 931)]
        [TestCase(26, 957)]
        public void TestSumBasedSpiralMemory(int maxSquareCount, int expectedResult)
        {
            // arrange

            // act
            var lookup = SpiralMemory.BuildSumBasedSpiralLookup(maxSquareCount);

            // assert
            Assert.AreEqual(expectedResult, lookup[maxSquareCount]);
        }

        [Test]
        public void TestSumBasedSpiralMemory_FindNextLargestValue()
        {
            // arrange
            var lookup = SpiralMemory.BuildSumBasedSpiralLookup(100);
            var values = lookup.Values;

            // act
            int answer = 0;
            foreach (int value in values)
            {
                if (value > 277678)
                {
                    answer = value;
                    break;
                }
            }

            // assert
            Assert.AreEqual(279138, answer);
        }
    }
}