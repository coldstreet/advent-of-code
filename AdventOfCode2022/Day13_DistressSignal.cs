using System.Text.Json;

namespace AdventOfCode2022
{
    public static class Day13_DistressSignal
    {
        public static long SumIndicesWithCorrectOrder(string[] input)
        {
            // parse input
            var (leftSignals, rightSignals) = ParseInput(input);

            var validIndicesSum = 0;
            for (var i = 0; i < leftSignals.Count; i++)  // iterate through the number of signal pairs given in input
            {
                if (!IsValidPacket((List<object>) leftSignals[i], (List<object>) rightSignals[i]))
                {
                    continue;
                }

                validIndicesSum += i + 1;
            }

            return validIndicesSum;
        }

        private static (List<object>, List<object>) ParseInput(string[] input)
        {
            var leftSignals = new List<object>();
            var rightSignals = new List<object>();
            for (var i = 0; i < input.Length; i += 3)
            {
                leftSignals.Add(PacketParser.ParseInput(input[i]));
                rightSignals.Add(PacketParser.ParseInput(input[i + 1]));
            }

            return (leftSignals, rightSignals);
        }

        private static bool IsValidPacket(List<object> left, List<object> right, bool checkArraySize = true)
        {
            if (checkArraySize && left.Count > right.Count)
            {
                return false;
            }

            var length = Math.Min(left.Count, right.Count);
            for (var i = 0; i < length; i++)
            {
                if (left[i] is int l && right[i] is int r)
                {
                    if (!IsValidInts(l, r))
                    {
                        return false;
                    }

                    continue;
                }

                if (left[i] is int lInt)
                {
                    left[i] = new List<object> { lInt };
                    return IsValidPacket((List<object>) left[i], (List<object>) right[i], false);
                }

                if (right[i] is int rInt)
                {
                    right[i] = new List<object> { rInt };
                    return IsValidPacket((List<object>)left[i], (List<object>)right[i], false);
                }

                if (!IsValidPacket((List<object>)left[i], (List<object>)right[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsValidInts(int left, int right)
        {
            return left <= right;
        }
    }

    public static class PacketParser
    {
        public static List<object> ParseInput(string input)
        {
            var queue = new Queue<char>();
            foreach (char x in input)
            {
                queue.Enqueue(x);
            }

            return ParseQueue(queue);
        }

        public static List<object> ParseQueue(Queue<char> data)
        {
            var elements = new List<object>();
            data.Dequeue(); // Remove the '[' from the queue
            while (data.Peek() != ']')
            {
                if (data.Peek() == ',')
                {
                    data.Dequeue();
                }
                object element = ParseElement(data);
                elements.Add(element);
            }
            data.Dequeue(); // Remove the ']' from the queue

            return elements;
        }

        public static object ParseElement(Queue<char> data)
        {
            char next = data.Peek();
            if (char.IsDigit(next))
            {
                return ParseInt(data);
            }

            if (next == '[')
            {
                return ParseQueue(data);
            }

            throw new Exception($"Expected an int or array/list but found: {string.Join("", data)}");
        }

        public static int ParseInt(Queue<char> data)
        {
            string token = string.Empty;
            while (char.IsDigit(data.Peek()))
            {
                token += data.Dequeue();
            }
            return int.Parse(token);
        }
    }
}
    


