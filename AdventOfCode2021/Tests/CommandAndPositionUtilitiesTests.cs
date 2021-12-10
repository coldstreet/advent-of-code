using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    internal class CommandAndPositionUtilitiesTests
    {
        // Day 2
        [Test]
        public void TestDetermineFinalPosition()
        {
            // arrange
            var input = ConvertToCommandAndPositionList(File.ReadLines("Tests/CommandsAndPositionsV1.txt").ToArray());

            // act
            var (x, y) = Day02_PositionUtilities.DetermineFinalPosition(input);

            // assert
            Assert.AreEqual(1893605, x * y);
        }

        // Day 2
        [Test]
        public void TestDetermineFinalPositionUsingAim()
        {
            // arrange
            var input = ConvertToCommandAndPositionList(File.ReadLines("Tests/CommandsAndPositionsV1.txt").ToArray());

            // act
            var (x, y) = Day02_PositionUtilities.DetermineFinalPositionUsingAim(input);

            // assert
            Assert.AreEqual(2120734350, x * y);
        }

        private (string, int)[] ConvertToCommandAndPositionList(string[] input)
        {
            List<(string, int)> results = new List<(string, int)>();
            foreach (var row in input)
            {
                var strings = row.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var position = int.Parse(strings[1]);
                results.Add((strings[0], position));
            }

            return results.ToArray();
        }
    }
}
