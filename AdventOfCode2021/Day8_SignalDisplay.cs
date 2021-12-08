namespace AdventOfCode2021
{
    public static class Day8_SignalDisplay
    {
        public static int CountDigits_2_4_5_7(string[] signalsAndOutputs)
        {
            var outputs = new List<string[]>();
            foreach (var signalAndOutput in signalsAndOutputs)
            {
                var output = signalAndOutput.Split('|', StringSplitOptions.TrimEntries).Last();
                if (output != null)
                {
                    outputs.Add(output.Split(' ', StringSplitOptions.TrimEntries));
                }
            }

            int count = 0;
            foreach(var output in outputs)
            {
                foreach(var digit in output)
                {
                    var length = digit.Length;
                    if (length == 2 || length == 3 || length == 4 || length == 7)
                    {
                        count++;
                    }
                }
            }
                
            return count;
        }
    }
}
