using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2022
{
    public static class Day15_BeaconExclusionZone
    {
        public static long NumberOfPositionsWithoutBeacon(string[] input, int rowToCheck)
        {
            var sensors = ParseSensorAndBeaconInput(input);

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
                    
                    if (s.BeaconLocation.X == x && s.BeaconLocation.Y == rowToCheck)
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

        // part 2
        public static long FindDistressBeacon(string[] input, int upperGridBound)
        {
            var sensors = ParseSensorAndBeaconInput(input);

            var minX = int.MaxValue;
            var maxX = int.MinValue;
            foreach (var s in sensors)
            {
                if (s.Location.X + s.DistanceToBeacon >= 0)
                {
                    minX = Math.Min(minX, s.Location.X - s.DistanceToBeacon);
                }

                if (s.Location.X - s.DistanceToBeacon <= upperGridBound)
                {
                    maxX = Math.Max(maxX, s.Location.X + s.DistanceToBeacon);
                }
            }

            for (int y = 0; y <= upperGridBound; y++)
            {
                var cannotContainBeaconInRow = new HashSet<int>();
                for (int x = minX; x <= maxX; x++)
                {
                    foreach (var s in sensors)
                    {
                        var distance = Math.Abs(x - s.Location.X) + Math.Abs(y - s.Location.Y);
                        if (distance > s.DistanceToBeacon)
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

                var orderedItems = cannotContainBeaconInRow.OrderBy(_ => _).Where(x => x >= 0 && x <= upperGridBound).ToArray();
                if (orderedItems.Length == upperGridBound + 1)
                {
                    continue;
                }

                var previous = -1;
                for (var x = 0; x < orderedItems.Length; x++)
                {
                    if (orderedItems[x] - previous > 1)
                    {
                        return x * 4000000 + y; 
                    }

                    previous = orderedItems[x];
                }
            }
            
            throw new ApplicationException("Unexpected - logic is wrong");
        }

        private static List<Sensor> ParseSensorAndBeaconInput(string[] input)
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

                sensors.Add(
                    new Sensor(new Coordinates(locations[0], locations[1]), new Coordinates(locations[2], locations[3])));
            }

            return sensors;
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

