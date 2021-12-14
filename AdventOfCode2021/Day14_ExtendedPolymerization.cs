namespace AdventOfCode2021
{
    public static class Day14_ExtendedPolymerization
    {
        public static int FindProductOfLeastAndMostLetters(string[] input)
        {
            var code = input[0];

            var insertInstructions = input
                .Take(input.Length - 2)
                .Select(l => l.Split(" -> ").ToArray());

            // build pairs list
            var pairs = new HashSet<string>();
            for (int i = 0; i < code.Length - 1; i++)
            {
                var pair = code.Substring(i, 2);
                if (!pairs.Contains(pair))
                {
                    pairs.Add(pair);
                }
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


            return 0;
        }
    }
}

