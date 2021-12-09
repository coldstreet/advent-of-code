namespace AdventOfCode2021
{
    public static class Day9_SmokeBasin
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
    }
}
