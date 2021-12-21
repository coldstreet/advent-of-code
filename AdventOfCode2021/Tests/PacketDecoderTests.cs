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
            Assert.AreEqual(906, result);
        }

        [Test]
        public void TestDecodingPacketAndSummingValues()
        {
            // arrange - read grid from file and load into jagged array 
            var input = File.ReadLines("Tests/PacketDecoderInputV1.txt").First();

            // act
            var result = Day16_PacketDecoder.DecodeAndSumValues(input);

            // assert
            Assert.AreEqual(819324480368, result);
        }
    }
}
