﻿using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    internal class TrickShotTests
    {
        // Day 99
        [Test]
        public void TestSomeAction()
        {
            // arrange 
            var input = File.ReadLines("Tests/TrickShotInputV1.txt").ToArray();

            // act
            var result = Day17_TrickShot.SomeAction(input);

            // assert
            Assert.AreEqual(0, result);
        }
    }
}
