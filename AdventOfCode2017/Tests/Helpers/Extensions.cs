namespace AdventOfCode2017.Tests.Helpers
{
    internal static class Extensions
    {
        internal static int[] ConvertToIntArray(this string[] input)
        {
            List<int> integers = new List<int>();
            foreach (var row in input)
            {
                if (!int.TryParse(row, out int number))
                {
                    throw new ArgumentException("Unexpected non number input from input file");
                }

                integers.Add(number);
            }

            return integers.ToArray();
        }
    }
}
