namespace AdventOfCode2019
{
    public static class Day02_1202ProgramAlarm
    {
        public static long RestoreAndRunProgram(string[] input)
        {
            // parse input
            var opCodes = input[0].Split(',').Select(int.Parse).ToArray();
            
            // restore the gravity assist opCodes to the "1202 opCodes alarm" state
            opCodes[1] = 12; // "noun"
            opCodes[2] = 2; // "verb"
            
            // run the program
            var result = RunProgram(opCodes);
            
            return result;
        }

        public static long RunProgramWithVariousNounVerbsUntilOutputIsKnownValue(string[] input)
        {
            // parse input
            var startingOpCodes = input[0].Split(',').Select(int.Parse).ToArray();

            // try all possible noun/verb combinations between 0 and 99    
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    var opCodes = startingOpCodes.ToArray(); // make a copy of the starting opCodes via using ToArray()
                    opCodes[1] = noun;
                    opCodes[2] = verb;

                    if (RunProgram(opCodes) == 19690720)
                    {
                        return 100 * noun + verb;
                    }
                }
            }

            return 0;
        }

        private static int RunProgram(int[] opCodes)
        {
            // check first of four opCodes for instruction: 1 = add, 2 = multiply, 99 = halt
            for (var i = 0; i < opCodes.Length; i += 4)
            {
                var opCode = opCodes[i];
                if (opCode == 99)
                {
                    break;
                }

                var operand1 = opCodes[opCodes[i + 1]];
                var operand2 = opCodes[opCodes[i + 2]];
                var resultIndex = opCodes[i + 3];

                opCodes[resultIndex] = opCode switch
                {
                    1 => operand1 + operand2,
                    2 => operand1 * operand2,
                    _ => opCodes[resultIndex]
                };
            }

            return opCodes[0];
        }
    }
}

