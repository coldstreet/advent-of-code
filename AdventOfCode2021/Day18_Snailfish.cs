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

        private static Stats _stats = new Stats();

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
                        Debug.WriteLine($"Explode [{left},{right}] with priority {minPriority}");
                        //PrintPairs(priorityPairs, pairsCount);
                        var nextLeftIsParentPair = i - 1 >= 0 && priorityPairs[i - 1].Right == -1 && priorityPairs[i - 1].Priority - 1 == minPriority ? true : false;

                        // add left amount to valid pair on left  
                        // look "left" (i.e., up in list)
                        if (i - 1 >= 0)
                        {
                            if (priorityPairs[i - 1].Left != -1)
                            {
                                // either a hosting pair or nothing
                                if (priorityPairs[i - 1].Right == -1 && nextLeftIsParentPair)
                                {
                                    _stats.AL1++;
                                    priorityPairs[i - 1].Left += left;
                                    priorityPairs[i - 1].Right = 0;
                                }
                                else if (priorityPairs[i - 1].Right != -1)
                                {
                                    _stats.AL2++;
                                    priorityPairs[i - 1].Right += left;
                                }
                                else
                                {
                                    _stats.AL3++;
                                    priorityPairs[i - 1].Left += left;
                                }
                            }
                            else // (priorityPairs[i - 1].Right != -1)
                            {
                                _stats.BL1++;
                                priorityPairs[i - 1].Right += left;
                            }
                        }


                        // add right amount to valid pair on right  
                        // look "right" (i.e., down in list)
                        var removePair = true;
                        if (i + 1 < priorityPairs.Count())
                        {
                            if (priorityPairs[i + 1].Right != -1)
                            {
                                if (!nextLeftIsParentPair && priorityPairs[i + 1].Left != -1 && priorityPairs[i + 1].Right != -1)
                                {
                                    _stats.AR1++;
                                    priorityPairs[i].Left = 0;
                                    priorityPairs[i].Right = -1;
                                    priorityPairs[i].Priority++;
                                    priorityPairs[i + 1].Left += right;
                                    removePair = false;
                                }
                                else if (priorityPairs[i + 1].Left == -1)
                                {
                                    _stats.AR2++;
                                    priorityPairs[i + 1].Left = 0;
                                    priorityPairs[i + 1].Right += right;
                                }
                                else  
                                {
                                    _stats.AR3++;
                                    priorityPairs[i + 1].Left += right;
                                }
                            }
                            else // (priorityPairs[i + 1].Left != -1)
                            {
                                _stats.BR1++;
                                priorityPairs[i + 1].Left += right;
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
                if (priorityPairs[i].Left > 9 || priorityPairs[i].Right > 9)
                {
                    var left = priorityPairs[i].Left;
                    var right = priorityPairs[i].Right;
                    var priority = priorityPairs[i].Priority;

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
                            _stats.SL1++;
        }
                        else
                        {
                            priorityPairs[i].Left = -1;
                            var newPriorityPair = new PriorityPair(newLeft, newRight, priority - 1);
                            priorityPairs.Insert(i, newPriorityPair);
                            _stats.SL2++;
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
                            _stats.SR1++;
                        }
                        else
                        {
                            priorityPairs[i].Right = -1;
                            var newPriorityPair = new PriorityPair(newLeft, newRight, priority - 1);
                            priorityPairs.Insert(i + 1, newPriorityPair);
                            _stats.SR2++;
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

    internal class Stats
    {
        internal int Down;

        public int AL1 { get; set; }
        public int AL2 { get; set; }
        public int AL3 { get; set; }
        public int BL1 { get; set; }

        public int AR1 { get; set; }
        public int AR2 { get; set; }
        public int AR3 { get; set; }
        public int BR1 { get; set; }

        public int SL1 { get; set; }
        public int SL2 { get; set; }
        public int SR1 { get; set; }
        public int SR2 { get; set; }
        public int Up { get; internal set; }
    }
}

