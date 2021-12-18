using System.Text;

namespace AdventOfCode2021
{
    public static class Day16_PacketDecoder
    {
        public static int DecodeAndSumVersionNumbers(string packet)
        {
            // Types
            // 4 => literal
            // {not 4} => operator

            var version = Convert.ToInt32(packet.Substring(0, 3), 2);
            var type = Convert.ToInt32(packet.Substring(3, 3), 2);

            var index = 6;
            var continueBit = true;
            var binaryRepresentation = new StringBuilder();
            while (continueBit)
            {
                continueBit = packet.Substring(index, 1) == "1" ? true : false;
                binaryRepresentation.Append(packet.Substring(index + 1, 4));
                if (!continueBit)
                {
                    break;
                }

                index += 5;
            }

            var number = Convert.ToInt32(binaryRepresentation.ToString(), 2);

            return 0;
        }
    }
}

