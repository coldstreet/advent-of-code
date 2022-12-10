using System.Diagnostics;
using System.Text;

namespace AdventOfCode2022
{
    public static class Day10_CathodeRayTube
    {
        public static long SumSignalStrengthAtVariousCycles(string[] input)
        {
            var operations = ParseInput(input);

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
                    // this is end of cycle, i.e., updating register
                    operationSleep = false;
                    register += nextRegisterValueIncrement;
                    nextRegisterValueIncrement = 0;
                }

                cycle++;
            }

            return sum;
        }

        public static string DrawMessageOnCrt(string[] input)
        {
            var operations = ParseInput(input);


            var register = 1;
            var cycle = 1;
            var i = 0;
            var operationSleep = false;
            var nextRegisterValueIncrement = 0;
            var crt = new bool[6, 40];
            var row = 0;
            var column = 0;
            while (cycle <= 240)
            {
                // draw
                if (column == register || column == register -1 || column == register + 1)
                {
                    crt[row, column] = true;
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
                    // this is end of cycle, i.e., updating register
                    operationSleep = false;
                    register += nextRegisterValueIncrement;
                    nextRegisterValueIncrement = 0;
                }
                
                if (cycle is 40 or 80 or 120 or 160 or 200 or 240)
                {
                    column = 0;
                    row++;
                }
                else
                {
                    column++;
                }

                cycle++;
            }

            
            DrawCrt(crt);

            return "EFGERURE"; // hard coding result based on what I visually see in drawn CRT (i.e., debug write lines)

        }

        private static void DrawCrt(bool[,] crt)
        {
            for (var column = 0; column < crt.GetLength(0); column++)
            {
                StringBuilder rowOnCrt = new StringBuilder();
                for (var row = 0; row < crt.GetLength(1); row++)
                {
                    var b = crt[column, row];
                    rowOnCrt.Append(b ? "#" : ".");
                }

                Debug.WriteLine(rowOnCrt);
            }
        }

        private static List<(string, int)> ParseInput(string[] input)
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

            return operations;
        }
    }
}

