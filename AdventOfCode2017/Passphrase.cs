namespace AdventOfCode2017
{
    public class Passphrase
    {
        public static bool Validate(string passphrase)
        {
            var words = passphrase.Split(' ');
            return words.Distinct().Count() == words.Length;
        }

        public static bool ValidateForAnagrams(string passphrase)
        {
            var words = passphrase.Split(' ');
            if (words.Distinct().Count() != words.Length)
            {
                return false;
            }

            for (int i = 0; i < words.Length; i++)
            {
                for (int j = i + 1; j < words.Length; j++)
                {
                    if (Passphrase.IsAnagram(words[i], words[j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsAnagram(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
                return false;
            if (s1.Length != s2.Length)
                return false;

            var sortedCharsOfS1 = s1.ToCharArray().OrderBy(c => c);
            var sortedCharsOfS2 = s2.ToCharArray().OrderBy(c => c);

            return sortedCharsOfS2.SequenceEqual(sortedCharsOfS1);
        }
    }
}
