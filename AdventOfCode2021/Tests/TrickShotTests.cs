using NUnit.Framework;

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
            var input = File.ReadLines("Tests/TrickShotInputV1.txt").First();

            // act
            var result = Day17_TrickShot.FindVelocityThatMaximizesHeight(input);

            // assert
            Assert.AreEqual(5253, result);
        }
    }
}