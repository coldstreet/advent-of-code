namespace AdventOfCode2022
{
    public static class Day11_MonkeyInTheMiddle

    {
        public static long DetermineLevelOfMonkeyBusiness(string[] input, int rounds, bool haveSomeRelief = true)
        {
            // parse input
            var monkeys = ParseInput(input);
            var multiplier = monkeys.Aggregate(1, (s, x) => s * x.DivisibleBy);

            while (rounds > 0)
            {
                for (var i = 0; i < monkeys.Count; i++)
                {
                    var monkey = monkeys[i];
                    while (monkey.WorryLevelPerItem.Count > 0)
                    {
                        monkey.InspectionCount++;
                        var worryLevel = monkey.WorryLevelPerItem.Dequeue();
                        var operationValue = monkey.OperationOnSelf 
                            ? worryLevel 
                            : monkey.OperationValue;
                        var newWorryLevel = monkey.Operation == '*' 
                            ? worryLevel * operationValue 
                            : worryLevel + operationValue;
                        if (haveSomeRelief) // part I
                        {
                            newWorryLevel = (long) Math.Floor(newWorryLevel / 3.0);
                        }

                        newWorryLevel %= multiplier;
                        if (newWorryLevel % monkey.DivisibleBy == 0)
                        {
                            monkeys[monkey.IfTrueMonkeyToThrowTo].WorryLevelPerItem.Enqueue(newWorryLevel);
                        }
                        else
                        {
                            monkeys[monkey.IfFalseMonkeyToThrowTo].WorryLevelPerItem.Enqueue(newWorryLevel);
                        }
                    }
                }

                rounds--;
            }

            // Find top 2 active monkeys and multiply inspection numbers
            var top2Counts = monkeys.OrderByDescending(x => x.InspectionCount).Take(2).Select(x => x.InspectionCount).ToArray();

            return top2Counts[0] * top2Counts[1];
        }

        private static List<MonkeyInstructions> ParseInput(string[] input)
        {
            var monkeys = new List<MonkeyInstructions>();
            var mi = new MonkeyInstructions();
            foreach (var item in input)
            {
                if (item.StartsWith("Monkey"))
                {
                    continue;
                }

                if (item.StartsWith("  Starting items: "))
                {
                    mi.WorryLevelPerItem = new Queue<long>(item.Substring(18).Split(", ").Select(long.Parse).ToList());
                    continue;
                }

                if (item.StartsWith("  Operation: new = old "))
                {
                    mi.Operation = char.Parse(item.Substring(23, 1));
                    if (item.Substring(25) == "old")
                    {
                        mi.OperationOnSelf = true;
                        continue;
                    }

                    mi.OperationValue = int.Parse(item.Substring(25));
                    continue;
                }

                if (item.StartsWith("  Test: divisible by "))
                {
                    mi.DivisibleBy = int.Parse(item.Substring(21));
                    continue;
                }

                if (item.StartsWith("    If true: throw to monkey "))
                {
                    mi.IfTrueMonkeyToThrowTo = int.Parse(item.Substring(29));
                    continue;
                }

                if (item.StartsWith("    If false: throw to monkey "))
                {
                    mi.IfFalseMonkeyToThrowTo = int.Parse(item.Substring(30));
                    continue;
                }

                if (string.IsNullOrEmpty(item))
                {
                    monkeys.Add(mi);
                    mi = new MonkeyInstructions();
                }
            }

            monkeys.Add(mi);
            return monkeys;
        }

        private static int ModuloForLargeNumber(String num, int a)
        {
            int result = 0;
            foreach (var i in num)
            {
                result = (result * 10 + i - '0') % a;
            }
            
            return result;
        }
    }

    public class MonkeyInstructions
    {
        public Queue<long> WorryLevelPerItem { get; set; } 
        public char Operation { get; set; }
        public bool OperationOnSelf { get; set; }
        public int OperationValue { get; set; }
        public int DivisibleBy { get; set;  }
        public int IfTrueMonkeyToThrowTo { get; set; }
        public int IfFalseMonkeyToThrowTo { get; set; }
        public long InspectionCount { get; set; }

        public MonkeyInstructions()
        {
            WorryLevelPerItem = new Queue<long>();
        }
    }
}

