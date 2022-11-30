namespace AdventOfCode2017
{
    public class JumpOffsets
    {
        public static int PerformJumpsAndCountSteps(int[] input)
        {
            if (input == null || input.Length == 0)
            {
                return 0;
            }

            int[] jumpOffsets = new int[input.Length];
            Array.Copy(input, jumpOffsets, input.Length);

            int i = 0;
            int steps = 0;
            while (i >= 0 && i < input.Length)
            {
                var move = jumpOffsets[i];
                jumpOffsets[i]++;

                i = i + move;
                steps++;
            }

            return steps;
        }

        public static int PerformJumpsAndCountStepsV2(int[] input)
        {
            if (input == null || input.Length == 0)
            {
                return 0;
            }

            int[] jumpOffsets = new int[input.Length];
            Array.Copy(input, jumpOffsets, input.Length);

            int i = 0;
            int steps = 0;
            while (i >= 0 && i < input.Length)
            {
                var move = jumpOffsets[i];
                if (move >= 3)
                {
                    jumpOffsets[i]--;
                }
                else
                {
                    jumpOffsets[i]++;
                }

                i = i + move;
                steps++;
            }

            return steps;
        }
    }
}
