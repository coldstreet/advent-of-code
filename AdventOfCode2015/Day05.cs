using System.Text.RegularExpressions;

namespace AdventOfCode2015
{
    public class Day05
    {
        public static int CountNiceStrings(string[] inputs)
        {
            return inputs.Count(DetermineIfNice);
        }

        public static bool DetermineIfNice(string input)
        {
            return IsStringNice(input);
        }

        private static bool IsStringNice(string input)
        {
            if (Regex.IsMatch(input, "ab|cd|pq|xy", RegexOptions.IgnoreCase))
            {
                return false;
            }

            return Regex.IsMatch(input, @"(.)\1{1,}", RegexOptions.IgnoreCase) &&
                   Regex.Matches(input, @"[aeiou]", RegexOptions.IgnoreCase).Count >= 3;
        }
    }
}
