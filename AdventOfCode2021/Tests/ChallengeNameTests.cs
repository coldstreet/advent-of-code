using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    internal class ChallengeNameTests
    {
        // Day 99
        [Test]
        public void TestSomeAction()
        {
            // arrange - read grid from file and load into jagged array 
            var input = File.ReadLines("Tests/ChallengeNameInputV1.txt").ToArray();

            // act
            var result = Day99_ChallengeName.SomeAction(input);

            // assert
            Assert.AreEqual(42, result);
        }
    }
}
