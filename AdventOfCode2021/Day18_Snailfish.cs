using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    public static class Day18_Snailfish
    {
        public static long SumAllNumbers(string[] input)
        {
            var line = input[0];
            var countPairs = line.Where(x => x == '[').Count();
            var queueCount = line.Split(',').Count(x => x.Contains("]["));

            // e.g., [[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]
            // [[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]

            // build priority queues for line
            var priorityQueues = new PriorityQueue<(int, int), int>(queueCount);
            var nestingDepth = countPairs;
            char[] charArray = line.ToCharArray();
            char previousChar = '\0';
            for (int i = 0; i < charArray.Length; i++)
            {
                char character = charArray[i];
                if (character == '[')
                {
                    nestingDepth--;
                    previousChar = character;
                    continue;
                }
                
                if (character == ']')
                {
                    nestingDepth++;
                    previousChar = character;
                    continue;
                }

                if (character == ',')
                {
                    previousChar = character;
                    continue;
                }

               


            }

            return 0;
        }

        private static void Explode()
        {

        }
    }
}

