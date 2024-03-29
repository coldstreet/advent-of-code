﻿using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    public class DumboOctopusTests
    {
        // Day 11
        [Test]
        public void TestCountFlashesAfterSteps()
        {
            // arrange - read grid from file and load into multidimensional array 
            var input = Helpers.TestUtilities.CreateRectangularArray(
                File.ReadAllLines("Tests/DumboOctopusEnergyLevelsV1.txt")
                   .Select(l => l.ToCharArray().Select(i => (int)Char.GetNumericValue(i)).ToArray())
                   .ToList());

            // act
            var result = Day11_DumboOctopus.CountFlashesAfterSteps(input, 100);

            // assert
            Assert.AreEqual(1697, result);
        }

        [Test]
        public void TestFindStepWhenAllFlashesAreSynchronized()
        {
            // arrange - read grid from file and load into multidimensional array 
            var input = Helpers.TestUtilities.CreateRectangularArray(
                File.ReadAllLines("Tests/DumboOctopusEnergyLevelsV1.txt")
                   .Select(l => l.ToCharArray().Select(i => (int)Char.GetNumericValue(i)).ToArray())
                   .ToList());

            // act
            var result = Day11_DumboOctopus.FindStepWhenAllFlashingSynchronize(input);

            // assert
            Assert.AreEqual(344, result);
        }
    }
}
