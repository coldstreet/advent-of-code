using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    internal class TrickShotTests
    {
        // Day 17
        [Test]
        public void TestFindVelocityThatMaximizesHeight()
        {
            // arrange 
            var input = File.ReadLines("Tests/TrickShotInputV1.txt").First();

            // act
            (var result, _) = Day17_TrickShot.FindVelocityThatMaximizesHeight(input);

            // assert
            Assert.AreEqual(5253, result);
        }

        [Test]
        public void TestFindVelocityCountForAllThatHitTarget()
        {
            // arrange 
            var input = File.ReadLines("Tests/TrickShotInputV1.txt").First();

            // act
            (_, var result) = Day17_TrickShot.FindVelocityThatMaximizesHeight(input);

            // assert
            Assert.AreEqual(1770, result);
        }
    }
}