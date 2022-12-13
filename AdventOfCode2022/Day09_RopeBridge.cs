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
            var head = coordinatesPerKnot[knotIndex];
            var tail = coordinatesPerKnot[knotIndex + 1];
            switch (direction)
            {
                case 'R':
                    MoveHeadTailRight(steps, tailMoves, coordinatesPerKnot, head, tail, knotIndex);
                    break;
                case 'L':
                    MoveHeadTailLeft(steps, tailMoves, coordinatesPerKnot, head, tail, knotIndex);
                    break;
                case 'U':
                    MoveHeadTailUp(steps, tailMoves, coordinatesPerKnot, head, tail, knotIndex);
                    break;
                default:
                    MoveHeadTailDown(steps, tailMoves, coordinatesPerKnot, head, tail, knotIndex);
                    break;
            }
        }

        private static void MoveHeadTailDown(int steps, Dictionary<(int, int), int> tailMoves, List<(int, int)> coordinatesPerKnot, (int, int) head,
            (int, int) tail, int knotIndex)
        {
            for (var j = head.Item2 - 1; j >= head.Item2 - steps; j--)
            {
                var newHead = (head.Item1, j);
                if (CanTailMove(tail, newHead, false))
                {
                    coordinatesPerKnot[knotIndex + 1] = (newHead.Item1, j + 1);
                    UpdateTailLocation(tailMoves, coordinatesPerKnot[knotIndex + 1]);
                }

                coordinatesPerKnot[knotIndex] = newHead;
            }
        }

        private static void MoveHeadTailUp(int steps, Dictionary<(int, int), int> tailMoves, List<(int, int)> coordinatesPerKnot, (int, int) head,
            (int, int) tail, int knotIndex)
        {
            for (var j = head.Item2 + 1; j <= head.Item2 + steps; j++)
            {
                var newHead = (head.Item1, j);
                if (CanTailMove(tail, newHead, false))
                {
                    coordinatesPerKnot[knotIndex + 1] = (newHead.Item1, j - 1);
                    UpdateTailLocation(tailMoves, coordinatesPerKnot[knotIndex + 1]);
                }

                coordinatesPerKnot[knotIndex] = newHead;
            }
        }

        private static void MoveHeadTailLeft(int steps, Dictionary<(int, int), int> tailMoves, List<(int, int)> coordinatesPerKnot, (int, int) head,
            (int, int) tail, int knotIndex)
        {
            for (var i = head.Item1 - 1; i >= head.Item1 - steps; i--)
            {
                var newHead = (i, head.Item2);
                if (CanTailMove(tail, newHead, true))
                {
                    coordinatesPerKnot[knotIndex + 1] = (i + 1, newHead.Item2);
                    UpdateTailLocation(tailMoves, coordinatesPerKnot[knotIndex + 1]);
                }

                coordinatesPerKnot[knotIndex] = newHead;
            }
        }

        private static void MoveHeadTailRight(int steps, Dictionary<(int, int), int> tailMoves, List<(int, int)> coordinatesPerKnot, (int, int) head,
            (int, int) tail, int knotIndex)
        {
            for (var i = head.Item1 + 1; i <= head.Item1 + steps; i++)
            {
                var newHead = (i, head.Item2);
                if (CanTailMove(tail, newHead, true))
                {
                    coordinatesPerKnot[knotIndex + 1] = (i - 1, newHead.Item2);
                    UpdateTailLocation(tailMoves, coordinatesPerKnot[knotIndex + 1]);
                }

                coordinatesPerKnot[knotIndex] = newHead;
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

