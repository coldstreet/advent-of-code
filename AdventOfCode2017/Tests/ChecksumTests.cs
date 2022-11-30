using AdventOfCode2017.Tests.Helpers;
using NUnit.Framework;

namespace AdventOfCode2017.Tests
{
    [TestFixture]
    public class ChecksumTests
    {
        [Test]
        public void TestCalcChecksumV1()
        {
            // arrange
            List<int[]> testCase =
                new List<int[]>
                {
                    new[] {5, 1, 9, 5},
                    new[] {7, 5, 3},
                    new[] {2, 4, 6, 8}
                };

            // act
            var result = Checksum.CalcChecksumV1(testCase);

            // assert
            Assert.AreEqual(18, result);
        }

        [Test]
        public void TestCalcChecksumV2()
        {
            // arrange
            List<int[]> testCase =
                new List<int[]>
                {
                    new[] {5, 9, 2, 8},
                    new[] {9, 4, 7, 3},
                    new[] {3, 8, 6, 5}
                };

            // act
            var result = Checksum.CalcChecksumV2(testCase);

            // assert
            Assert.AreEqual(9, result);
        }

        [Test]
        public void TestChecksumV1_UsingInputFile()
        {
            // arrange
            string[] input = EmbeddedResource.ReadFile("AdventOfCode2017.Tests.ChecksumInputV1.txt");

            List<int[]> testCase = new List<int[]>();
            foreach(var row in input)
            {
                var strings = row.Split(null as string[], StringSplitOptions.RemoveEmptyEntries);
                testCase.Add(strings.Select(arg => int.Parse(arg)).ToArray());
            }
            
            // act
            var result = Checksum.CalcChecksumV1(testCase);

            // assert
            Assert.AreEqual(48357, result);
        }

        [Test]
        public void TestChecksumV2_UsingInputFile()
        {
            // arrange
            string[] input = EmbeddedResource.ReadFile("AdventOfCode2017.Tests.ChecksumInputV2.txt");

            List<int[]> testCase = new List<int[]>();
            foreach (var row in input)
            {
                var strings = row.Split(null as string[], StringSplitOptions.RemoveEmptyEntries);
                testCase.Add(strings.Select(arg => int.Parse(arg)).ToArray());
            }

            // act
            var result = Checksum.CalcChecksumV2(testCase);

            // assert
            Assert.AreEqual(351, result);
        }
    }
}