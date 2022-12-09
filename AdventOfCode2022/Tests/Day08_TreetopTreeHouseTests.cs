using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    internal class Day08_TreetopTreeHouseTests
    {
        [Test]
        public void TestCountVisibleTrees()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day08_TreetopTreeHouseTests.txt").ToArray();

            // act
            var result = Day08_TreetopTreeHouse.CountVisibleTrees(input);

            // assert
            Assert.AreEqual(1688, result);
        }

        [Test]
        public void TestFindMaxScenicView()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day08_TreetopTreeHouseTests.txt").ToArray();

            // act
            var result = Day08_TreetopTreeHouse.FindMaxScenicView(input);

            // assert
            Assert.AreEqual(1688, result);
        }
    }
}
