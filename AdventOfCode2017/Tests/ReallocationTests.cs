using NUnit.Framework;

namespace AdventOfCode2017.Tests
{
    [TestFixture]
    public class ReallocationTests
    {
        [TestCase(new[] {0, 2, 7, 0}, 5, 4)]
        [TestCase(new[] {2, 8, 8, 5, 4, 2, 3, 1, 5, 5, 1, 2, 15, 13, 5, 14}, 3156, 1610)]
        public void TestReallocation(int[] input, int expectedCount, int expectedCyclesApart)
        {
            // arrange

            // act
            var actual = Reallocation.ReallocateAndCountToRepeat(input);

            // assert
            Assert.AreEqual(expectedCount, actual.count);
            Assert.AreEqual(expectedCyclesApart, actual.cyclesApart);
        }
    }
}