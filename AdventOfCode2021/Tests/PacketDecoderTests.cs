using NUnit.Framework;

namespace AdventOfCode2021.Tests
{
    [TestFixture]
    internal class PacketDecoderTests
    {
        // Day 16
        [Test]
        public void TestDecodingPacket()
        {
            // arrange - read grid from file and load into jagged array 
            var input = File.ReadLines("Tests/PacketDecoderInputV1.txt").First();

            // act
            var result = Day16_PacketDecoder.DecodeAndSumVersionNumbers(input);

            // assert
            Assert.AreEqual(16, result);
        }
    }
}
