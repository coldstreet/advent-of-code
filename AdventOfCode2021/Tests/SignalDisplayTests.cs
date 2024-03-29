﻿using NUnit.Framework;

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
            var result = Day08_SignalDisplay.CountDigits_1_4_7_8(input);

            // assert
            Assert.AreEqual(473, result);
        }

        // Day 8
        [Test]
        public void TestSumOfAllDigits()
        {
            // arrange
            var input = File.ReadLines("Tests/SignalDisplayInputV1.txt").ToArray();

            // act
            var result = Day08_SignalDisplay.SumAllDigits(input);

            // assert
            Assert.AreEqual(1097568, result);
        }
    }
}
