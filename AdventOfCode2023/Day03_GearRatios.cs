namespace AdventOfCode2023
{
    public static class Day03_GearRatios
    {
        public static long SumPartNumbers(string[] input)
        {
            // build grid
            var grid = new char[input.Length, input[0].Length];
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    grid[i, j] = input[i][j];
                }
            }

            var parts = new Dictionary<int, int>();
            List<char> numbers = new List<char>();
            bool isPart = false;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    var c = grid[i, j];
                    if (char.IsNumber(c))
                    {
                        numbers.Add(c);
                        if (DoesNumberInGridHavePartIdNearBy(i, j, grid))
                        {
                            isPart = true;
                        }
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
            foreach(int key in parts.Keys)
            {
                sum += key * parts[key];
            }
            
            return sum;
        }

        private static void CheckIfNumberIsPartAndUpdateList(Dictionary<int, int> parts, List<char> numbers, bool isPart)
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

        private static bool DoesNumberInGridHavePartIdNearBy(int i, int j, char[,] grid)
        {
            int maxI = grid.GetLength(0);
            int maxJ = grid.GetLength(1);

            int up = i - 1;
            int down = i + 1;
            int left = j - 1;
            int right = j + 1;

            if (up >= 0 && left >=0 && HasPartSymbolAdjecent(grid[up, left]))
            {
                return true;
            }

            if (up >= 0 && HasPartSymbolAdjecent(grid[up, j]))
            {
                return true;
            }

            if (up >= 0 && right < maxJ && HasPartSymbolAdjecent(grid[up, right]))
            {
                return true;
            }

            if (left >= 0 && HasPartSymbolAdjecent(grid[i, left]))
            {
                return true;
            }

            if (right < maxJ && HasPartSymbolAdjecent(grid[i, right]))
            {
                return true;
            }

            if (down < maxI && left >= 0 && HasPartSymbolAdjecent(grid[down, left]))
            {
                return true;
            }

            if (down < maxI && HasPartSymbolAdjecent(grid[down, j]))
            {
                return true;
            }

            if (down < maxI && right < maxJ && HasPartSymbolAdjecent(grid[down, right]))
            {
                return true;
            }


            return false;
        }

        private static int BuildNumber(IEnumerable<char> numbers)
        {
            char[] chars = numbers.ToArray();
            string s = new string(chars);
            return int.Parse(s);
        }

        private static bool HasPartSymbolAdjecent(char c)
        {
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
    }
}

