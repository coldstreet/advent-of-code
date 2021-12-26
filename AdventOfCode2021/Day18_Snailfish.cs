using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    public static class Day18_Snailfish
    {
        public static long SumAllNumbers(string[] input)
        {

            // e.g., [[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]    
            // [[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]
            var previousPriorityQueues = new PriorityQueue<(int, int), int>[0];
            foreach (var line in input)
            {
                var pairCount = line.Where(x => x == '[').Count();

                // build priority queues for line
                var priorityQueues = BuildQueues(line, pairCount);
                if (previousPriorityQueues.Count() > 0)
                {
                    Merge(previousPriorityQueues, priorityQueues);
                }

                // build new line after explode and split operations and adding to next line
                Explode();

                StringSplitOptions();

                previousPriorityQueues = priorityQueues;
            }

            var result = GetMagnitude(previousPriorityQueues);
            

            return result;
        }

        private static void Explode()
        {
            return;
        }

        private static int GetMagnitude(PriorityQueue<(int, int), int>[] previousPriorityQueues)
        {
            return 0; // todo
        }

        private static void StringSplitOptions()
        {
            return; // todo
        }

        private static void Merge(PriorityQueue<(int, int), int>[] previousPriorityQueues, PriorityQueue<(int, int), int>[] priorityQueues)
        {
            return; // todo
        }

        private static StringBuilder RebuildLine(PriorityQueue<(int, int), int>[] priorityQueues)
        {
            StringBuilder newLine = new StringBuilder();
            for (int i = 0; i < priorityQueues.Length; i++)
            {
                var queue = priorityQueues[i];
                var pairsInQueue = queue.Count;
                StringBuilder subline = new StringBuilder();
                while (pairsInQueue > 0)
                {
                    (var left, var right) = queue.Dequeue();
                    if (left != -1 && right != -1)
                    {
                        subline.Append($"[{left},{right}]");
                    }
                    else if (right == -1)
                    {
                        subline.Insert(0, $"[{left},");
                        subline.Append(']');
                    }
                    else
                    {
                        subline.Insert(0, '[');
                        subline.Append($",{right}]");
                    }
                    pairsInQueue--;
                }

                newLine.Append(subline);
                if (i - 1 >= 0 && queue.Count == priorityQueues[i - 1].Count)
                {
                    newLine.Insert(0, '[');
                    newLine.Append(']');
                }

                if (i + 1 < priorityQueues.Length)
                {
                    newLine.Append(",");
                }

            }

            return newLine;
        }

        private static PriorityQueue<(int, int), int>[] BuildQueues(string line, int pairCount)
        {
            var queueCount = Regex.Matches(line, "\\],\\[").Count() == 0 ? 1 : Regex.Matches(line, "\\],\\[").Count() + 1;
            var inputChars = line.Replace(",", "").ToCharArray();

            var priorityQueues = new PriorityQueue<(int, int), int>[queueCount];
            var nestingDepth = pairCount + 1;
            int queueIndex = 0;
            priorityQueues[queueIndex] = new PriorityQueue<(int, int), int>();
            for (int i = 0; i < inputChars.Length; i++)
            {
                char previousChar = i - 1 >= 0 ? inputChars[i - 1] : '\0';
                char character = inputChars[i];
                char nextChar = i + 1 < inputChars.Length ? inputChars[i + 1] : '\0';
                if (character == '[')
                {
                    nestingDepth--;
                    if (previousChar == ']')
                    {
                        queueIndex++;
                        priorityQueues[queueIndex] = new PriorityQueue<(int, int), int>();
                    }
                    continue;
                }

                if (character == ']')
                {
                    nestingDepth++;
                    continue;
                }

                if (char.IsNumber(nextChar))
                {
                    priorityQueues[queueIndex].Enqueue(((int)char.GetNumericValue(character), (int)char.GetNumericValue(nextChar)), nestingDepth);
                    i++;
                }
                else if (nextChar == ']')
                {
                    priorityQueues[queueIndex].Enqueue((-1, (int)char.GetNumericValue(character)), nestingDepth);
                }
                else
                {
                    priorityQueues[queueIndex].Enqueue(((int)char.GetNumericValue(character), -1), nestingDepth);
                }
            }

            return priorityQueues;
        }
    }
}

