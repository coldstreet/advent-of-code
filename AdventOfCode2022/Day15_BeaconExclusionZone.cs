namespace AdventOfCode2022
{
    public static class Day15_BeaconExclusionZone
    {
        public static long NumberOfPositionsWithoutBeacon(string[] input, int rowToCheck)
        {
            var sensors = new List<Sensor>();
            foreach (var s in input)
            {
                var x = s.Substring("Sensor at x=".Length);
                var locations = s
                    .Substring("Sensor at x=".Length)
                    .Replace(": closest beacon is at x=", ",")
                    .Replace(" y=", "")
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray();

                sensors.Add(new Sensor(new Coordinates(locations[0], locations[1]), new Coordinates(locations[2], locations[3])));
            }

            return 0;
        }
    }

    public record Sensor(Coordinates Location, Coordinates BeaconLocation)
    {
        public int Distance { get; } = Math.Abs(Location.X - BeaconLocation.X) + Math.Abs(Location.Y - BeaconLocation.Y);
    }

    public record Coordinates(int X, int Y);
}

