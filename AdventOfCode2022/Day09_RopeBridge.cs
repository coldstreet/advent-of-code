namespace AdventOfCode2022
{
    public static class Day09_RopeBridge
    {
        public static long CountTailVisitLocations(string[] input, int knotsInRope = 2, int tailToTrack = 1)
        {
            var instructions = ParseInput(input);

            var tailMoves = new Dictionary<(int, int), int>();
            var coordinatesPerKnot = new List<(int, int)>();
            var knotsInPlay = 2;
            while (knotsInRope > 0)
            {
                coordinatesPerKnot.Add((0,0));
                knotsInRope--;
            }

            foreach (var instruction in instructions)
            {
                var direction = instruction.Item1;
                var steps = instruction.Item2;

                knotsInPlay = MoveHeadAndTailPerDirectionAndSteps(direction, steps, tailToTrack, knotsInPlay, tailMoves, coordinatesPerKnot);
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

        private static int MoveHeadAndTailPerDirectionAndSteps(
            char direction, 
            int steps,
            int tailToTrack,
            int knotsInPlay,
            Dictionary<(int, int), int> tailMoves, 
            List<(int, int)> coordinatesPerKnot)
        {
            (int, int) newHeadCoordinates = (0, 0);
            switch (direction)
            {
                case 'R':
                    for (var knotIndex = 0; knotIndex < knotsInPlay - 1; knotIndex++)
                    {
                        var headKnotCoordinates = coordinatesPerKnot[knotIndex];
                        var tailKnotCoordinates = coordinatesPerKnot[knotIndex + 1];
                        for (var i = headKnotCoordinates.Item1 + 1; i <= headKnotCoordinates.Item1 + steps; i++)
                        {
                            //for (int headKnotIndex = 1; headKnotIndex < knotsInPlay; headKnotIndex++)
                            //{
                            //    knotsInPlay = knotsInPlay < knotsInRope ? knotsInPlay + 1 : knotsInRope;
                            //}
                            newHeadCoordinates = (i, headKnotCoordinates.Item2);
                            if (CanTailMove(tailKnotCoordinates, newHeadCoordinates, true))
                            {
                                coordinatesPerKnot[knotIndex + 1] = (i - 1, newHeadCoordinates.Item2);
                                if (knotIndex + 1 == tailToTrack)
                                {
                                    UpdateTailLocation(tailMoves, coordinatesPerKnot[knotIndex + 1]);
                                    //if (knotsInPlay < knotsInRope)
                                    //{
                                    //    knotsInPlay++;
                                    //}
                                }
                            }

                            coordinatesPerKnot[knotIndex] = newHeadCoordinates;
                        }
                    }

                    break;
                case 'L':
                    for (var knotIndex = 0; knotIndex < coordinatesPerKnot.Count - 1; knotIndex++)
                    {
                        var headKnotCoordinates = coordinatesPerKnot[knotIndex];
                        var tailKnotCoordinates = coordinatesPerKnot[knotIndex + 1];
                        for (var i = headKnotCoordinates.Item1 - 1; i >= headKnotCoordinates.Item1 - steps; i--)
                        {
                            newHeadCoordinates = (i, headKnotCoordinates.Item2);
                            if (CanTailMove(tailKnotCoordinates, newHeadCoordinates, true))
                            {
                                coordinatesPerKnot[knotIndex + 1] = (i + 1, newHeadCoordinates.Item2);
                                if (knotIndex + 1 == tailToTrack)
                                {
                                    UpdateTailLocation(tailMoves, coordinatesPerKnot[knotIndex + 1]);
                                }
                            }
                            //else
                            //{
                            //    break;
                            //}
                            coordinatesPerKnot[knotIndex] = newHeadCoordinates;
                        }

                        
                    }

                    break;
                case 'U':
                    for (var knotIndex = 0; knotIndex < coordinatesPerKnot.Count - 1; knotIndex++)
                    {
                        var headKnotCoordinates = coordinatesPerKnot[knotIndex];
                        var tailKnotCoordinates = coordinatesPerKnot[knotIndex + 1];
                        for (var j = headKnotCoordinates.Item2 + 1; j <= headKnotCoordinates.Item2 + steps; j++)
                        {
                            newHeadCoordinates = (headKnotCoordinates.Item1, j);
                            if (CanTailMove(tailKnotCoordinates, newHeadCoordinates, false))
                            {
                                coordinatesPerKnot[knotIndex + 1] = (newHeadCoordinates.Item1, j - 1);
                                if (knotIndex + 1 == tailToTrack)
                                {
                                    UpdateTailLocation(tailMoves, coordinatesPerKnot[knotIndex + 1]);
                                }
                            }
                            //else
                            //{
                            //    break;
                            //}
                            coordinatesPerKnot[knotIndex] = newHeadCoordinates;
                        }

                        
                    }

                    break;
                default:
                    for (var knotIndex = 0; knotIndex < coordinatesPerKnot.Count - 1; knotIndex++)
                    {
                        var headKnotCoordinates = coordinatesPerKnot[knotIndex];
                        var tailKnotCoordinates = coordinatesPerKnot[knotIndex + 1];
                        for (var j = headKnotCoordinates.Item2 - 1; j >= headKnotCoordinates.Item2 - steps; j--)
                        {
                            newHeadCoordinates = (headKnotCoordinates.Item1, j);
                            if (CanTailMove(tailKnotCoordinates, newHeadCoordinates, false))
                            {
                                coordinatesPerKnot[knotIndex + 1] = (newHeadCoordinates.Item1, j + 1);
                                if (knotIndex + 1 == tailToTrack)
                                {
                                    UpdateTailLocation(tailMoves, coordinatesPerKnot[knotIndex + 1]);
                                }
                            }
                            //else
                            //{
                            //    break;
                            //}
                            coordinatesPerKnot[knotIndex] = newHeadCoordinates;
                        }

                        
                    }

                    break;
            }

            return knotsInPlay;
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

