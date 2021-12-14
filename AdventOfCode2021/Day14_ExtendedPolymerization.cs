namespace AdventOfCode2021
{
    public static class Day14_ExtendedPolymerization
    {
        public static int FindProductOfLeastAndMostLetters(string[] input, int steps)
        {
            var code = input[0];

            var insertInstructions = input
                .Take(input.Length - 2)
                .Select(l => l.Split(" -> "))
                .ToDictionary(k => k[0], v => v[1]);

            // build pairs list
            var pairs = new Dictionary<string, int>();
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

            // build alphabit counts
            var alphabitCounts = new Dictionary<char, int>(26);
            foreach(var c in code)
            {
                if (!alphabitCounts.ContainsKey(c))
                {
                    alphabitCounts.Add(c, 1);
                    continue;
                }

                alphabitCounts[c]++;
            }

            while(steps > 0)
            {
                var pairsToProcess = pairs.Keys;
                foreach(var pair in pairsToProcess)
                {
                    var insertAlpha = insertInstructions[pair];
                    var insertAlphaChar = char.Parse(insertAlpha);

                    // todo - update pair dictionary

                    // todo - depends on number of pairs
                    if (!alphabitCounts.ContainsKey(insertAlphaChar))
                    {
                        alphabitCounts.Add(insertAlphaChar, 1);
                    }
                    else
                    {
                        alphabitCounts[insertAlphaChar]++;
                    }


                }

                steps--;
            }

            var minCount = alphabitCounts.Values.Min();
            var maxCount = alphabitCounts.Values.Max();

            return minCount * maxCount;
        }
    }
}

