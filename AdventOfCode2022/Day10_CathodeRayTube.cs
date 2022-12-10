namespace AdventOfCode2022
{
    public static class Day10_CathodeRayTube
    {
        public static long SumSignalStrengthAtVariousCycles(string[] input)
        {
            var operations = new List<(string, int)>();
            foreach (var s in input)
            {
                int value = 0;
                var instruction = s.Substring(0, 4);
                if (instruction == "addx")
                {
                    value = int.Parse(s.Split(" ")[1]);
                }

                operations.Add((instruction, value));
            }

            var sum = 0;
            var register = 1;
            var cycle = 1;
            var i = 0;
            var operationSleep = false;
            var nextRegisterValueIncrement = 0;
            while (cycle <= 220)
            {
                if (cycle is 20 or 60 or 100 or 140 or 180 or 220)
                {
                    sum += cycle * register;
                }

                if (!operationSleep && i < operations.Count)
                {
                    var instruction = operations[i];
                    if (instruction.Item1 == "addx")
                    {
                        operationSleep = true;
                        nextRegisterValueIncrement = instruction.Item2;
                    }
                    
                    i++;
                }
                else
                {
                    operationSleep = false;
                    register += nextRegisterValueIncrement;
                    nextRegisterValueIncrement = 0;
                }

                cycle++;
            }

            return sum;
        }
    }
}

