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
                var result = IsPacketInRightOrder((List<object>)leftSignals[i], (List<object>)rightSignals[i]);
                if (result == 1)
                {
                    validIndicesSum += i + 1;
                }
            }

            return validIndicesSum;
        }

        public static long SumIndicesOfSpecialTwoSignalsWithAllSorted(string[] input)
        {
            // parse input and add markers
            var signals = ParseInputPart2(input);
            var marker1 = "[[2]]";
            var marker2 = "[[6]]";
            signals.Add(marker1);
            signals.Add(marker2);

            var results = signals.OrderBy(_ => _, new SignalComparer()).ToList();
            var indexOfMarker1 = results.IndexOf(marker1) + 1;
            var indexofMarker2 = results.IndexOf(marker2) + 1;

            return indexOfMarker1 * indexofMarker2;
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

        private static List<string> ParseInputPart2(string[] input)
        {
            var signals = new List<string>();
            for (var i = 0; i < input.Length; i += 3)
            {
                signals.Add(input[i]);
                signals.Add(input[i+1]);
            }

            return signals;
        }

        // Result
        // 0 - no
        // 1 - yes
        // 2 - unknown
        private static int IsPacketInRightOrder(List<object> left, List<object> right)
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

                var result = IsPacketInRightOrder((List<object>)left[i], (List<object>)right[i]);
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

        public class SignalComparer : IComparer<string>
        {
            public int Compare(string? s1, string? s2)
            {
                var result = IsPacketInRightOrder(PacketParser.ParseInput(s1), PacketParser.ParseInput(s2));
                if (result == 1)
                {
                    return -1;
                }

                if (result == 2)
                {
                    return 1;
                }

                return 0;
            }
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
    


