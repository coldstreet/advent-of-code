using System.Diagnostics;

namespace AdventOfCode2021
{
    public static class Day12_PassagePathing
    {
        internal class Cave
        {
            public string Name { get; }

            public int TimesVisited { get; set; }

            public IList<string> ConnectedCaves { get; set; }

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
            VisitCave("start", caves, new List<string>(), allPaths);

            totalPaths = allPaths.Count();
            return totalPaths;
        }

        private static void VisitCave(string caveName, Dictionary<string, Cave> caves, List<string> currentPath, List<string> allPaths)
        {
            currentPath.Add(caveName);
            if (caveName == "end")
            {
                allPaths.Add(string.Join(",", currentPath));
                //Debug.WriteLine(string.Join(",", currentPath));
                return;
            }

            caves[caveName].TimesVisited++;

            var smallCave = caveName.Any(char.IsLower) && caveName != "start";
            if (smallCave && caves[caveName].TimesVisited >= 2)
            {
                caves[currentPath[currentPath.Count - 1]].TimesVisited--;
                currentPath.RemoveAt(currentPath.Count - 1);
                caves[currentPath[currentPath.Count - 1]].TimesVisited--;
                currentPath.RemoveAt(currentPath.Count - 1);
                return;
            }

            var newPath = new string[currentPath.Count]; 
            currentPath.CopyTo(newPath);
            foreach (var connectedCaveName in caves[caveName].ConnectedCaves)
            {
                VisitCave(connectedCaveName, caves, newPath.ToList(), allPaths);
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
    }
}

