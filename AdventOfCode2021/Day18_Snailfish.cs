using System.Diagnostics;

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

                    Debug.WriteLine("After merge");
                    PrintPairs(priorityPairs, pairsCount);
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

            var result = GetMagnitude(previousPriorityPairs, previousPairsCount);
            

            return result;
        }

        public static long GetLargestMagnitudeFromTwo(string[] input)
        {
            var largestMagnitude = int.MinValue;

            foreach (var line in input)
            {
                foreach (var otherLine in input)
                {
                    if (line == otherLine)
                    {
                        continue;
                    }

                    var pairsCount = line.Where(x => x == '[').Count();
                    var priorityPairs = BuildPriorityPairs(line, pairsCount);
                    var otherPairsCount = line.Where(x => x == '[').Count();
                    var otherPriorityPairs = BuildPriorityPairs(otherLine, otherPairsCount);

                    priorityPairs = Merge(otherPriorityPairs, otherPairsCount, priorityPairs, pairsCount);
                    pairsCount += otherPairsCount + 1;

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

                    var magnitude = GetMagnitude(priorityPairs, pairsCount);
                    largestMagnitude = magnitude > largestMagnitude ? magnitude : largestMagnitude;
                }
            }

            return largestMagnitude;
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
                    var priority = priorityPairs[i].Priority;
                    var removePair = true;
                    var leftIsHostingPair = false;

                    // find the deepest, leftmost pair to "explode"
                    if (priority == minPriority && left != -1 && right != -1)
                    {
                        Debug.WriteLine($"Explode [{left},{right}] with priority {minPriority}");
                        //PrintPairs(priorityPairs, pairsCount);

                        // add left amount to valid pair on left  
                        // look "left" (i.e., up in list)
                        if (i - 1 >= 0)
                        {
                            if (priorityPairs[i - 1].Left == -1)
                            {
                                priorityPairs[i - 1].Right += left;
                            }
                            else // (priorityPairs[i - 1].Right != -1)
                            {
                                // either a hosting pair or nothing
                                if (priorityPairs[i - 1].Right == -1 && priorityPairs[i - 1].Priority - 1 == minPriority) // this is a "hosting" pair
                                {
                                    priorityPairs[i - 1].Left += left;
                                    priorityPairs[i - 1].Right = 0;
                                    leftIsHostingPair = true;
                                }
                                else if (priorityPairs[i - 1].Right != -1)
                                {
                                    priorityPairs[i - 1].Right += left;
                                }
                                else
                                {
                                    priorityPairs[i - 1].Left += left;
                                }
                            }
                        }


                        // add right amount to valid pair on right  
                        // look "right" (i.e., down in list)
                        if (i + 1 < priorityPairs.Count())
                        {
                            if (priorityPairs[i + 1].Right == -1)
                            {
                                priorityPairs[i + 1].Left += right;
                            }
                            else // (priorityPairs[i + 1].Left != -1)
                            {
                                if (!leftIsHostingPair && priorityPairs[i + 1].Left != -1 && priorityPairs[i + 1].Right != -1 && priorityPairs[i + 1].Priority == priority)
                                {
                                    priorityPairs[i].Left = 0;
                                    priorityPairs[i].Right = -1;
                                    priorityPairs[i].Priority++;
                                    priorityPairs[i + 1].Left += right;
                                    removePair = false;
                                }
                                else if (priorityPairs[i + 1].Left == -1 && priorityPairs[i + 1].Priority - 1 == minPriority)
                                {
                                    priorityPairs[i + 1].Right += right;
                                    priorityPairs[i + 1].Left = 0;
                                }
                                else if (priorityPairs[i + 1].Left == -1)
                                {
                                    priorityPairs[i + 1].Right += right;
                                }
                                else
                                {
                                    priorityPairs[i + 1].Left += right;
                                }
                            }
                        }

                        // adjust priority numbers for each pair (-1 per pair)
                        for (int j = 0; j < priorityPairs.Count(); j++)
                        {
                            priorityPairs[j].Priority--;
                        }

                        // Either remove the "exploded" pair or set it to zero
                        if (removePair)
                        {
                            priorityPairs.RemoveAt(i);
                        }
                        
                        // check for more deep nested pairs to explode
                        pairsCount--;
                        PrintPairs(priorityPairs, pairsCount);
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
                var left = priorityPairs[i].Left;
                var right = priorityPairs[i].Right;
                var priority = priorityPairs[i].Priority;

                if (left <= 9 && right <= 9)
                {
                    continue;
                }

                if (left > 9)
                {
                    Debug.WriteLine($"Split left [{left},{right}]");

                    var newLeft = left/2;
                    var newRight = (left % 2 == 0) ? left/2 : left/2 + 1;

                    if (right == -1)
                    {
                        // replace existing pair in list with new pair (thus increasing the pairs by 1)
                        priorityPairs[i].Left = newLeft;
                        priorityPairs[i].Right = newRight;
                        priorityPairs[i].Priority = priority - 1;
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
                    Debug.WriteLine($"Split right [{left},{right}]");

                    var newLeft = right/2;
                    var newRight = (right % 2 == 0) ? right / 2 : right / 2 + 1;

                    if (left == -1)
                    {
                        // replace existing pair in list with new pair (thus increasing the pairs by 1)
                        priorityPairs[i].Left = newLeft;
                        priorityPairs[i].Right = newRight;
                        priorityPairs[i].Priority = priority - 1;
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
                PrintPairs(priorityPairs, pairsCount);
                pairsCount = Explode(priorityPairs, pairsCount);
                pairsCount = Split(priorityPairs, pairsCount);
                break;
                
            }

            return pairsCount;  
        }

        private static int GetMagnitude(List<PriorityPair> priorityPairs, int pairsCount)
        {
            var magnitude = 0;
            var minPriority = priorityPairs.OrderBy(p => p.Priority).First().Priority;
            for (int i = 0; i < priorityPairs.Count(); i++)
            {
                var left = priorityPairs[i].Left;
                var right = priorityPairs[i].Right;
                var priority = priorityPairs[i].Priority;
                var leftIsHostingPair = false;
                var removePair = true;

                // find the deepest pair to calculate
                if (priority == minPriority && left != -1 && right != -1)
                {
                    Debug.WriteLine($"Calcluate magnitude for [{left},{right}] with priority {minPriority}");
                    magnitude = 3 * left + 2 * right;

                    // look "left" (i.e., up in list)
                    if (i - 1 >= 0 && priorityPairs[i - 1].Right == -1 && priorityPairs[i - 1].Priority - 1 == minPriority) // this is a "hosting" pair
                    {
                        priorityPairs[i - 1].Right = magnitude;
                        leftIsHostingPair = true;
                    }
                        
                    // look "right" (i.e., down in list)
                    if (i + 1 < priorityPairs.Count())
                    {
                        if (!leftIsHostingPair && priorityPairs[i + 1].Left != -1 && priorityPairs[i + 1].Right != -1 && priorityPairs[i + 1].Priority == priority)
                        {
                            priorityPairs[i].Left = magnitude;
                            priorityPairs[i].Right = -1;
                            priorityPairs[i].Priority++;
                            removePair = false;
                        }
                        else if (priorityPairs[i + 1].Left == -1 && priorityPairs[i + 1].Priority - 1 == minPriority)
                        {
                            priorityPairs[i + 1].Left = magnitude;
                        }
                    }
                    
                    // adjust priority numbers for each pair (-1 per pair)
                    for (int j = 0; j < priorityPairs.Count(); j++)
                    {
                        priorityPairs[j].Priority--;
                    }

                    // Either remove the "exploded" pair or set it to zero
                    if (removePair)
                    {
                        priorityPairs.RemoveAt(i);
                    }

                    if (priorityPairs.Count == 0)
                    {
                        Debug.WriteLine($"Magnitude is {magnitude}");
                        return magnitude;
                    }

                    // check for more deep nested pairs to explode
                    pairsCount--;
                    magnitude = GetMagnitude(priorityPairs, pairsCount);

                    break;

                }
            }
       
            return magnitude;   
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

        private static List<PriorityPair> BuildPriorityPairs(string line, int pairsCount)
        {
            var inputChars = line.Replace(",", "").ToCharArray();

            var priorityPairs = new List<PriorityPair>();
            var nestingDepth = pairsCount + 1;
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

            Debug.WriteLine("Built initial list");
            PrintPairs(priorityPairs, pairsCount);

            return priorityPairs;
        }

        private static void PrintPairs(List<PriorityPair> priorityPairs, int pairsCount)
        {
            Debug.WriteLine($"Pairs count = {pairsCount}  ------------------------------");
            for (int i = 0; i < priorityPairs.Count(); i++)
            {
                var left = priorityPairs[i].Left;
                var right = priorityPairs[i].Right;
                var priority = priorityPairs[i].Priority;

                Debug.WriteLine($"[{left},{right}] {priority}");
            }

            Debug.WriteLine("");
        }
    }
}

