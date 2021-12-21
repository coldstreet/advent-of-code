using System.Text;

namespace AdventOfCode2021
{
    public static class Day16_PacketDecoder
    {
        public static long DecodeAndSumVersionNumbers(string input)
        {
            // input is hexidecimal - we need it in binary
            var binaryPacket = string.Join(String.Empty,
              input.Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
              )
            );

            var packetInfo = ProcessPacket(binaryPacket, 0);

            return packetInfo.VersionSum;
        }

        private static PacketInfo ProcessPacket(string packet, int startingIndex)
        {
            long numberSum = 0;
            var bitIncrement = 0;
            var version = Convert.ToInt32(packet.Substring(startingIndex + bitIncrement, 3), 2);
            long versionSum = version;
            bitIncrement += 3;
            var type = Convert.ToInt32(packet.Substring(startingIndex + bitIncrement, 3), 2);
            bitIncrement += 3;

            // Types
            // 4 => literal
            // {not 4} => operator
            if (type == 4)
            {
                var pi = GetNumberFromLiteralType(packet, startingIndex + bitIncrement);
                numberSum += pi.NumberSum;
                bitIncrement += pi.BitsProcessed;
            }
            else
            {
                // Subpacket Type
                //  0 - next 15 bits are a number that represents the total length in bits of the sub-packets contained by this packet
                //  1 - next 11 bits are a number that represents the number of sub-packets immediately contained by this packet
                var subpacketType = Convert.ToInt32(packet.Substring(startingIndex + bitIncrement, 1), 2);
                bitIncrement++;
                if (subpacketType == 0)
                {
                    var lengthOfBits = Convert.ToInt32(packet.Substring(startingIndex + bitIncrement, 15), 2);
                    bitIncrement += 15;
                    var totalBitsProcessed = 0;
                    while (totalBitsProcessed + 11 <= lengthOfBits)
                    {
                        var subPacketInfo = ProcessPacket(packet, startingIndex + bitIncrement);
                        bitIncrement += subPacketInfo.BitsProcessed;
                        numberSum += subPacketInfo.NumberSum;
                        versionSum += subPacketInfo.VersionSum;
                        totalBitsProcessed += subPacketInfo.BitsProcessed;
                    }

                    // adjust for extra bits not used
                    bitIncrement += (lengthOfBits - totalBitsProcessed);

                    // bitIncrement += (packet.Count() - bitIncrement);
                }
                else
                {
                    // type 1
                    var numberOfSubpackets = Convert.ToInt32(packet.Substring(startingIndex + bitIncrement, 11), 2);
                    bitIncrement += 11;
                    while (numberOfSubpackets > 0)
                    {
                        var subPacketInfo = ProcessPacket(packet, startingIndex + bitIncrement);
                        bitIncrement += subPacketInfo.BitsProcessed;
                        numberSum += subPacketInfo.NumberSum;
                        versionSum += subPacketInfo.VersionSum;
                        numberOfSubpackets--;
                    }
                }
            }

            return new PacketInfo(numberSum, versionSum, bitIncrement);
        }

        private static PacketInfo GetNumberFromLiteralType(string packet, int startingIndex)
        {
            var bitIncrement = 0;  
            var continueBit = true;
            var binaryRepresentation = new StringBuilder();
            while (continueBit)
            {
                continueBit = packet.Substring(startingIndex + bitIncrement, 1) == "1" ? true : false;
                bitIncrement++;
                binaryRepresentation.Append(packet.Substring(startingIndex + bitIncrement, 4));
                if (!continueBit)
                {
                    bitIncrement += 4;

                    // account for proper byte padding on entire packet
                    //bitIncrement += bitIncrement / (bitIncrement * 4);  
                    
                    break;
                }

                bitIncrement += 4;
            }

            var number = Convert.ToInt64(binaryRepresentation.ToString(), 2);
            return new PacketInfo(number, 0, bitIncrement);
        }
    }

    internal readonly record struct PacketInfo
    {
        public long NumberSum { get; }

        public long VersionSum { get; }

        public int BitsProcessed { get; }

        public PacketInfo(long numberSum, long versionSum, int bitsProcessed)
        {
            NumberSum = numberSum;
            VersionSum = versionSum;
            BitsProcessed = bitsProcessed;
        }
    }
}

