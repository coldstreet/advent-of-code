using AdventOfCode2018.Tests.Helpers;
using NUnit.Framework;

namespace AdventOfCode2018.Tests
{
    [TestFixture]
    public class Day2ChecksumTests
    {
        [Test]
        public void TestChecksum_SimpleCase()
        {
            // arrange
            List<string> testCase = new List<string> { "abcdef", "bababc", "abbcde", "abcccd", "aabcdd", "abcdee", "ababab" };

            // act
            var result = Day2Checksum.CalculateChecksum(testCase.ToArray());

            // assert
            Assert.AreEqual(12, result);
        }

        [Test]
        public void TestChecksum_UsingInputFile()
        {
            // arrange
            string[] input = EmbeddedResource.ReadFile("AdventOfCode2018.Tests.Day2-v1.txt");

            // act
            var result = Day2Checksum.CalculateChecksum(input);

            // assert
            Assert.AreEqual(9139, result);
        }

        [Test]
        public void TestChecksumFindCommonLettersForTwoCorrectBoxIDs_UsingInputFile()
        {
            // arrange
            string[] input = EmbeddedResource.ReadFile("AdventOfCode2018.Tests.Day2-v1.txt");

            // act
            var result = Day2Checksum.FindCommonLettersForTwoCorrectBoxIDs(input);

            // assert
            Assert.AreEqual("uqcidadzwtnhsljvxyobmkfyr", result);
        }
    }
}
