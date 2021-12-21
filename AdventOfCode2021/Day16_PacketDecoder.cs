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

        public static long DecodeAndSumValues(string input)
        {
            // input is hexidecimal - we need it in binary
            var binaryPacket = string.Join(String.Empty,
              input.Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
              )
            );

            var packetInfo = ProcessPacket(binaryPacket, 0);

            return packetInfo.Value;
        }

        private static PacketInfo ProcessPacket(string packet, int startingIndex)
        {
            long value = 0;
            var bitIncrement = 0;
            var version = Convert.ToInt32(packet.Substring(startingIndex + bitIncrement, 3), 2);
            long versionSum = version;
            bitIncrement += 3;
            var type = Convert.ToInt32(packet.Substring(startingIndex + bitIncrement, 3), 2);
            bitIncrement += 3;

            // Types
            // 4 => literal (single number)
            // {not 4} => see method CalculateValue
            if (type == 4)
            {
                var pi = GetNumberFromLiteralType(packet, startingIndex + bitIncrement);
                value += pi.Value;
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
                    var numberResults = new List<long>();
                    while (totalBitsProcessed + 11 <= lengthOfBits)
                    {
                        var subPacketInfo = ProcessPacket(packet, startingIndex + bitIncrement);
                        bitIncrement += subPacketInfo.BitsProcessed;
                        numberResults.Add(subPacketInfo.Value);
                        versionSum += subPacketInfo.VersionSum;
                        totalBitsProcessed += subPacketInfo.BitsProcessed;
                    }

                    // adjust for extra bits not used
                    bitIncrement += (lengthOfBits - totalBitsProcessed);

                    value += CalculateValue(type, numberResults.ToArray());
                }
                else
                {
                    // subpacket type 1
                    var numberOfSubpackets = Convert.ToInt32(packet.Substring(startingIndex + bitIncrement, 11), 2);
                    bitIncrement += 11;
                    var numberResults = new List<long>();
                    while (numberOfSubpackets > 0)
                    {
                        var subPacketInfo = ProcessPacket(packet, startingIndex + bitIncrement);
                        bitIncrement += subPacketInfo.BitsProcessed;
                        numberResults.Add(subPacketInfo.Value);
                        versionSum += subPacketInfo.VersionSum;
                        numberOfSubpackets--;
                    }

                    value += CalculateValue(type, numberResults.ToArray());
                }
            }

            return new PacketInfo(value, versionSum, bitIncrement);
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

        private static long CalculateValue(int type, long[] numberResults) =>
            type switch
            {
                // Types
                // 4 => literal (single number)
                // 0 => sum packets
                // 1 => product
                // 2 => min
                // 3 => max
                // 5 => 1 if first is greater than second; otherwise 0
                // 6 => 1 if first is less than second; otherwise 0
                // 7 => 1 if first is equal than second; otherwise 0
                0 => numberResults.Sum(),
                1 => numberResults.Aggregate((long)1, (total, next) => total * next),
                2 => numberResults.Min(),
                3 => numberResults.Max(),
                5 => numberResults[0] > numberResults[1] ? 1 : 0,
                6 => numberResults[0] < numberResults[1] ? 1 : 0,
                7 => numberResults[0] == numberResults[1] ? 1 : 0,
                _ => 0
            };
    }

    internal readonly record struct PacketInfo
    {
        public long Value { get; }

        public long VersionSum { get; }

        public int BitsProcessed { get; }

        public PacketInfo(long value, long versionSum, int bitsProcessed)
        {
            Value = value;
            VersionSum = versionSum;
            BitsProcessed = bitsProcessed;
        }
    }
}

