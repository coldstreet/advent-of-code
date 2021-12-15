namespace AdventOfCode2021
{
    public static class Day14_ExtendedPolymerization
    {
        public static long FindMostMinusLeastLetters(string[] input, int steps)
        {
            var code = input[0];

            var insertInstructions = input
                .TakeLast(input.Length - 2)
                .Select(l => l.Split(" -> "))
                .ToDictionary(k => k[0], v => v[1]);

            // build pairs count dictionary
            var pairs = new Dictionary<string, long>();
            for (int i = 0; i < code.Length - 1; i++)
            {
                var pair = code.Substring(i, 2);
                if (!pairs.ContainsKey(pair))
                {
                    pairs.Add(pair, 1);
                    continue;
                }

                pairs[pair]++;
            }

            // build alphabit count dictionary
            var alphabitCounts = new Dictionary<char, long>(26);
            foreach (var c in code)
            {
                if (!alphabitCounts.ContainsKey(c))
                {
                    alphabitCounts.Add(c, 1);
                    continue;
                }

                alphabitCounts[c]++;
            }

            while (steps > 0)
            {
                var newPairs = new Dictionary<string, long>();
                var pairsToProcess = pairs.Keys;

                foreach(var pair in pairsToProcess)
                {
                    var increment = pairs.Keys.Contains(pair) ? pairs[pair] : 1;
                    var insertAlpha = insertInstructions[pair];
                    var possibleNewPairA = pair[0] + insertAlpha;
                    var possibleNewPairB = insertAlpha + pair[1];
                    if (!newPairs.ContainsKey(possibleNewPairA))
                    {
                        newPairs.Add(possibleNewPairA, increment);
                    }
                    else
                    {
                        newPairs[possibleNewPairA] += increment;
                    }

                    if (!newPairs.ContainsKey(possibleNewPairB))
                    {
                        newPairs.Add(possibleNewPairB, increment);
                    }
                    else
                    {
                        newPairs[possibleNewPairB] += increment;
                    }

                    // update alphabit counts
                    var insertAlphaChar = char.Parse(insertAlpha);
                    if (!alphabitCounts.ContainsKey(insertAlphaChar))
                    {
                        alphabitCounts.Add(insertAlphaChar, increment);
                    }
                    else
                    {
                        alphabitCounts[insertAlphaChar] += increment;
                    }
                }

                pairs = newPairs;
                steps--;
            }

            var minCount = alphabitCounts.Values.Min();
            var maxCount = alphabitCounts.Values.Max();

            return maxCount - minCount;
        }
    }
}

