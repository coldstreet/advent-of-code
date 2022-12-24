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

            var minX = int.MaxValue;
            var maxX = int.MinValue;
            foreach (var s in sensors)
            {
                minX = Math.Min(minX, s.Location.X - s.DistanceToBeacon);
                maxX = Math.Max(maxX, s.Location.X + s.DistanceToBeacon);
            }

            var cannotContainBeaconInRow = new HashSet<int>();
            for(int x = minX; x <= maxX; x++)
            {
                foreach (var s in sensors)
                {
                    var distance = Math.Abs(x - s.Location.X) + Math.Abs(rowToCheck - s.Location.Y);
                    if (distance > s.DistanceToBeacon)
                    {
                        continue;
                    }
                    else if (s.BeaconLocation.X == x && s.BeaconLocation.Y == rowToCheck)
                    {
                        continue;
                    }

                    if (cannotContainBeaconInRow.Contains(x))
                    {
                        continue;
                    }
                    cannotContainBeaconInRow.Add(x);
                }
            }

            return cannotContainBeaconInRow.Count;
        }
    }

    public record Sensor(Coordinates Location, Coordinates BeaconLocation)
    {
        public int DistanceToBeacon { get; } = Math.Abs(Location.X - BeaconLocation.X) + Math.Abs(Location.Y - BeaconLocation.Y);
    }

    public record Coordinates(int X, int Y);

    public class SignalRange
    {
        public int StartX { get; set; }

        public int EndX { get; set; }

        public int Length => EndX - StartX + 1;

        public SignalRange(int startX, int endX)
        {
            StartX = startX;
            EndX = endX;
        }
    }
}

