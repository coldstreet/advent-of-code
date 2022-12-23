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

            var positionsWithoutBeacons = new HashSet<Coordinates>();
            foreach (var s in sensors)
            {
                var distance = s.DistanceToBeacon;
                for (int x = 0; x <= s.DistanceToBeacon; x++)
                {
                    for (int y = 0; y <= s.DistanceToBeacon - x; y++)
                    {
                        positionsWithoutBeacons.Add(new Coordinates(s.Location.X + x, s.Location.Y + y));
                        positionsWithoutBeacons.Add(new Coordinates(s.Location.X + (-1 * x), s.Location.Y + y));
                        positionsWithoutBeacons.Add(new Coordinates(s.Location.X + x, s.Location.Y + (-1 * y)));
                        positionsWithoutBeacons.Add(new Coordinates(s.Location.X + (-1 * x), s.Location.Y + (-1 * y)));
                    }
                }

                positionsWithoutBeacons.Remove(s.BeaconLocation);
            }

            var noBeaconsInRow = 0;
            foreach (var coordinates in positionsWithoutBeacons)
            {
                if (coordinates.X == rowToCheck)
                {
                    noBeaconsInRow++;
                }
            }
            
            return noBeaconsInRow;
        }
    }

    public record Sensor(Coordinates Location, Coordinates BeaconLocation)
    {
        public int DistanceToBeacon { get; } = Math.Abs(Location.X - BeaconLocation.X) + Math.Abs(Location.Y - BeaconLocation.Y);
    }

    public record Coordinates(int X, int Y);
}

