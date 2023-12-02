
using NUnit.Framework;

namespace AdventOfCode2023.Tests
{
    [TestFixture]
    internal class Day01_TrebuchetTests
    {
        [Test]
        public void TestSumCalibrationValues()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day01_TrebuchetTests.txt").ToArray();

            // act
            var result = Day01_Trebuchet.SumCalibrationValues(input);

            // assert
            Assert.AreEqual(54573, result);
        }

        [Test]
        public void TestSumCalibrationValuesV2()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day01_TrebuchetTests.txt").ToArray();

            // act
            var result = Day01_Trebuchet.SumCalibrationValuesV2(input);

            // assert
            Assert.AreEqual(54573, result);
        }

        [Test]
        [TestCase("8nine2hbmdnvbthree", '9', 1)]
        [TestCase("fiveeight2zxjpzffvdsevenjhjvjfiveone", '5', 0)]
        [TestCase("7576threesix", '3', 4)]
        [TestCase("1bgqspl958lrj", '0', int.MaxValue)]
        public void TestFindFirstStringNumber(string input, char expectedNumber, int expectedPosition)
        {
            // arrange - see test cases

            // act
            (char number, int position) = Day01_Trebuchet.FindFirstStringNumber(input);

            // assert
            Assert.AreEqual(expectedNumber, number);
            Assert.AreEqual(expectedPosition, position);
        }

        [Test]
        [TestCase("8nine2hbmdnvbthree", '3', 13)]
        [TestCase("fiveeight2zxjpzffvdsevenjhjvjfiveone", '1', 33)]
        [TestCase("7576threesix", '6', 9)]
        [TestCase("1bgqspl958lrj", '0', int.MinValue)]
        public void TestFindLastStringNumber(string input, char expectedNumber, int expectedPosition)
        {
            // arrange - see test cases

            // act
            (char number, int position) = Day01_Trebuchet.FindLastStringNumber(input);

            // assert
            Assert.AreEqual(expectedNumber, number);
            Assert.AreEqual(expectedPosition, position);
        }
    }
}
