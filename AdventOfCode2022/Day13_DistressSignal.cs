using System.Reflection.Metadata.Ecma335;

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
                var result = IsValidPacket((List<object>)leftSignals[i], (List<object>)rightSignals[i]);
                if (result == 1)
                {
                    validIndicesSum += i + 1;
                }
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

        // Result
        // 0 - failed
        // 1 - validated
        // 2 - unknown
        private static int IsValidPacket(List<object> left, List<object> right)
        {
            if (left.Count == 0 && right.Count > 0)
            {
                return 1;
            }

            for (var i = 0; i < left.Count; i++)
            {
                if (i == right.Count)
                {
                    return 0;
                }

                if (left[i] is int l && right[i] is int r)
                {
                    if (l < r)
                    {
                        return 1;
                    }

                    if (l == r)
                    {
                        continue;
                    }

                    return 0;
                }

                if (left[i] is int lInt)
                {
                    left[i] = new List<object> { lInt };
                }

                if (right[i] is int rInt)
                {
                    right[i] = new List<object> { rInt };
                }

                var result = IsValidPacket((List<object>)left[i], (List<object>)right[i]);
                if (result == 0 || result == 1)
                {
                    return result;
                }

                // keep investigating
            }

            if (left.Count < right.Count)
            {
                return 1;
            }

            return 2;
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
    


