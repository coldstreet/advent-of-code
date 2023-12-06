using NUnit.Framework.Constraints;
using System.Diagnostics;

namespace AdventOfCode2023
{
    public static class Day03_GearRatios
    {
        public static long SumPartNumbers(string[] input)
        {
            // build grid
            char[,] grid = BuildGrid(input);

            var parts = new Dictionary<int, int>();
            List<char> numbers = new List<char>();
            bool isPart = false;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    var c = grid[i, j];
                    if (char.IsNumber(c))
                    {
                        numbers.Add(c);
                        (isPart, _) = DoesNumberInGridHavePartIdNearBy(i, j, grid);
                    }
                    else
                    {
                        CheckIfNumberIsPartAndUpdateList(parts, numbers, isPart);

                        numbers.Clear();
                        isPart = false;
                    }
                }

                CheckIfNumberIsPartAndUpdateList(parts, numbers, isPart);

                numbers.Clear();
                isPart = false;
            }

            int sum = 0;
            foreach (int key in parts.Keys)
            {
                sum += key * parts[key];
            }

            return sum;
        }

        
        public static long SumPartNumbersV2(string[] input)
        {
            // build grid
            char[,] grid = BuildGrid(input);

            var stars = new Dictionary<(int, int), (int, int)>();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i,j] == '*')
                    {
                        stars.Add(new(i, j), new(0, 0));
                    }
                }
            }

            List<char> numbers = new List<char>();
            bool isPart = false;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                int starI = 0;
                int starJ = 0;
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    var c = grid[i, j];
                    if (char.IsNumber(c))
                    {
                        numbers.Add(c);
                        if (isPart)
                        {
                            continue;
                        }

                        (isPart, (starI, starJ)) = DoesNumberInGridHavePartIdNearBy(i, j, grid, true);
                    }
                    else
                    {
                        CheckIfNumberIsPartAndUpdateList(stars, numbers, isPart, starI, starJ);

                        numbers.Clear();
                        isPart = false;
                    }
                }

                CheckIfNumberIsPartAndUpdateList(stars, numbers, isPart, starI, starJ);

                numbers.Clear();
                isPart = false;
            }

            int sum = 0;
            foreach ((int, int) key in stars.Keys)
            {
                sum += stars[key].Item1 * stars[key].Item2;
            }

            return sum;
        }

        private static char[,] BuildGrid(string[] input)
        {
            var grid = new char[input.Length, input[0].Length];
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    grid[i, j] = input[i][j];
                }
            }

            return grid;
        }

        private static void CheckIfNumberIsPartAndUpdateList(IDictionary<int, int> parts, IList<char> numbers, bool isPart)
        {
            if (numbers.Count > 0)
            {
                int number = BuildNumber(numbers);
                if (!parts.ContainsKey(number))
                {
                    parts.Add(number, 0);
                }

                if (isPart)
                {
                    parts[number]++;
                }
            }
        }

        private static void CheckIfNumberIsPartAndUpdateList(IDictionary<(int, int), (int, int)> stars, IList<char> numbers, bool isPart, int i, int j)
        {
            if (numbers.Count > 0 && isPart)
            {
                if (stars[(i, j)].Item1 == 0)
                {
                    stars[(i, j)] = (BuildNumber(numbers), 0);
                    return;
                }

                stars[(i, j)] = (stars[(i, j)].Item1, BuildNumber(numbers));
            }
        }

        private static (bool, (int, int)) DoesNumberInGridHavePartIdNearBy(int i, int j, char[,] grid, bool partSymbolMustBeStar = false)
        {
            int maxI = grid.GetLength(0);
            int maxJ = grid.GetLength(1);

            int up = i - 1;
            int down = i + 1;
            int left = j - 1;
            int right = j + 1;

            if (up >= 0 && left >=0 && HasPartSymbolAdjecent(grid[up, left], partSymbolMustBeStar))
            {
                return (true, new(up, left));
            }

            if (up >= 0 && HasPartSymbolAdjecent(grid[up, j], partSymbolMustBeStar))
            {
                return (true, new(up, j));
            }

            if (up >= 0 && right < maxJ && HasPartSymbolAdjecent(grid[up, right], partSymbolMustBeStar))
            {
                return (true, new(up, right));
            }

            if (left >= 0 && HasPartSymbolAdjecent(grid[i, left], partSymbolMustBeStar))
            {
                return (true, new(i, left));
            }

            if (right < maxJ && HasPartSymbolAdjecent(grid[i, right],partSymbolMustBeStar))
            {
                return (true, new(i, right));
            }

            if (down < maxI && left >= 0 && HasPartSymbolAdjecent(grid[down, left], partSymbolMustBeStar))
            {
                return (true, new(down, left));
            }

            if (down < maxI && HasPartSymbolAdjecent(grid[down, j], partSymbolMustBeStar))
            {
                return (true, new(down, j));
            }

            if (down < maxI && right < maxJ && HasPartSymbolAdjecent(grid[down, right], partSymbolMustBeStar))
            {
                return (true, new(down, right));
            }


            return (false, new(0, 0));
        }

        private static int BuildNumber(IEnumerable<char> numbers)
        {
            char[] chars = numbers.ToArray();
            string s = new string(chars);
            return int.Parse(s);
        }

        private static bool HasPartSymbolAdjecent(char c, bool partSymbolMustBeStar = false)
        {
            if (partSymbolMustBeStar)
            {
                return HasStarSymbolAdjecent(c);
            }

            if (char.IsNumber(c))
            {
                return false;
            }

            if (c == '.')
            {
                return false;
            }

            return true;
        }

        private static bool HasStarSymbolAdjecent(char c)
        {
            if (c == '*')
            {
                return true;
            }

            return false;
        }
    }
}

