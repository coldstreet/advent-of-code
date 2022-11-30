using AdventOfCode2017.Tests.Helpers;
using NUnit.Framework;

namespace AdventOfCode2017.Tests
{
    [TestFixture]
    public class StreamProcessorTests
    {
        [TestCase("{}", 1)]
        [TestCase("{{{}}}", 6)]
        [TestCase("{{},{}}", 5)]
        [TestCase("{{{},{},{{}}}}", 16)]
        [TestCase("{<a>,<a>,<a>,<a>}", 1)]
        [TestCase("{{<ab>},{<ab>},{<ab>},{<ab>}}", 9)]
        [TestCase("{{<!!>},{<!!>},{<!!>},{<!!>}}", 9)]
        [TestCase("{{<a!>},{<a!>},{<a!>},{<ab>}}", 3)]
        public void Score_UsingTestCases_DeterminesCorrectScore(string stream, int expected)
        {
            // arrange

            // act
            var actual = StreamProcessor.Score(stream);

            // assert
            Assert.AreEqual(expected, actual.score);
        }

        [Test]
        public void Score_UsingStreamFromInputV1_DeterminesCorrectScore()
        {
            // arrange
            var input = EmbeddedResource.ReadFile("AdventOfCode2017.Tests.StreamProcessorInputV1.txt");

            // act
            var actual = StreamProcessor.Score(input[0]);

            // assert
            Assert.AreEqual(14421, actual.score);
        }

        [Test]
        public void DetermineRemovedGarbageCharacters_UsingStreamFromInputV1_CountsCorrectRemovedGarbageCharacters()
        {
            // arrange
            var input = EmbeddedResource.ReadFile("AdventOfCode2017.Tests.StreamProcessorInputV1.txt");

            // act
            var actual = StreamProcessor.Score(input[0]);

            // assert
            Assert.AreEqual(6817, actual.removedCharCount);
        }
    }
}
