using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    internal class Day08_TreetopTreeHouseTests
    {
        [Test]
        public void TestSomeAction()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day08_TreetopTreeHouseTests.txt").ToArray();

            // act
            var result = Day08_TreetopTreeHouse.CountVisibleTrees(input);

            // assert
            Assert.AreEqual(1688, result);
        }
    }
}
