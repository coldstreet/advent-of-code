namespace AdventOfCode2022
{
    public static class Day09_RopeBridge
    {
        public static long CountTailVisitLocations(string[] input, bool part2 = false)
        {
            var instructions = ParseInput(input);

            var tailMoves = new Dictionary<(int, int), int>();
            var coordinatesPerKnot = new List<(int, int)> { (0,0), (0,0) };
            var knotsVisible = new List<bool> { true, false };
            if (part2)
            {
                for (int i = 0; i < 8; i++)
                {
                    coordinatesPerKnot.Add((0,0));
                    knotsVisible.Add(false);
                }
            }

            foreach (var instruction in instructions)
            {
                var direction = instruction.Item1;
                var steps = instruction.Item2;

                MoveHeadAndTailPerDirectionAndSteps(direction, steps, tailMoves, coordinatesPerKnot, knotsVisible);
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
            List<(int, int)> coordinatesPerKnot,
            List<bool> knotsVisible)
        {
            var head = coordinatesPerKnot[0];
            switch (direction)
            {
                case 'R':
                    for (var x = head.Item1 + 1; x <= head.Item1 + steps; x++)
                    {
                        MoveRopeRight(x, 0, tailMoves, coordinatesPerKnot, knotsVisible);
                    }
                    break;
                case 'L':
                    for (var x = head.Item1 - 1; x >= head.Item1 - steps; x--)
                    {
                        MoveRopeLeft(x, 0, tailMoves, coordinatesPerKnot, knotsVisible);
                    }
                    break;
                case 'U':
                    for (var y = head.Item2 + 1; y <= head.Item2 + steps; y++)
                    {
                        MoveRopeUp(y, 0, tailMoves, coordinatesPerKnot, knotsVisible);
                    }
                    break;
                default:
                    for (var y = head.Item2 - 1; y >= head.Item2 - steps; y--)
                    {
                        MoveRopeDown(y, 0, tailMoves, coordinatesPerKnot, knotsVisible);
                    }
                    break;
            }
        }

        private static void MoveRopeRight(int stepIndex, int knotIndex, Dictionary<(int, int), int> tailMoves, List<(int, int)> coordinatesPerKnot, List<bool> knotsVisible)
        {
            var head = coordinatesPerKnot[knotIndex];
            var tail = coordinatesPerKnot[knotIndex + 1];
            var newHead = (stepIndex, head.Item2);
            var moveTail = CanTailMove(tail, newHead, true);
            if (moveTail)
            {
                coordinatesPerKnot[knotIndex + 1] = (stepIndex - 1, newHead.Item2);
                if (knotIndex + 1 == coordinatesPerKnot.Count - 1)
                {
                    UpdateTailLocation(tailMoves, coordinatesPerKnot[knotIndex + 1]);
                }
            }

            coordinatesPerKnot[knotIndex] = newHead;
            if (coordinatesPerKnot[knotIndex + 1] != (0, 0) && moveTail && knotIndex + 2 < coordinatesPerKnot.Count)
            {
                knotsVisible[knotIndex + 2] = true;
            }

            knotIndex++;
            if (moveTail && knotIndex + 1 < coordinatesPerKnot.Count && knotsVisible[knotIndex + 1])
            {
                // use tail as "head"
                MoveRopeRight(coordinatesPerKnot[knotIndex].Item1, knotIndex, tailMoves, coordinatesPerKnot, knotsVisible);
            }
        }

        private static void MoveRopeLeft(int stepIndex, int knotIndex, Dictionary<(int, int), int> tailMoves, List<(int, int)> coordinatesPerKnot, List<bool> knotsVisible)
        {
            var head = coordinatesPerKnot[knotIndex];
            var tail = coordinatesPerKnot[knotIndex + 1];

            var newHead = (stepIndex, head.Item2);
            var moveTail = CanTailMove(tail, newHead, true);
            if (moveTail)
            {
                coordinatesPerKnot[knotIndex + 1] = (stepIndex + 1, newHead.Item2);
                if (knotIndex + 1 == coordinatesPerKnot.Count - 1)
                {
                    UpdateTailLocation(tailMoves, coordinatesPerKnot[knotIndex + 1]);
                }
            }

            coordinatesPerKnot[knotIndex] = newHead;
            if (coordinatesPerKnot[knotIndex + 1] != (0, 0) && moveTail && knotIndex + 2 < coordinatesPerKnot.Count)
            {
                knotsVisible[knotIndex + 2] = true;
            }

            coordinatesPerKnot[knotIndex] = newHead;
            knotIndex++;
            if (moveTail && knotIndex + 1 < coordinatesPerKnot.Count && knotsVisible[knotIndex + 1])
            {
                // use tail as "head"
                MoveRopeLeft(coordinatesPerKnot[knotIndex].Item1, knotIndex, tailMoves, coordinatesPerKnot, knotsVisible);
            }
        }

        private static void MoveRopeUp(int stepIndex, int knotIndex, Dictionary<(int, int), int> tailMoves, List<(int, int)> coordinatesPerKnot, List<bool> knotsVisible)
        {
            var head = coordinatesPerKnot[knotIndex];
            var tail = coordinatesPerKnot[knotIndex + 1];

            var newHead = (head.Item1, stepIndex);
            var moveTail = CanTailMove(tail, newHead, false);
            if (moveTail)
            {
                coordinatesPerKnot[knotIndex + 1] = (newHead.Item1, stepIndex - 1);
                if (knotIndex + 1 == coordinatesPerKnot.Count - 1)
                {
                    UpdateTailLocation(tailMoves, coordinatesPerKnot[knotIndex + 1]);
                }
            }

            coordinatesPerKnot[knotIndex] = newHead;
            if (coordinatesPerKnot[knotIndex + 1] != (0, 0) && moveTail && knotIndex + 2 < coordinatesPerKnot.Count)
            {
                knotsVisible[knotIndex + 2] = true;
            }

            coordinatesPerKnot[knotIndex] = newHead;
            knotIndex++;
            if (moveTail && knotIndex + 1 < coordinatesPerKnot.Count && knotsVisible[knotIndex + 1])
            {
                // use tail as "head"
                MoveRopeUp(coordinatesPerKnot[knotIndex].Item2, knotIndex, tailMoves, coordinatesPerKnot, knotsVisible);
            }
        }

        private static void MoveRopeDown(int stepIndex, int knotIndex, Dictionary<(int, int), int> tailMoves, List<(int, int)> coordinatesPerKnot, List<bool> knotsVisible)
        {
            var head = coordinatesPerKnot[knotIndex];
            var tail = coordinatesPerKnot[knotIndex + 1];

            var newHead = (head.Item1, stepIndex);
            var moveTail = CanTailMove(tail, newHead, false);
            if (moveTail)
            {
                coordinatesPerKnot[knotIndex + 1] = (newHead.Item1, stepIndex + 1);
                if (knotIndex + 1 == coordinatesPerKnot.Count - 1)
                {
                    UpdateTailLocation(tailMoves, coordinatesPerKnot[knotIndex + 1]);
                }
            }

            coordinatesPerKnot[knotIndex] = newHead;
            if (coordinatesPerKnot[knotIndex + 1] != (0, 0) && moveTail && knotIndex + 2 < coordinatesPerKnot.Count)
            {
                knotsVisible[knotIndex + 2] = true;
            }

            coordinatesPerKnot[knotIndex] = newHead;
            knotIndex++;
            if (moveTail && knotIndex + 1 < coordinatesPerKnot.Count && knotsVisible[knotIndex + 1])
            {
                // use tail as "head"
                MoveRopeUp(coordinatesPerKnot[knotIndex].Item2, knotIndex, tailMoves, coordinatesPerKnot, knotsVisible);
            }
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

