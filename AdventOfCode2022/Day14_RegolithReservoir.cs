using System.Diagnostics;
using System.Text;

namespace AdventOfCode2022
{
    public static class Day14_RegolithReservoir
    {
        public static long DetermineNumberOfSandBeforeInfiniteDrop(string[] input)
        {
            var rockLayerCoordinates = new List<int[]>();
            foreach (var s in input)
            {
                var coordinates = s
                    .Replace(" -> ", ",")
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray();
                rockLayerCoordinates.Add(coordinates);
            }

            var (minX, maxX, maxY, cave) = InitializeCave(rockLayerCoordinates);
            PrintCave(minX, cave);

            int sandAtRestCount = 0;
            bool sandNotFallingInAbyss = true;
            while (sandNotFallingInAbyss)
            {
                var restingCoordinates = MoveSandUntilBlocked(500, 0, cave);
                if (restingCoordinates.Item2 == maxY)
                {
                    sandNotFallingInAbyss = false;
                }
                else
                {
                    cave[restingCoordinates.Item1, restingCoordinates.Item2] = 'O';
                    sandAtRestCount++;
                }
            }

            return sandAtRestCount;
        }

        private static void PrintCave(int minX, char[,] cave)
        {
            Debug.WriteLine("The Cave");
            for (int y = 0; y < cave.GetLength(1); y++)
            {
                StringBuilder row = new StringBuilder();
                for (int x = minX - 1; x < cave.GetLength(0); x++)
                {
                    row.Append(cave[x, y]);
                }
                Debug.WriteLine(row);
            }
        }

        private static (int, int, int, char[,]) InitializeCave(List<int[]> rockLayerCoordinates)
        {
            var minX = int.MaxValue;
            var maxX = 0;
            var maxY = 0;
            foreach (var coordinates in rockLayerCoordinates)
            {
                for (int i = 0; i < coordinates.Length; i += 2)
                {
                    minX = Math.Min(minX, coordinates[i]);
                    maxX = Math.Max(maxX, coordinates[i]);
                    maxY = Math.Max(maxY, coordinates[i + 1]);
                }
            }

            var cave = new char[maxX + 2, maxY + 1];
            for (int y = 0; y < cave.GetLength(1); y++)
            {
                for (int x = 0; x < cave.GetLength(0); x++)
                {
                    cave[x, y] = '.';
                }
            }

            cave[500, 0] = '+';
            foreach (var coordinates in rockLayerCoordinates)
            {
                var previousCoordinates = (-1, -1);
                for (int i = 0; i < coordinates.Length; i += 2) // x1, y1, x2, y2, x3, y3, etc.
                {
                    var x = coordinates[i];
                    var y = coordinates[i + 1];
                    cave[x, y] = '#';
                    if (previousCoordinates.Item1 == -1 && previousCoordinates.Item2 == -1)
                    {
                        cave[x, y] = '#';
                    }
                    else
                    {
                        if (x - previousCoordinates.Item1 == 0)
                        {
                            var from = Math.Min(previousCoordinates.Item2, y);
                            var to = Math.Max(previousCoordinates.Item2, y);
                            for (int yi = from; yi <= to; yi++)
                            {
                                cave[x, yi] = '#';
                            }
                        }
                        else
                        {
                            var from = Math.Min(previousCoordinates.Item1,x);
                            var to = Math.Max(previousCoordinates.Item1, x);
                            for (int xi =from; xi <= to; xi++)
                            {
                                cave[xi, y] = '#';
                            }
                        }
                    }

                    previousCoordinates = (x, y);
                }
            }

            return (minX, maxX, maxY, cave);
        }

        private static (int, int) MoveSandUntilBlocked(int x, int y, char[,] cave)
        {
            var restingCoordinates = (x, y);
            if (y + 1 == cave.GetLength(1))
            {
                return restingCoordinates;
            }

            // try forward first, then left, and finally right
            if (cave[x, y + 1] == '.')
            {
                restingCoordinates = MoveSandUntilBlocked(x, y + 1, cave);
            }
            else if ((cave[x - 1, y + 1] == '.'))
            {
                restingCoordinates = MoveSandUntilBlocked(x - 1, y + 1, cave);
            }
            else if (cave[x + 1, y + 1] == '.')
            {
                restingCoordinates = MoveSandUntilBlocked(x + 1, y + 1, cave);
            }
            

            return restingCoordinates;
        }
    }
}

