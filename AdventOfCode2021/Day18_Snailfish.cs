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
            var previousPairsCount = 0;
            foreach (var line in input)
            {
                var pairsCount = line.Where(x => x == '[').Count();

                // build priority pairs list based on line
                var priorityPairs = BuildPriorityPairs(line, pairsCount);
                if (previousPriorityPairs.Count() > 0)
                {
                    priorityPairs = Merge(previousPriorityPairs, previousPairsCount, priorityPairs, pairsCount);
                    pairsCount += previousPairsCount + 1;
                }

                var pairsNestedTooDeep = pairsCount - priorityPairs.OrderBy(p => p.Priority).First().Priority >= 4 ? true : false;
                while (pairsNestedTooDeep)
                {
                    pairsCount = Explode(priorityPairs, pairsCount);
                    pairsNestedTooDeep = false;
                    var countsAbove9 = priorityPairs.Where(x => x.Left > 9 || x.Right > 9).Count();
                    if (countsAbove9 > 0)
                    {
                        pairsCount = Split(priorityPairs, pairsCount);
                        pairsNestedTooDeep = pairsCount - priorityPairs.OrderBy(p => p.Priority).First().Priority >= 4 ? true : false;
                    }
                }
                
                previousPriorityPairs = priorityPairs;
                previousPairsCount = pairsCount;
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
                    var left = priorityPairs[i].Left;
                    var right = priorityPairs[i].Right;

                    // find the deepest, leftmost pair to "explode"
                    if (priorityPairs[i].Priority == minPriority && left != -1 && right != -1)
                    {
                        var nextLeftIsParentPair = i - 1 >= 0 && priorityPairs[i - 1].Right == -1 && priorityPairs[i - 1].Priority - 1 == minPriority ? true : false;

                        // add left amount to valid pair on left  
                        // look "left" (i.e., up in list)
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (priorityPairs[j].Left != -1)
                            {
                                // either a hosting pair or nothing
                                if (priorityPairs[j].Right == -1 && nextLeftIsParentPair)
                                {
                                    priorityPairs[j].Left += left;
                                    priorityPairs[j].Right = 0;
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
                        var doNotRemove = false;
                        for (int j = i + 1; j < priorityPairs.Count(); j++)
                        {
                            if (priorityPairs[j].Right != -1)
                            {
                                if (!nextLeftIsParentPair && priorityPairs[j].Left != -1 && priorityPairs[j].Right != -1)
                                {
                                    priorityPairs[i].Left = 0;
                                    priorityPairs[i].Right = -1;
                                    priorityPairs[i].Priority++;
                                    priorityPairs[j].Left += right;
                                    doNotRemove = true;
                                }
                                else if (priorityPairs[j].Left == -1)
                                {
                                    priorityPairs[j].Left = 0;
                                    priorityPairs[j].Right += right;
                                }
                                else  
                                {
                                    priorityPairs[j].Left += right;
                                }
                                
                                break;
                            }
                        }

                        // adjust priority numbers for each pair (-1 per pair)
                        for (int j = 0; j < priorityPairs.Count(); j++)
                        {
                            priorityPairs[j].Priority--;
                        }

                        // Either remove the "exploded" pair or set it to zero
                        if (!doNotRemove)
                        {
                            priorityPairs.RemoveAt(i);
                        }

                        // check for more deep nested pairs to explode
                        pairsCount--;
                        pairsCount = Explode(priorityPairs, pairsCount);

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
                    pairsCount = Split(priorityPairs, pairsCount);
                    break;
                }
            }

            return pairsCount;  
        }

        private static int GetMagnitude(List<PriorityPair> priorityPairs)
        {
            return 0; // todo
        }

        private static List<PriorityPair> Merge(List<PriorityPair> priorityPairs, int priorityPairsCount, List<PriorityPair> newPriorityPairs, int newPairsCount)
        {
            for (int j = 0; j < newPriorityPairs.Count(); j++)
            {
                newPriorityPairs[j].Priority += priorityPairsCount;
            }

            for (int j = 0; j < priorityPairs.Count(); j++)
            {
                priorityPairs[j].Priority += newPairsCount;
            }

            priorityPairs.AddRange(newPriorityPairs);

            return priorityPairs;  
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

