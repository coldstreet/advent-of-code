using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    internal class Day13_DistressSignalTests
    {
        [Test]
        public void TestSumIndicesWithCorrectOrder()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day13_DistressSignalTests.txt").ToArray();

            // act
            var result = Day13_DistressSignal.SumIndicesWithCorrectOrder(input);

            // assert
            Assert.AreEqual(5905, result);
        }

        [Test]
        public void TestSumIndicesOfSpecialTwoSignalsWithAllSorted()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day13_DistressSignalTests.txt").ToArray();

            // act
            var result = Day13_DistressSignal.SumIndicesOfSpecialTwoSignalsWithAllSorted(input);

            // assert
            Assert.AreEqual(21691, result);
        }


        [TestCase("[1,1,3,1,1]")]
        [TestCase("[[1],[2,3,4]]")]
        [TestCase("[[1],4]")]
        [TestCase("[1,[4]]")]
        [TestCase("[9]")]
        [TestCase("[[8,7,6]]")]
        [TestCase("[[4,4],4,4]")]
        [TestCase("[[4,4],4,4,4]")]
        [TestCase("[7,7,7,7]")]
        [TestCase("[]")]
        [TestCase("[[[]]]")]
        [TestCase("[[]]")]
        [TestCase("[1,[2,[3,[4,[5,6,0]]]],8,9]")]
        public void TestParser(string input)
        {
            // act
            var result = PacketParser.ParseInput(input);

            // assert
            Assert.AreEqual(input, ListToString(result));
        }

        internal static string ListToString(List<object> objects)
        {
            List<string> asStrings = new();
            foreach (object el in objects)
            {
                string s = el switch
                {
                    (List<object> list) => ListToString(list),
                    _ => $"{el}",
                };
                asStrings.Add(s);
            }
            return "[" + string.Join(",", asStrings) + "]";
        }
    }
}
