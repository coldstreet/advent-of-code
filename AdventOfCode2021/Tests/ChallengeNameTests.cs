using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    internal class ChallengeNameTests
    {
        // Day 99 - This is a template class
        [Test]
        public void TestSomeAction()
        {
            // arrange 
            var input = File.ReadLines("Tests/ChallengeNameInputV1.txt").ToArray();

            // act
            var result = Day99_ChallengeName.SomeAction(input);

            // assert
            Assert.AreEqual(0, result);
        }
    }
}
