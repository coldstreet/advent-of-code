namespace AdventOfCode2018
{
    public class Day3Fabric
    {
        public static int CountOverlappingSquareInches(string[] items)
        {
            var result = 0;
            const string marked = "X";
            const int maxSize = 1000;

            var fabric = new string[maxSize, maxSize];
            foreach (var item in items)
            {
                var (id, left, top, width, height) = ParseInstructions(item);
                for (int row = top; row < height + top; row++)
                {
                    for (int column = left; column < width + left; column++)
                    {
                        fabric[row, column] = (fabric[row, column] == null) ? id : marked;
                    }
                }
            }

            for (int x = 0; x < maxSize; x += 1)
            {
                for (int y = 0; y < maxSize; y += 1)
                {
                    if (fabric[x, y] == marked)
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        public static string FindTheOneIdThatDoesNotOverlapping(string[] items)
        {
            string result = null!;
            const string marked = "X";
            const int maxSize = 1000;
            Dictionary<string, int> ids = new Dictionary<string, int>();

            var fabric = new string[maxSize, maxSize];
            foreach (var item in items)
            {
                var (id, left, top, width, height) = ParseInstructions(item);
                ids.Add(id, width * height);
                for (int row = top; row < height + top; row++)
                {
                    for (int column = left; column < width + left; column++)
                    {
                        fabric[row, column] = (fabric[row, column] == null) ? id : marked;
                    }
                }
            }

            foreach (var id in ids.Keys)
            {
                int totalSqInches = 0;
                for (int x = 0; x < maxSize; x += 1)
                {
                    for (int y = 0; y < maxSize; y += 1)
                    {
                        if (fabric[x, y] == id)
                        {
                            totalSqInches++;
                        }
                    }
                }

	            if (ids[id] != totalSqInches)
	            {
		            continue;
	            }

	            result = id;
	            break;
            }

            return result;
        }

        private static (string id, int left, int top, int width, int height) ParseInstructions(string instructions)
        {
            instructions = RemoveWhitespace(instructions);
            var items = instructions.Split(new char[] {'@', ',', ':', 'x'});

            return (items[0], int.Parse(items[1]), int.Parse(items[2]), int.Parse(items[3]), int.Parse(items[4]));
        }

        private static string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }
    }
}
