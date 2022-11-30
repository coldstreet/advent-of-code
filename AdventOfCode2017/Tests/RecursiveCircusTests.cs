using AdventOfCode2017.Tests.Helpers;
using NUnit.Framework;

namespace AdventOfCode2017.Tests
{
    [TestFixture]
    public class RecursiveCircusTests
    {
        [Test]
        public void TestRecursiveCircus()
        {
            // arrange
            string[] input =
            {
                "pbga (66)",
                "xhth (57)",
                "ebii (61)",
                "havc (66)",
                "ktlj (57)",
                "fwft (72) -> ktlj, cntj, xhth",
                "qoyq (66)",
                "padx (45) -> pbga, havc, qoyq",
                "tknk (41) -> ugml, padx, fwft",
                "jptl (61)",
                "ugml (68) -> gyxo, ebii, jptl",
                "gyxo (61)",
                "cntj (57)"
            };

            // act
            var actual = RecursiveCircus.ConvertInput(input);

            // assert
            Assert.AreEqual("tknk", actual.Name);
        }

        [Test]
        public void TestRecursiveCircus_FindWeightBalance()
        {
            // arrange
            string[] input =
            {
                "pbga (66)",
                "xhth (57)",
                "ebii (61)",
                "havc (66)",
                "ktlj (57)",
                "fwft (72) -> ktlj, cntj, xhth",
                "qoyq (66)",
                "padx (45) -> pbga, havc, qoyq",
                "tknk (41) -> ugml, padx, fwft",
                "jptl (61)",
                "ugml (68) -> gyxo, ebii, jptl",
                "gyxo (61)",
                "cntj (57)"
            };

            // act
            var baseTowerBlock = RecursiveCircus.ConvertInput(input);
            int actual = RecursiveCircus.BalanceTower(baseTowerBlock);

            // assert
            Assert.AreEqual(60, actual);
        }

        [Test]
        public void TestRecursiveCircus_UsingInputFileV1()
        {
            // arrange
            string[] input = EmbeddedResource.ReadFile("AdventOfCode2017.Tests.RecursiveCircusInputV1.txt");
            

            // act
            var actual = RecursiveCircus.ConvertInput(input);

            // assert
            Assert.AreEqual("ykpsek", actual.Name);
        }

        [Test]
        public void TestRecursiveCircus_UsingInputFileV1_CorrectWeight()
        {
            // arrange
            string[] input = EmbeddedResource.ReadFile("AdventOfCode2017.Tests.RecursiveCircusInputV1.txt");

            // act
            var baseTowerBlock = RecursiveCircus.ConvertInput(input);
            int actual = RecursiveCircus.BalanceTower(baseTowerBlock);

            // assert
            Assert.AreEqual(1060, actual);
        }
    }
}