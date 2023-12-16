﻿
using NUnit.Framework;

namespace AdventOfCode2023.Tests
{
    [TestFixture]
    internal class Day04_ScratchcardsTests
    {
        [Test]
        public void TestAddScoresFromCards()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day04_ScratchcardsTests.txt").ToArray();

            // act
            var result = Day04_Scratchcards.AddScoresFromCards(input);

            // assert
            Assert.AreEqual(0, result);
        }
    }
}