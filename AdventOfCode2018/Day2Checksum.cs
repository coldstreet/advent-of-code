namespace AdventOfCode2018
{
    public class Day2Checksum
    {
        public static int CalculateChecksum(string[] inputs)
        {
            int exactTwoCount = 0;
            int exactThreeCount = 0;

            foreach (var input in inputs)
            {
                (bool exactlyTwo, bool exactlyThree) = CheckForTwoAndThreeOfAny(input);
                if (exactlyTwo)
                {
                    exactTwoCount++;
                }

                if (exactlyThree)
                {
                    exactThreeCount++;
                }
            }

            return exactTwoCount * exactThreeCount;
        }

        public static string FindCommonLettersForTwoCorrectBoxIDs(string[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = i + 1; j < inputs.Length; j++)
                {
                    if (inputs[i].Length != inputs[j].Length)
                    {
                        continue;
                    }

                    var diffCount = CheckForCorrectIds(inputs[i], inputs[j], out var lastDiffIndex);

                    if (diffCount == 1)
                    {
                        return inputs[i].Remove(lastDiffIndex, 1);
                    }
                }
            }

            throw new InvalidDataException("Input data does not have two correct IDs");
        }

        private static (bool exactlyTwo, bool exactlyThree) CheckForTwoAndThreeOfAny(string input)
        {
            var counts = input.GroupBy(c => c) // put each character into a "bucket"
                .ToDictionary(grp => grp.Key, grp => grp.Count());  // then convert to dictionary where key = character, value = count

            bool exactlyTwo = counts.Values.Contains(2);
            bool exactlyThree = counts.Values.Contains(3);

            return (exactlyTwo, exactlyThree);
        }

        private static int CheckForCorrectIds(string id1, string id2, out int lastDiffIndex)
        {
            var diffCount = 0;
            lastDiffIndex = 0;
            for (var charIndex = 0; charIndex < id1.Length; charIndex++)
            {
                if (id1[charIndex] != id2[charIndex])
                {
                    diffCount++;
                    lastDiffIndex = charIndex;
                }

                if (diffCount > 1)
                {
                    break;
                }
            }

            return diffCount;
        }
    }
}
