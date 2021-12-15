using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    public class ChitonTests
    {
        // Day 15
        [Test]
        public void TestFindMinRiskLevelPath()
        {
            // arrange - read grid from file and load into multidimensional array 
            var input = Helpers.TestUtilities.CreateRectangularArray(
                File.ReadAllLines("Tests/ChitonRiskLevelGridV1.txt")
                   .Select(l => l.ToCharArray().Select(i => (int)Char.GetNumericValue(i)).ToArray())
                   .ToList());

            // act
            var result = Day15_Chiton.FindMinRiskLevelPath(input);

            // assert
            Assert.AreEqual(40, result);
        }

    
    }
}
