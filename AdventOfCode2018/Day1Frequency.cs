namespace AdventOfCode2018
{
    public class Day1Frequency
    {
        public static int ChangeFrequency(int currentFrequency, List<int> stepChanges)
        {
            int newFrequency = currentFrequency;
            foreach (var i in stepChanges)
            {
                newFrequency += i;
            }

            return newFrequency;
        }

         public static int FindFirstRepeatFrequency(int currentFrequency, List<int> stepChanges, List<int>? foundFrequencies = null)
        {
            if (foundFrequencies == null)
            {
                foundFrequencies = new List<int>() { currentFrequency };
            }

            int newFrequency = currentFrequency;
            foreach (var i in stepChanges)
            {
                newFrequency += i;
                if (foundFrequencies.Contains(newFrequency))
                {
                    return newFrequency;
                }

                foundFrequencies.Add(newFrequency);
            }

            //
            // The repeat was not found so process the frequency step changes again
            //
            return FindFirstRepeatFrequency(newFrequency, stepChanges, foundFrequencies);
        }
    }
}
