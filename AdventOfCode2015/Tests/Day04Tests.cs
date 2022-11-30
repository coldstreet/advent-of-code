using NUnit.Framework;

namespace AdventOfCode2015.Tests
{
    [TestFixture]
    public class Day04Tests
    {
        [TestCase("pqrstuv", 1048970)]
        [TestCase("abcdef", 609043)]
        public void HashKeyAndInt_StartWithSmallestIntAndIncrement_FoundFirstHashWithPrefixOf00000(string key, int expected)
        {
            // arrange

            // act
            int result = Day04.HashUntilSmallestIntIsFound(key);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void HashKeyAndInt_OfficialInput_CorrectAnswer()
        {
            // arrange

            // act
            int result = Day04.HashUntilSmallestIntIsFound("bgvyzdsv");

            // assert
            Assert.AreEqual(254575, result);
        }

        [Test]
        public void HashKeyAndInt_OfficialInputButWith6ZeroPrefix_CorrectAnswer()
        {
            // arrange

            // act
            int result = Day04.HashUntilSmallestIntIsFound("bgvyzdsv", true);

            // assert
            Assert.AreEqual(1038736, result);
        }
    }
}
