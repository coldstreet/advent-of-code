using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    internal class SignalDisplayTests
    {
        // Day 8
        [Test]
        public void TestCountingEasyDigits()
        {
            // arrange
            var input = File.ReadLines("Tests/SignalDisplayInputV1.txt").ToArray();

            // act
            var result = Day8_SignalDisplay.CountDigits_2_4_5_7(input);

            // assert
            Assert.AreEqual(473, result);
        }
    }
}
