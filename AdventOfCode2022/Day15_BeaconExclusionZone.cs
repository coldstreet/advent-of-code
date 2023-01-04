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

            var rows = new Dictionary<int, List<SignalRange>>();
            foreach (var s in sensors.OrderByDescending(s => s.DistanceToBeacon))
            {
                // is sensor range outside grid?
                if (s.Location.Y + s.DistanceToBeacon < 0
                    || s.Location.Y - s.DistanceToBeacon > upperGridBound
                    || s.Location.X + s.DistanceToBeacon < 0
                    || s.Location.X - s.DistanceToBeacon > upperGridBound)
                {
                    continue;
                }

                // process rows that are inside grid
                var minY = Math.Max(0, s.Location.Y - s.DistanceToBeacon);
                var maxY = Math.Min(upperGridBound, s.Location.Y + s.DistanceToBeacon);

                for(var y = minY; y <= maxY; y++)
                {
                    var distance = Math.Abs(s.Location.Y - y);
                    var lowX = Math.Max(0, s.Location.X - (s.DistanceToBeacon - distance));
                    var highX = Math.Min(upperGridBound, s.Location.X + (s.DistanceToBeacon - distance));
                    var range = new SignalRange(lowX, highX);
                    if (rows.ContainsKey(y))
                    {
                        rows[y].Add(range);
                        rows[y] = ReduceListByMergingRangesThatOverlap(rows[y]);
                    }
                    else
                    {
                        rows.Add(y, new List<SignalRange> { range });
                    }
                }
            }

            var rowWithGap = rows.First(r => r.Value.Count > 1);
            long x = rowWithGap.Value.First().EndX + 1;
            long result = x * 4000000 + (long) rowWithGap.Key;
            return result;
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

        public static List<SignalRange> ReduceListByMergingRangesThatOverlap(List<SignalRange> ranges)
        {
            var reducedList = new List<SignalRange>();

            foreach (var range in ranges.OrderBy(r => r.StartX))
            {
                if (reducedList.Count == 0)
                {
                    reducedList.Add(range);
                }
                else
                {
                    var lastRange = reducedList.Last();
                    if (range.StartX <= lastRange.EndX)
                    {
                        lastRange.StartX = Math.Min(lastRange.StartX, range.StartX);
                        lastRange.EndX = Math.Max(lastRange.EndX, range.EndX);
                    }
                    else
                    {
                        reducedList.Add(range);
                    }
                }
            }

            return reducedList;
        }
    }

    public record Sensor(Coordinates Location, Coordinates BeaconLocation)
    {
        public int DistanceToBeacon { get; } = Math.Abs(Location.X - BeaconLocation.X) + Math.Abs(Location.Y - BeaconLocation.Y);

        public Dictionary<int, SignalRange> RangesPerRow
        {
            get
            {
                var rangesPerRow = new Dictionary<int, SignalRange>();
                var y = Location.Y - DistanceToBeacon;
                var xRange = new SignalRange(Location.X, Location.X);
                while (y <= Location.Y + DistanceToBeacon)
                {
                    rangesPerRow.Add(y, xRange);
                    
                    xRange = y < Location.Y 
                        ? new SignalRange(xRange.StartX - 1, xRange.EndX + 1) 
                        : new SignalRange(xRange.StartX + 1, xRange.EndX - 1);

                    y++;
                }

                return rangesPerRow;
            }
        }
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

