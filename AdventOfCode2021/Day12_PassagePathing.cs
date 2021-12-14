namespace AdventOfCode2021
{
    public static class Day12_PassagePathing
    {
        internal class Cave
        {
            public string Name { get; }

            public int TimesVisited { get; set; }

            public IList<string> ConnectedCaves { get; set; }

            public bool IsSmallCave => Name.Any(char.IsLower);

            public Cave(string name)
            {
                Name = name;
                ConnectedCaves = new List<string>();
            }
        }

        public static int CountAllPaths(string[] input)
        {
            int totalPaths = 0;
            Dictionary<string, Cave> caves = BuildCaveDictionary(input);

            List<string> allPaths = new List<string>();
            VisitCave("start", caves, new Stack<string>(), allPaths);

            totalPaths = allPaths.Count();
            return totalPaths;
        }

        public static int CountAllPathsWithOneSmallCaveTwice(string[] input)
        {
            int totalPaths = 0;
            Dictionary<string, Cave> caves = BuildCaveDictionary(input);

            List<string> allPaths = new List<string>();
            VisitCave("start", caves, new Stack<string>(), allPaths, true);

            totalPaths = allPaths.Count();
            return totalPaths;
        }

        private static void VisitCave(string caveName, Dictionary<string, Cave> caves, Stack<string> currentPath, List<string> allPaths, bool allowOneSmallCaveTwice = false)
        {
            currentPath.Push(caveName);
            if (caveName == "end")
            {
                allPaths.Add(string.Join(",", currentPath.Reverse().ToArray()));
                return;
            }

            if (caves[caveName].IsSmallCave && caveName != "start")
            {
                bool oneSmallCaveVisitedTwice = caves.Values.Any(x => x.IsSmallCave && x.TimesVisited >= 2);
                int smallCaveLimit = allowOneSmallCaveTwice && !oneSmallCaveVisitedTwice ? 2 : 1;
                if (caves[caveName].TimesVisited >= smallCaveLimit)
                {
                    currentPath.Pop();
                    currentPath.Pop();
                    return;
                }
            }

            caves[caveName].TimesVisited++;

            foreach (var connectedCaveName in caves[caveName].ConnectedCaves)
            {
                VisitCave(connectedCaveName, caves, CloneStack(currentPath), allPaths, allowOneSmallCaveTwice);
            }

            if (caveName != "start")
            {
                caves[caveName].TimesVisited--;
            }

            return;
        }

        private static Dictionary<string, Cave> BuildCaveDictionary(string[] input)
        {
            var caves = new Dictionary<string, Cave>();
            foreach (var line in input)
            {
                var startAndEndNodes = line.Split('-', StringSplitOptions.TrimEntries);
                if (startAndEndNodes[1] == "start")
                {
                    continue;
                }

                if (!caves.ContainsKey(startAndEndNodes[0]))
                {
                    caves.Add(startAndEndNodes[0], new Cave(startAndEndNodes[0]));
                }

                caves[startAndEndNodes[0]].ConnectedCaves.Add(startAndEndNodes[1]);
            }

            foreach (var line in input)
            {
                var endAndStartNodes = line.Split('-', StringSplitOptions.TrimEntries).Reverse().ToArray();
                if (endAndStartNodes[1] == "start")
                {
                    continue;
                }

                if (!caves.ContainsKey(endAndStartNodes[0]))
                {
                    caves.Add(endAndStartNodes[0], new Cave(endAndStartNodes[0]));
                }

                caves[endAndStartNodes[0]].ConnectedCaves.Add(endAndStartNodes[1]);
            }

            caves.Remove("end");

            return caves;
        }

        private static Stack<string> CloneStack(Stack<string> original)
        {
            var arr = new string[original.Count];
            original.CopyTo(arr, 0);
            Array.Reverse(arr);
            return new Stack<string>(arr);
        }
    }
}

