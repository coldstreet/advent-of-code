using NUnit.Framework;

namespace AdventOfCode2019.Tests
{
    [TestFixture]
    internal class Day99_ChallengeNameTests
    {
        [Test]
        public void TestSomeAction()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day99_ChallengeNameTests.txt").ToArray();

            // act
            var result = Day99_ChallengeName.SomeAction(input);

            // assert
            Assert.AreEqual(0, result);
        }
    }
}
