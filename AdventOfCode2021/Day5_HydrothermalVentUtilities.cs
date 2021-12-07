namespace AdventOfCode2021
{
    public static class Day5_HydrothermalVentUtilities
    {
        public static int GetCountOfGridPointsToAvoid(string[] input)
        {
            var lines = ParseLinePoints(input, false);
            int count = CalculateGridSquaresToAvoid(lines);

            return count;
        }

        public static int GetCountOfGridPointsToAvoidWithDiagnols(string[] input)
        {
            var lines = ParseLinePoints(input, true);
            int count = CalculateGridSquaresToAvoid(lines);

            return count;
        }

        private static int CalculateGridSquaresToAvoid(int[][] lines)
        {
            // for each line, increament grid squares in matrix
            var gridSize = 1000;
            var grid = new int[gridSize, gridSize];
            foreach (var pointsInLine in lines)
            {
                var x1 = pointsInLine[0];
                var x2 = pointsInLine[2];
                var y1 = pointsInLine[1];
                var y2 = pointsInLine[3];
                if (x1 == x2) // vertical line
                {
                    int min = Math.Min(y1, y2);
                    int max = Math.Max(y1, y2);
                    for (int y = min; y <= max; y++)
                    {
                        grid[x1, y]++;
                    }
                }
                else if (y1 == y2) // horizontal line
                {
                    int min = Math.Min(x1, x2);
                    int max = Math.Max(x1, x2);
                    for (int x = min; x <= max; x++)
                    {
                        grid[x, y1]++;
                    }
                }
                else // diagnoal (45 degrees)
                {
                    var x = x1;
                    var y = y1;
                    var pointsRemaining = Math.Abs(x1 - x2) + 1;
                    while (pointsRemaining > 0)
                    {
                        grid[x, y]++;
                        x = x1 < x2 ? ++x : --x;
                        y = y1 < y2 ? ++y : --y;
                        pointsRemaining--;
                    }
                }
            }

            // count sqares where grid square value >= 2
            int count = 0;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (grid[i, j] >= 2)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private static int[][] ParseLinePoints(string[] input, bool includeDiagonals = false)
        {
            var lines = new List<int[]>();
            foreach(var line in input)
            {
                // e.g., line: 0,9 -> 5,9
                var points = line.Replace(" -> ", ",").Split(',', StringSplitOptions.TrimEntries).Select(x => int.Parse(x)).ToArray();
                if (includeDiagonals)
                {
                    lines.Add(new int[] { points[0], points[1], points[2], points[3] });
                }
                else if (points[0] == points[2] || points[1] == points[3])
                {
                    lines.Add(new int[] { points[0], points[1], points[2], points[3] });
                }
            }

            return lines.ToArray();
        }
    }
}
