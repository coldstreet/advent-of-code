namespace AdventOfCode2022
{
    public static class Day09_RopeBridge
    {
        public static long CountTailVisitLocations(string[] input)
        {
            var instructions = ParseInput(input);

            var tailMoves = new Dictionary<(int, int), int>();
            var coordinatesPerKnot = new List<(int, int)> { (0,0), (0,0) };

            foreach (var instruction in instructions)
            {
                var direction = instruction.Item1;
                var steps = instruction.Item2;

                MoveHeadAndTailPerDirectionAndSteps(direction, steps, tailMoves, coordinatesPerKnot);
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

        private static void MoveHeadAndTailPerDirectionAndSteps(
            char direction, 
            int steps,
            Dictionary<(int, int), int> tailMoves, 
            List<(int, int)> coordinatesPerKnot)
        {
            var knotIndex = 0;
            (int, int) newHeadCoordinates = (0, 0);
            switch (direction)
            {
                case 'R':
                    var headKnotCoordinates = coordinatesPerKnot[knotIndex];
                    var tailKnotCoordinates = coordinatesPerKnot[knotIndex + 1];
                    for (var i = headKnotCoordinates.Item1 + 1; i <= headKnotCoordinates.Item1 + steps; i++)
                    {
                        newHeadCoordinates = (i, headKnotCoordinates.Item2);
                        if (CanTailMove(tailKnotCoordinates, newHeadCoordinates, true))
                        {
                            coordinatesPerKnot[knotIndex + 1] = (i - 1, newHeadCoordinates.Item2);
                            UpdateTailLocation(tailMoves, coordinatesPerKnot[knotIndex + 1]);
                        }

                        coordinatesPerKnot[knotIndex] = newHeadCoordinates;
                    }


                    break;
                case 'L':
                    headKnotCoordinates = coordinatesPerKnot[knotIndex];
                    tailKnotCoordinates = coordinatesPerKnot[knotIndex + 1];
                    for (var i = headKnotCoordinates.Item1 - 1; i >= headKnotCoordinates.Item1 - steps; i--)
                    {
                        newHeadCoordinates = (i, headKnotCoordinates.Item2);
                        if (CanTailMove(tailKnotCoordinates, newHeadCoordinates, true))
                        {
                            coordinatesPerKnot[knotIndex + 1] = (i + 1, newHeadCoordinates.Item2);
                            UpdateTailLocation(tailMoves, coordinatesPerKnot[knotIndex + 1]);
                        }

                        coordinatesPerKnot[knotIndex] = newHeadCoordinates;
                    }

                    break;
                case 'U':
                    headKnotCoordinates = coordinatesPerKnot[knotIndex];
                    tailKnotCoordinates = coordinatesPerKnot[knotIndex + 1];
                    for (var j = headKnotCoordinates.Item2 + 1; j <= headKnotCoordinates.Item2 + steps; j++)
                    {
                        newHeadCoordinates = (headKnotCoordinates.Item1, j);
                        if (CanTailMove(tailKnotCoordinates, newHeadCoordinates, false))
                        {
                            coordinatesPerKnot[knotIndex + 1] = (newHeadCoordinates.Item1, j - 1);
                            UpdateTailLocation(tailMoves, coordinatesPerKnot[knotIndex + 1]);
                        }

                        coordinatesPerKnot[knotIndex] = newHeadCoordinates;
                    }

                    break;
                default:
                    headKnotCoordinates = coordinatesPerKnot[knotIndex];
                    tailKnotCoordinates = coordinatesPerKnot[knotIndex + 1];
                    for (var j = headKnotCoordinates.Item2 - 1; j >= headKnotCoordinates.Item2 - steps; j--)
                    {
                        newHeadCoordinates = (headKnotCoordinates.Item1, j);
                        if (CanTailMove(tailKnotCoordinates, newHeadCoordinates, false))
                        {
                            coordinatesPerKnot[knotIndex + 1] = (newHeadCoordinates.Item1, j + 1);
                            UpdateTailLocation(tailMoves, coordinatesPerKnot[knotIndex + 1]);
                        }

                        coordinatesPerKnot[knotIndex] = newHeadCoordinates;
                    }
                    
                    break;
            }

            return;
        }

        private static bool CanTailMove((int, int) tailCoordinates, (int, int) newHeadCoordinates, bool horizontalMove = true)
        {
            var allowed = horizontalMove 
                ? newHeadCoordinates.Item1 != tailCoordinates.Item1 
                : newHeadCoordinates.Item2 != tailCoordinates.Item2;


            return !IsDirectDiagonal(newHeadCoordinates, tailCoordinates) &&
                   !IsHeadAndTailOnSame(newHeadCoordinates, tailCoordinates) &&
                   allowed;
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

