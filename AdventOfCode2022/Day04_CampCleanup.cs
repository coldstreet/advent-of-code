namespace AdventOfCode2022
{
    public static class Day04_CampCleanup
    {
        public static long FindFullyOverlappingPairs(string[] input)
        {
            var overlappingPairs = 0;

            foreach (var pair in input)
            {
                var x = pair
                    .Split('-', ',')
                    .Select(x => int.Parse(x))
                    .ToArray();
                
                if (DoPairsFullyOverlap(x[0], x[1], x[2], x[3]))
                {
                    overlappingPairs++;
                }
            }

            return overlappingPairs;
        }

        public static long FindOverlappingPairs(string[] input)
        {
            var overlappingPairs = 0;

            foreach (var pair in input)
            {
                var x = pair
                    .Split('-', ',')
                    .Select(x => int.Parse(x))
                    .ToArray();

                if (DoPairsOverlap(x[0], x[1], x[2], x[3]))
                {
                    overlappingPairs++;
                }
            }

            return overlappingPairs;
        }

        private static bool DoPairsFullyOverlap(int min1, int max1, int min2, int max2)
        {
            if (min1 >= min2 && max1 <= max2)
            {
                return true;
            }

            return min2 >= min1 && max2 <= max1;
        }

        private static bool DoPairsOverlap(int min1, int max1, int min2, int max2)
        {
            if (min1 >= min2 && max1 <= max2)
            {
                return true;
            }

            if (min2 >= min1 && max2 <= max1)
            {
                return true;
            }

            if (min1 <= min2 && max1 <= max2 && max1 >= min2)
            {
                return true;
            }

            return min1 >= min2 && max1 >= max2 && min1 <= max2;
        }
    }
}

