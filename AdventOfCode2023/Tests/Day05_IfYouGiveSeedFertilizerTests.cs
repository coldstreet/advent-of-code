using NUnit.Framework;

namespace AdventOfCode2023.Tests
{
    [TestFixture]
    internal class Day05_IfYouGiveSeedFertilizerTests
    {
        [Test]
        public void TestDetermineLowestLocationNumber()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day05_IfYouGiveSeedFertilizerTests.txt").ToArray();

            // act
            var result = Day05_IfYouGiveSeedFertilizer.DetermineLowestLocationNumber(input);

            // assert
            Assert.AreEqual(388071289, result);
        }

        [Test]
        public void TestDetermineLowestLocationNumberV2()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day05_IfYouGiveSeedFertilizerTests.txt").ToArray();

            // act
            var result = Day05_IfYouGiveSeedFertilizer.DetermineLowestLocationNumberV2(input);

            // assert
            Assert.AreEqual(84206669, result);
        }

        [Test]
        public void TestMergeRangesCase1()
        {
            // arrange
            var input = new List<(long, long)>
            {
                { (10, 11) },
                { (9, 12) },
                { (0, 2) },
                { (20, 22) },
                { (1, 4) },
                { (21, 24) },
                { (14, 18) }
            };

            // act
            var result = Day05_IfYouGiveSeedFertilizer.MergeRanges(input);

            // assert
            Assert.AreEqual(4, result.Count());
            Assert.IsTrue(result.Contains((9, 12)));
            Assert.IsTrue(result.Contains((0, 4)));
            Assert.IsTrue(result.Contains((20, 24)));
            Assert.IsTrue(result.Contains((14, 18)));
        }

        [Test]
        public void TestMergeRangesCase2()
        {
            // arrange
            var input = new List<(long, long)>
            {
                { (10, 11) },
                { (9, 12) },
                { (0, 2) },
                { (20, 22) },
                { (1, 4) },
                { (21, 24) },
                { (14, 18) },
                { (5, 17) }
            };

            // act
            var result = Day05_IfYouGiveSeedFertilizer.MergeRanges(input);

            // assert
            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Contains((0, 4)));
            Assert.IsTrue(result.Contains((5, 18)));
            Assert.IsTrue(result.Contains((20, 24)));
        }
    }
}
