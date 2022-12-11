namespace AdventOfCode2022
{
    public static class Day09_RopeBridge
    {
        public static long CountTailVisitLocations(string[] input, int knots = 2)
        {
            var instructions = ParseInput(input);

            var tailMoves = new Dictionary<(int, int), int>();
            var headCoordinates = (0, 0);
            var tailCoordinates = (0, 0);
            foreach (var instruction in instructions)
            {
                var direction = instruction.Item1;
                var steps = instruction.Item2;

                (headCoordinates, tailCoordinates) = MoveHeadAndTailOne(direction, steps, tailMoves, headCoordinates, tailCoordinates);
            }

            return tailMoves.Count();
        }

        private static List<(char, int)> ParseInput(string[] input)
        {
            var instructions = new List<(char, int)>();
            foreach (var s in input)
            {
                var moveAndSteps = s.Split(" ");
                instructions.Add((char.Parse(moveAndSteps[0]), int.Parse(moveAndSteps[1])));
            }

            return instructions;
        }

        private static ((int, int), (int, int)) MoveHeadAndTailOne(char direction, int steps,
            Dictionary<(int, int), int> tailMoves, (int, int) headCoordinates, (int, int) tailCoordinates)
        {
            (int, int) newHeadCoordinates = (0, 0);
            switch (direction)
            {
                case 'R':
                    for (int i = headCoordinates.Item1 + 1; i <= headCoordinates.Item1 + steps; i++)
                    {
                        newHeadCoordinates = (i, headCoordinates.Item2);
                        if (!IsDirectDiagonal(newHeadCoordinates, tailCoordinates) &&
                            !IsHeadAndTailOnSame(newHeadCoordinates, tailCoordinates) &&
                            newHeadCoordinates.Item1 != tailCoordinates.Item1)
                        {
                            tailCoordinates = (i - 1, newHeadCoordinates.Item2);
                            UpdateTailLocation(tailMoves, tailCoordinates);
                        }
                    }

                    break;
                case 'L':
                    for (int i = headCoordinates.Item1 - 1; i >= headCoordinates.Item1 - steps; i--)
                    {
                        newHeadCoordinates = (i, headCoordinates.Item2);
                        if (!IsDirectDiagonal(newHeadCoordinates, tailCoordinates) &&
                            !IsHeadAndTailOnSame(newHeadCoordinates, tailCoordinates) &&
                            newHeadCoordinates.Item1 != tailCoordinates.Item1)
                        {
                            tailCoordinates = (i + 1, newHeadCoordinates.Item2);
                            UpdateTailLocation(tailMoves, tailCoordinates);
                        }
                    }

                    break;
                case 'U':
                    for (int j = headCoordinates.Item2 + 1; j <= headCoordinates.Item2 + steps; j++)
                    {
                        newHeadCoordinates = (headCoordinates.Item1, j);
                        if (!IsDirectDiagonal(newHeadCoordinates, tailCoordinates) &&
                            !IsHeadAndTailOnSame(newHeadCoordinates, tailCoordinates) &&
                            newHeadCoordinates.Item2 != tailCoordinates.Item2)
                        {
                            tailCoordinates = (newHeadCoordinates.Item1, j - 1);
                            UpdateTailLocation(tailMoves, tailCoordinates);
                        }
                    }

                    break;
                default:
                    for (int j = headCoordinates.Item2 - 1; j >= headCoordinates.Item2 - steps; j--)
                    {
                        newHeadCoordinates = (headCoordinates.Item1, j);
                        if (!IsDirectDiagonal(newHeadCoordinates, tailCoordinates) &&
                            !IsHeadAndTailOnSame(newHeadCoordinates, tailCoordinates) &&
                            newHeadCoordinates.Item2 != tailCoordinates.Item2)
                        {
                            tailCoordinates = (newHeadCoordinates.Item1, j + 1);
                            UpdateTailLocation(tailMoves, tailCoordinates);
                        }
                    }

                    break;
            }

            return (newHeadCoordinates, tailCoordinates);
        }

        private static bool IsDirectDiagonal((int, int) newHeadCoordinates, (int, int) tailCoordinates)
        {
            return Math.Abs(newHeadCoordinates.Item1 - tailCoordinates.Item1) == 1 &&
                   Math.Abs(newHeadCoordinates.Item2 - tailCoordinates.Item2) == 1;
        }

        private static bool IsHeadAndTailOnSame((int, int) newHeadCoordinates, (int, int) tailCoordinates)
        {
            return newHeadCoordinates.Item1 == tailCoordinates.Item1 &&
                   newHeadCoordinates.Item2 == tailCoordinates.Item2;
        }

        private static void UpdateTailLocation(Dictionary<(int, int), int> tailMoves, (int, int) newTailCoordinates)
        {
            if (tailMoves.ContainsKey(newTailCoordinates))
            {
                tailMoves[newTailCoordinates]++;
            }
            else
            {
                tailMoves.Add(newTailCoordinates, 1);
            }
        }
    }
}

