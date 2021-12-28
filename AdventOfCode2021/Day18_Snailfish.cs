using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    public static class Day18_Snailfish
    {
        private class PriorityPair
        {
            public int Left { get; set; }
            public int Right { get; set; }
            public int Priority { get; set; }

            public PriorityPair(int left, int right, int priority)
            {
                Left = left;
                Right = right;
                Priority = priority;
            }
        }

        public static long SumAllNumbers(string[] input)
        {

            // e.g., [[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]    
            // [[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]
            var previousPriorityPairs = new List<PriorityPair>();
            foreach (var line in input)
            {
                var pairCount = line.Where(x => x == '[').Count();

                // build priority pairs list based on line
                var priorityPairs = BuildPriorityPairs(line, pairCount);
                if (previousPriorityPairs.Count() > 0)
                {
                    Merge(previousPriorityPairs, priorityPairs);
                }

                // build new line after explode and split operations and adding to next line
                Explode(priorityPairs, pairCount);

                Split();

                previousPriorityPairs = priorityPairs;
            }

            var result = GetMagnitude(previousPriorityPairs);
            

            return result;
        }

        private static void Explode(List<PriorityPair> priorityPairs, int pairsCount)
        {
            var minPriority = priorityPairs.OrderBy(p => p.Priority).First().Priority;

            if (pairsCount - minPriority >= 4)
            {
                for (int i = 0; i < priorityPairs.Count(); i++)
                {
                    // find the deepest pair to "explode"
                    if (priorityPairs[i].Priority == minPriority)
                    {
                        // explode
                        var left = priorityPairs[i].Left;
                        var right = priorityPairs[i].Right;

                        var nextLeftPriority = i - 1 >= 0 ? priorityPairs[i - 1].Priority : int.MaxValue;
                        var nextRightPriority = i + 1 < priorityPairs.Count() ? priorityPairs[i + 1].Priority : int.MaxValue;

                        // add left amount to valid pair on left  
                        // look "left" (i.e., up in list)
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (priorityPairs[j].Left != -1)
                            {
                                if (priorityPairs[j].Right == -1 && nextLeftPriority < nextRightPriority)
                                {
                                    priorityPairs[j].Right = 0;
                                }
                                
                                priorityPairs[j].Left += left;
                                break;
                            }
                        }

                        // add right amount to valid pair on right  
                        // look "right" (i.e., down in list)
                        for (int j = i + 1; j < priorityPairs.Count(); j++)
                        {
                            if (priorityPairs[j].Right != -1)
                            {
                                if (priorityPairs[j].Left == -1 && nextRightPriority < nextLeftPriority)
                                {
                                    priorityPairs[j].Left = 0;
                                }
                                
                                priorityPairs[j].Right += right;
                                break;
                            }
                        }

                        // adjust priority numbers for each pair (+1 per pair)
                        for (int j = 0; j < priorityPairs.Count(); j++)
                        {
                            priorityPairs[j].Priority--;
                        }

                        // remove the "exploded" pair
                        priorityPairs.RemoveAt(i);

                        // check for more deep nested pairs to explode
                        Explode(priorityPairs, pairsCount - 1);

                        break;
                    }
                }
            }

            return;
        }

        private static void Split()
        {
            return; // todo
        }

        private static int GetMagnitude(List<PriorityPair> priorityPairs)
        {
            return 0; // todo
        }

        private static void Merge(List<PriorityPair> previousPriorityPairs, List<PriorityPair> priorityPairs)
        {
            return; // todo
        }

        private static List<PriorityPair> BuildPriorityPairs(string line, int pairCount)
        {
            var inputChars = line.Replace(",", "").ToCharArray();

            var priorityPairs = new List<PriorityPair>();
            var nestingDepth = pairCount + 1;
            for (int i = 0; i < inputChars.Length; i++)
            {
                char previousChar = i - 1 >= 0 ? inputChars[i - 1] : '\0';
                char character = inputChars[i];
                char nextChar = i + 1 < inputChars.Length ? inputChars[i + 1] : '\0';
                if (character == '[')
                {
                    nestingDepth--;
                    //if (previousChar == ']')
                    //{
                    //    nestingDepth++;
                    //}
                    continue;
                }

                if (character == ']')
                {
                    nestingDepth++;
                    continue;
                }

                if (char.IsNumber(nextChar))
                {
                    priorityPairs.Add(new PriorityPair((int)char.GetNumericValue(character), (int)char.GetNumericValue(nextChar), nestingDepth));
                    i++;
                }
                else if (nextChar == ']')
                {
                    priorityPairs.Add(new PriorityPair(-1, (int)char.GetNumericValue(character), nestingDepth));
                }
                else
                {
                    priorityPairs.Add(new PriorityPair((int)char.GetNumericValue(character), -1, nestingDepth));
                }
            }

            return priorityPairs;
        }
    }
}

