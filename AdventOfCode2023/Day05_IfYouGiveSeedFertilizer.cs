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

