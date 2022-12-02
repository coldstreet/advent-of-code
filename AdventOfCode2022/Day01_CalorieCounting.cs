namespace AdventOfCode2022
{
    public static class Day01_CalorieCounting
    {
        public static long FindMaxCalorieForAnElf(string[] input)
        {
            var caloriesPerElf = ParseInputForTotalCaloriesPerElf(input);

            return caloriesPerElf.Max();
        }

        public static long FindMaxCalorieForTopThreeElves(string[] input)
        {
            var caloriesPerElf = ParseInputForTotalCaloriesPerElf(input);

            return caloriesPerElf.OrderByDescending(x => x).Take(3).Sum();
        }

        private static List<int> ParseInputForTotalCaloriesPerElf(string[] input)
        {
            var caloriesPerElf = new List<int>();
            var sum = 0;
            foreach (var item in input)
            {
                if (item.Length == 0)
                {
                    caloriesPerElf.Add(sum);
                    sum = 0;
                    continue;
                }

                sum += int.Parse(item);
            }

            caloriesPerElf.Add(sum);
            return caloriesPerElf;
        }
    }
}