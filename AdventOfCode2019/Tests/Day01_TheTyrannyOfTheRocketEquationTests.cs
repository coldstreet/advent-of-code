using NUnit.Framework;

namespace AdventOfCode2019.Tests
{
    [TestFixture]
    public class Day01_TheTyrannyOfTheRocketEquationTests
    {
        [Test]
        public void TestCalculateFuelForModules()
        {
            // arrange
            List<int> masses = File.ReadLines("Tests/Day01_TheTyrannyOfTheRocketEquationTests.txt").Select(int.Parse).ToList();

            // act
            var result = Day01_TheTyrannyOfTheRocketEquation.CalculateFuelForModules(masses);

            // assert
            Assert.AreEqual(3478233, result);
        }

        [Test]
        public void TestCalculateFuelForModulesAndFuelForFuel()
        {
            // arrange
            List<int> masses = File.ReadLines("Tests/Day01_TheTyrannyOfTheRocketEquationTests.txt").Select(int.Parse).ToList();

            // act
            var result = Day01_TheTyrannyOfTheRocketEquation.CalculateFuelForModulesAndFuelForFuel(masses);

            // assert
            Assert.AreEqual(5214475, result);
        }
    }
}
