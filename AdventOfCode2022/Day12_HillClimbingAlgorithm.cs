using AdventOfCode2021;

namespace AdventOfCode2022
{
    // based on Dijkstra's algorithm (https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm)
    public static class Day12_HillClimbingAlgorithm
    {
        public static long FindFewestStepsToHighSpot(string[] input)
        {
            // parse input
            var (grid, startingCoordinates, endingCoordinates) = ParseGridAndSetStartAndEnd(input);

            // initialize weightedRiskGrid
            var rowCount = grid.GetLength(1);
            var columnCount = grid.GetLength(0);
            var unvisitedGrid = new int[columnCount, rowCount];
            for (var i = 0; i < columnCount; i++)
            {
                for (var j = 0; j < rowCount; j++)
                {
                    unvisitedGrid[i, j] = int.MaxValue;
                }
            }

            var priorityQueue = new PriorityQueue<(int, int), int>();
            unvisitedGrid[startingCoordinates.Item1, startingCoordinates.Item2] = 0;
            priorityQueue.Enqueue((startingCoordinates.Item1, startingCoordinates.Item2), 0);
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            while (true)
            {
                (int i, int j) = priorityQueue.Dequeue();
                if (grid[i, j] == (int)Convert.ToChar('z') + 1)
                {
                    break;
                }

                var currentElevation = grid[i, j];

                // right
                if (IsValidSquareToVisit(i, j + 1, grid, currentElevation, visited))
                {
                    UpdateUnvisitedGridAndQueue(i, j + 1, unvisitedGrid[i, j], unvisitedGrid, priorityQueue);
                }

                // down
                if (IsValidSquareToVisit(i + 1, j, grid, currentElevation, visited))
                {
                    UpdateUnvisitedGridAndQueue(i + 1, j, unvisitedGrid[i, j], unvisitedGrid, priorityQueue);
                }

                // up
                if (IsValidSquareToVisit(i - 1, j, grid, currentElevation, visited))
                {
                    UpdateUnvisitedGridAndQueue(i - 1, j, unvisitedGrid[i, j], unvisitedGrid, priorityQueue);
                }

                // left
                if (IsValidSquareToVisit(i, j - 1, grid, currentElevation, visited))
                {
                    UpdateUnvisitedGridAndQueue(i, j - 1, unvisitedGrid[i, j], unvisitedGrid, priorityQueue);
                }

                visited.Add((i, j));
            }

            return unvisitedGrid[endingCoordinates.Item1,endingCoordinates.Item2];
        }

        private static (int[,], (int, int), (int, int)) ParseGridAndSetStartAndEnd(string[] input)
        {
            var grid = AdventOfCode2021.Tests.Helpers.TestUtilities.CreateRectangularArray(
                input
                    .Select(l => l.ToCharArray().Select(i => (int)Convert.ToChar(i)).ToArray())
                    .ToList());

            var endingCoordinates = (0, 0);
            var startingCoordinates = (0, 0);

            var rowCount = grid.GetLength(1);
            var columnCount = grid.GetLength(0);
            for (var i = 0; i < columnCount; i++)
            {
                for (var j = 0; j < rowCount; j++)
                {
                    if (grid[i, j] == (int)Convert.ToChar('E'))
                    {
                        endingCoordinates = (i, j);
                        grid[i, j] = (int)Convert.ToChar('z') + 1;
                    }

                    if (grid[i, j] == (int)Convert.ToChar('S'))
                    {
                        startingCoordinates = (i, j);
                        grid[i, j] = (int)Convert.ToChar('a');
                    }
                }
            }

            return (grid, startingCoordinates, endingCoordinates);
        }

        private static void UpdateUnvisitedGridAndQueue(int i, int j, int distance, int[,] unvisitedGrid, PriorityQueue<(int, int), int> priorityQueue)
        {
            if (distance + 1 < unvisitedGrid[i, j])
            {
                unvisitedGrid[i, j] = distance + 1;
                priorityQueue.Enqueue((i, j), unvisitedGrid[i, j]);
            }
        }

        private static bool IsValidSquareToVisit(int i, int j, int[,] grid, int currentElevation, HashSet<(int, int)> visited)
        {
            if (visited.Contains((i, j)))
            {
                return false;
            }

            var rowCount = grid.GetLength(1);
            var columnCount = grid.GetLength(0);
            return (j >= 0 && j < rowCount && i >= 0 && i < columnCount) && (grid[i, j] - 1 <= currentElevation);
        }
    }
}

