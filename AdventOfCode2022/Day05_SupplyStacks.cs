namespace AdventOfCode2022
{
    public static class Day05_SupplyStacks
    {
        public static string MoveCratesOneAtATime(string[] input)
        {
            // parse input
            var (stacksOfCrates, moveInstructions) = ParseStacksOfCratesAndInstructions(input);

            // Move Crates
            foreach (var moveInstruction in moveInstructions)
            {
                int numberOfCratesToMove = moveInstruction.Item1;
                int fromColumn = moveInstruction.Item2;
                int toColumn = moveInstruction.Item3;

                for (int i = 0; i < numberOfCratesToMove; i++)
                {
                    var crate = stacksOfCrates[fromColumn].Dequeue();
                    var priority = stacksOfCrates[toColumn].Count;
                    stacksOfCrates[toColumn].Enqueue(crate, priority * -1);
                }
            }

            string topCrateIds = string.Empty;
            foreach (var key in stacksOfCrates.Keys.OrderBy(x => x))
            {
                topCrateIds += stacksOfCrates[key].Peek();
            }


            return topCrateIds;
        }

        public static string MoveCratesManyAtATime(string[] input)
        {
            // parse input
            var (stacksOfCrates, moveInstructions) = ParseStacksOfCratesAndInstructions(input);

            // Move Crates
            foreach (var moveInstruction in moveInstructions)
            {
                int numberOfCratesToMove = moveInstruction.Item1;
                int fromColumn = moveInstruction.Item2;
                int toColumn = moveInstruction.Item3;

                var priority = stacksOfCrates[toColumn].Count + numberOfCratesToMove; ;
                for (int i = 0; i < numberOfCratesToMove; i++)
                {
                    var crate = stacksOfCrates[fromColumn].Dequeue();
                    stacksOfCrates[toColumn].Enqueue(crate, priority * -1);
                    priority--;
                }
            }

            string topCrateIds = string.Empty;
            foreach (var key in stacksOfCrates.Keys.OrderBy(x => x))
            {
                topCrateIds += stacksOfCrates[key].Peek();
            }


            return topCrateIds;
        }

        private static (Dictionary<int, PriorityQueue<char, int>>, List<(int, int, int)>) ParseStacksOfCratesAndInstructions(string[] input)
        {
            var stacksOfCrates = new Dictionary<int, PriorityQueue<char, int>>();
            var moveInstructions = new List<(int, int, int)>();
            foreach (var item in input)
            {
                if (item == "" || item[1] == '1')
                {
                    continue;
                }

                if (item.StartsWith("move"))
                {
                    var instructions = item
                        .Substring(5)
                        .Split(new string[] { " from ", " to " }, StringSplitOptions.None)
                        .Select(x => int.Parse(x))
                        .ToArray();
                    moveInstructions.Add((instructions[0], instructions[1], instructions[2]));
                    continue;
                }

                for (var i = 0; i < item.Length; i += 4)
                {
                    if (item[i] == '[')
                    {
                        var crateColumn = (i + 4) / 4;
                        if (!stacksOfCrates.ContainsKey(crateColumn))
                        {
                            stacksOfCrates.Add(crateColumn, new PriorityQueue<char, int>());
                        }

                        var priority = stacksOfCrates[crateColumn].Count;
                        stacksOfCrates[crateColumn].Enqueue(item[i + 1], priority);
                    }
                }
            }

            return (stacksOfCrates, moveInstructions);
        }
    }
}

