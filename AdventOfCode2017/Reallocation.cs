namespace AdventOfCode2017
{
    public class Reallocation
    {
        public static (int count, int cyclesApart) ReallocateAndCountToRepeat(int[] input)
        {
            int count = 0;
            int cyclesApart = 0;

            int size = input.Length;
            int[] workingRegister = new int[size];
            Array.Copy(input, workingRegister, size);
            var registerHistory = new List<int[]> {input};

            while (true)
            {
                count++;

                // find amount to allocate
                var amountToAllocate = workingRegister.Max();

                // find starting position
                int i = workingRegister.TakeWhile(number => number != amountToAllocate).Count();
                workingRegister[i] = 0;

                // distribute
                while (amountToAllocate > 0)
                {
                    if (i < size - 1)
                    {
                        i++;
                    }
                    else
                    {
                        i = 0;
                    }
                    workingRegister[i]++;
                    amountToAllocate--;
                }

                var newRegistry = new int[size];
                Array.Copy(workingRegister, newRegistry, size);

                // Have we repeated yet?
                var index = registerHistory.FindIndex(x => x.SequenceEqual(newRegistry));
                if (index >= 0)
                {
                    cyclesApart = registerHistory.Count - index;
                    break;
                }

                registerHistory.Add(newRegistry);
            }
            

            return new ValueTuple<int, int>(count, cyclesApart);
        }
    }
}
