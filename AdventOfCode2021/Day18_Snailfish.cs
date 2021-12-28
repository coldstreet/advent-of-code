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
            var previousPriorityPairs = new List<PriorityPair>();
            foreach (var line in input)
            {
                var pairsCount = line.Where(x => x == '[').Count();

                // build priority pairs list based on line
                var priorityPairs = BuildPriorityPairs(line, pairsCount);
                if (previousPriorityPairs.Count() > 0)
                {
                    Merge(previousPriorityPairs, priorityPairs);
                }

                var pairsNestedTooDeep = pairsCount - priorityPairs.OrderBy(p => p.Priority).First().Priority >= 4 ? true : false;
                var countsAbove9 = priorityPairs.Where(x => x.Left > 9 || x.Right > 9).Count();
                while (pairsNestedTooDeep || countsAbove9 > 0)
                {
                    pairsCount = Explode(priorityPairs, pairsCount);
                    pairsCount = Split(priorityPairs, pairsCount);

                    pairsNestedTooDeep = pairsCount - priorityPairs.OrderBy(p => p.Priority).First().Priority >= 4 ? true : false;
                    countsAbove9 = priorityPairs.Where(x => x.Left > 9 || x.Right > 9).Count();
                }
                
                previousPriorityPairs = priorityPairs;
            }

            var result = GetMagnitude(previousPriorityPairs);
            

            return result;
        }

        private static int Explode(List<PriorityPair> priorityPairs, int pairsCount)
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
                                    priorityPairs[j].Left += left;
                                }
                                else if (priorityPairs[j].Right != -1)
                                {
                                    priorityPairs[j].Right += left;
                                }
                                else
                                {
                                    priorityPairs[j].Left += left;
                                }

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
                                    priorityPairs[j].Right += right;
                                }
                                else if (priorityPairs[j].Left != -1)
                                {
                                    priorityPairs[j].Left += right;
                                }
                                else
                                {
                                    priorityPairs[j].Right += right;
                                }
                                
                                break;
                            }
                        }

                        // adjust priority numbers for each pair (-1 per pair)
                        for (int j = 0; j < priorityPairs.Count(); j++)
                        {
                            priorityPairs[j].Priority--;
                        }

                        // remove the "exploded" pair
                        priorityPairs.RemoveAt(i);

                        // check for more deep nested pairs to explode
                        pairsCount--;
                        Explode(priorityPairs, pairsCount);

                        break;
                    }
                }
            }

            return pairsCount;
        }

        private static int Split(List<PriorityPair> priorityPairs, int pairsCount)
        {
            for (int i = 0; i < priorityPairs.Count(); i++)
            {
                if (priorityPairs[i].Left > 9 || priorityPairs[i].Right > 9)
                {
                    var left = priorityPairs[i].Left;
                    var right = priorityPairs[i].Right;
                    var priority = priorityPairs[i].Priority;

                    var nextLeftPriority = i - 1 >= 0 ? priorityPairs[i - 1].Priority : int.MaxValue;
                    var nextRightPriority = i + 1 < priorityPairs.Count() ? priorityPairs[i + 1].Priority : int.MaxValue;

                    if (left > 9)
                    {
                        var newLeft = left/2;
                        var newRight = (left % 2 == 0) ? left/2 : left/2 + 1;

                        if (right == -1)
                        {
                            // replace existing pair in list with new pair with same priority as nextRightPriority (thus increasing the pairs by 1)
                            priorityPairs[i].Left = newLeft;
                            priorityPairs[i].Right = newRight;
                            priorityPairs[i].Priority = nextRightPriority;
                        }
                        else
                        {
                            priorityPairs[i].Left = -1;
                            var newPriorityPair = new PriorityPair(newLeft, newRight, priority - 1);
                            priorityPairs.Insert(i, newPriorityPair);
                        }
                    }
                    else if (right > 9)
                    {
                        var newLeft = right/2;
                        var newRight = (right % 2 == 0) ? right / 2 : right / 2 + 1;

                        if (left == -1)
                        {
                            // replace existing pair in list with new pair with same priority as nextLeftPriority (thus increasing the pairs by 1)
                            priorityPairs[i].Left = newLeft;
                            priorityPairs[i].Right = newRight;
                            priorityPairs[i].Priority = nextLeftPriority;
                        }
                        else
                        {
                            priorityPairs[i].Right = -1;
                            var newPriorityPair = new PriorityPair(newLeft, newRight, priority - 1);
                            priorityPairs.Insert(i + 1, newPriorityPair);
                        }
                    }

                    // adjust priority numbers for each pair (+1 per pair)
                    for (int j = 0; j < priorityPairs.Count(); j++)
                    {
                        priorityPairs[j].Priority++;
                    }

                    // check if there are other numbers
                    pairsCount++;
                    Split(priorityPairs, pairsCount);
                    break;
                }
            }

            return pairsCount;  
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

