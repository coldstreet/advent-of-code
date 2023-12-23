namespace AdventOfCode2023
{
    public static class Day05_IfYouGiveSeedFertilizer
    {
        public static long DetermineLowestLocationNumber(string[] input)
        {
            (IEnumerable<long> seeds, IEnumerable<IEnumerable<Map>> categoryMaps) = ParseInput(input);

            long location = long.MaxValue;  
            foreach (var seed in seeds)
            {
                long source = seed;
                foreach(var category in categoryMaps)
                {
                    foreach (var map in category)
                    {
                        if (map.Source <= source && source <= map.Source + map.Range)
                        {
                            source = map.Destination - map.Source + source;
                            break;
                        }
                    }
                }

                location = Math.Min(source, location);
            }

            return location;
        }

        public static long DetermineLowestLocationNumberV2(string[] input)
        {
            (IEnumerable<long> seedPairs, IEnumerable<IEnumerable<Map>> categoryMaps) = ParseInput(input);

            var seedRanges = new List<(long, long)>();
            for(int i = 0; i < seedPairs.Count(); i += 2)
            {
                var start = seedPairs.ElementAt(i);
                var end = seedPairs.ElementAt(i) + seedPairs.ElementAt(i + 1) - 1;
                seedRanges.Add((start, end));
            }

            seedRanges = MergeRanges(seedRanges);

            long location = long.MaxValue;
            foreach (var seedRange in seedRanges) 
            {
                var seeds = new List<long>();
                for (long s = seedRange.Item1; s <= seedRange.Item2; s++)
                {
                    seeds.Add(s);
                }
                
                foreach (var seed in seeds)
                {
                    long source = seed;
                    foreach (var category in categoryMaps)
                    {
                        foreach (var map in category)
                        {
                            if (map.Source <= source && source <= map.Source + map.Range)
                            {
                                source = map.Destination - map.Source + source;
                                break;
                            }
                        }
                    }

                    location = Math.Min(source, location);
                }

            }

            return location;
        }

        internal static List<(long, long)> MergeRanges(IList<(long, long)> inputRanges)
        {
            // Sort the input ranges by their start values
            var sortedRanges = inputRanges.OrderBy(r => r.Item1);

            var mergedRanges = new List<(long, long)>();

            // Initialize the current range with the first range in the sorted list
            var currentRange = sortedRanges.First();

            foreach (var range in sortedRanges.Skip(1))
            {
                // Check if the current range and the next range can be merged
                if (range.Item1 <= currentRange.Item2)
                {
                    // Merge the ranges by taking the minimum start value and maximum end value
                    currentRange = (Math.Min(currentRange.Item1, range.Item1), Math.Max(currentRange.Item2, range.Item2));
                }
                else
                {
                    // Add the current merged range to the result and start a new current range
                    mergedRanges.Add(currentRange);
                    currentRange = range;
                }
            }

            // Add the last merged range to the result
            mergedRanges.Add(currentRange);

            return mergedRanges;
        }

        private static (IEnumerable<long> seeds, IEnumerable<IEnumerable<Map>> categoryMaps) ParseInput(string[] input)
        {
            var categoryMaps = new List<List<Map>>();
            var category = new List<Map>();

            var seeds = input[0].Split(new string[] { "seeds: ", " " }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse);
            for (long i = 3; i < input.Length; i++)
            {
                if (string.IsNullOrEmpty(input[i]))
                {
                    categoryMaps.Add(category);
                    category = new List<Map>();
                    continue;
                }

                if (input[i].Contains("map:"))
                {
                    continue;
                }

                var values = input[i]
                    .Split(new char[] { ' ' })
                    .Select(long.Parse)
                    .ToArray();
                var map = new Map(values[1], values[0], values[2]);
                category.Add(map);
            }

            categoryMaps.Add(category);

            return (seeds, categoryMaps);
        }
    }

    public class Map
    {
        public long Source { get; set; }
        public long Destination { get; set; }
        public long Range { get; set; } 

        public Map(long source, long destination, long range)
        {
            Source = source;
            Destination = destination;
            Range = range;

        }
    }
}

