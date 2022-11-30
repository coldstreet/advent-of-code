using AdventOfCode2017.Tests.Helpers;
using NUnit.Framework;

namespace AdventOfCode2017.Tests
{
    [TestFixture]
    public class PassphraseTests
    {
        [TestCase("abcde fghij", true)]
        [TestCase("abcde xyz ecdab", false)]
        [TestCase("a ab abc abd abf abj", true)]
        [TestCase("iiii oiii ooii oooi oooo", true)]
        [TestCase("oiii ioii iioi iiio", false)]
        public void TestValidationOfAnagramPassphrase(string passphrase, bool expectedResult)
        {
            // arrange

            // act
            var result = Passphrase.ValidateForAnagrams(passphrase);

            // assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("aa bb cc dd", true)]
        [TestCase("aa bb cc aa", false)]
        [TestCase("aa bb cc aaa", true)]
        public void TestValidationOfPassphrase(string passphrase, bool expectedResult)
        {
            // arrange

            // act
            var result = Passphrase.Validate(passphrase);

            // assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("cold", "oldc", true)]
        [TestCase("cold", "bold", false)]
        public void TestValidationOfAnagram(string s1, string s2, bool expectedResult)
        {
            // arrange

            // act
            var result = Passphrase.IsAnagram(s1, s2);

            // assert
            Assert.AreEqual(expectedResult, result);
        }


        [Test]
        public void ValidatePassphrase_UseFileWithTestCases_CountOfValidPhassphrase()
        {
            // arrange
            string[] inputs = EmbeddedResource.ReadFile("AdventOfCode2017.Tests.PassphraseInputV1.txt");
            
            // act
            int validPassphraseCount = inputs.Count(passphrase => Passphrase.Validate(passphrase));

            // assert
            Assert.AreEqual(386, validPassphraseCount);
        }

        [Test]
        public void ValidatePassphrase_UseFileWithTestCasesV2_CountOfValidPhassphrase()
        {
            // arrange
            string[] inputs = EmbeddedResource.ReadFile("AdventOfCode2017.Tests.PassphraseInputV2.txt");

            // act
            int validPassphraseCount = inputs.Count(passphrase => Passphrase.ValidateForAnagrams(passphrase));

            // assert
            Assert.AreEqual(208, validPassphraseCount);
        }
    }
}