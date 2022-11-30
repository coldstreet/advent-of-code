using AdventOfCode2018.Tests.Helpers;
using NUnit.Framework;

namespace AdventOfCode2018.Tests
{
    [TestFixture]
    public class Day1FrequencyTests
    {
        [Test]
        public void TestChangeFrequencyV1_UsingInputFile()
        {
            // arrange
            string[] input = EmbeddedResource.ReadFile("AdventOfCode2018.Tests.Day1-v1.txt");

            List<int> testCase = new List<int>();
            foreach (var row in input)
            {
                testCase.Add(int.Parse(row));
            }

            // act
            var result = Day1Frequency.ChangeFrequency(0, testCase);

            // assert
            Assert.AreEqual(525, result);
        }

        [Test]
        public void TestFindFirstRepeatFrequency_SimpleCase()
        {
            // arrange
            List<int> testCase = new List<int> {1, -1};

            // act
            var result = Day1Frequency.FindFirstRepeatFrequency(0, testCase);

            // assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void TestFindFirstRepeatFrequency_UsingInputFile()
        {
            // arrange
            string[] input = EmbeddedResource.ReadFile("AdventOfCode2018.Tests.Day1-v1.txt");

            List<int> testCase = new List<int>();
            foreach (var row in input)
            {
                testCase.Add(int.Parse(row));
            }

            // act
            var result = Day1Frequency.FindFirstRepeatFrequency(0, testCase);

            // assert
            Assert.AreEqual(75749, result);
        }
    }
}
