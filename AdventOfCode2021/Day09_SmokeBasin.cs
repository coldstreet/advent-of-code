namespace AdventOfCode2021
{
    public static class Day09_SmokeBasin
    {
        public static int FindLowPoints(int[][] grid)
        {
            int sum = 0;
            int maxRowIndex = grid.Length - 1;
            for (int i = 0; i < grid.Length; i++)
            {
                int maxColumnIndex = grid[i].Length - 1;

                // set up, down, left, right indexes 
                int up = i - 1 >= 0 ? i - 1 : i + 1;
                int down = i + 1 <= maxRowIndex ? i + 1 : i - 1;

                for (int j = 0; j < grid[i].Length; j++)
                {
                    int left = j - 1 >= 0 ? j - 1 : j + 1;
                    int right = j + 1 <= maxColumnIndex ? j + 1 : j - 1;

                    if (grid[i][j] < grid[up][j] &&
                        grid[i][j] < grid[down][j] &&
                        grid[i][j] < grid[i][left] &&
                        grid[i][j] < grid[i][right])
                    {
                        sum += grid[i][j] + 1;
                    }
                }
            }
               
            return sum;
        }

        public static int FindSizeOfAllBasins(int[][] grid)
        {
            var basinSizes = new List<int>();
            int maxRowIndex = grid.Length - 1;
            var counted = new bool[grid.Length, grid[0].Length];
            for (int i = 0; i < grid.Length; i++)
            {
                int maxColumnIndex = grid[i].Length - 1;

                // set up, down, left, right indexes 
                int up = i - 1 >= 0 ? i - 1 : i + 1;
                int down = i + 1 <= maxRowIndex ? i + 1 : i - 1;

                for (int j = 0; j < grid[i].Length; j++)
                {
                    int left = j - 1 >= 0 ? j - 1 : j + 1;
                    int right = j + 1 <= maxColumnIndex ? j + 1 : j - 1;

                    if (grid[i][j] < grid[up][j] &&
                        grid[i][j] < grid[down][j] &&
                        grid[i][j] < grid[i][left] &&
                        grid[i][j] < grid[i][right])
                    {
                        // basin found
                        int count = CountIncreasingAdjacentGrids(i, j, int.MinValue, grid, counted, maxRowIndex, maxColumnIndex); ;
                        basinSizes.Add(count);
                    }
                }
            }

            var result = basinSizes
                .OrderByDescending(x => x)
                .Take(3)
                .Aggregate((x, y) => x * y);

            return result;
        }

        private static int CountIncreasingAdjacentGrids(int i, int j, int lastNumber, int[][] grid, bool[,] counted, int maxRowIndex, int maxColumnIndex)
        {
            if (i < 0 || i > maxRowIndex)
            {
                return 0;
            }

            if (j < 0 || j > maxColumnIndex)
            {
                return 0;
            }

            if (counted[i, j])
            {
                return 0;
            }

            if (grid[i][j] == 9 || grid[i][j] <= lastNumber)
            {
                return 0;
            }

            int count = 1;
            counted[i, j] = true;

            // look left
            count += CountIncreasingAdjacentGrids(i, j - 1, grid[i][j], grid, counted, maxRowIndex, maxColumnIndex);

            // look up
            count += CountIncreasingAdjacentGrids(i + 1, j, grid[i][j], grid, counted, maxRowIndex, maxColumnIndex);

            // look right
            count += CountIncreasingAdjacentGrids(i, j + 1, grid[i][j], grid, counted, maxRowIndex, maxColumnIndex);

            // look down
            count += CountIncreasingAdjacentGrids(i - 1, j, grid[i][j], grid, counted, maxRowIndex, maxColumnIndex);

            return count;
        }
    }
}
