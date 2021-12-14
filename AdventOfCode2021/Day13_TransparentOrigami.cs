using System.Diagnostics;
using System.Text;

namespace AdventOfCode2021
{
    public static class Day13_TransparentOrigami
    {
        public static int FoldUpAndCountDots(string[] input, bool doJustOneFold = false)
        {
            var separatorIndex = Array.IndexOf(input, "");

            var dotMap = input
                .Take(separatorIndex)
                .Select(l => l.Split(',').Select(int.Parse).ToArray());

            var maxIndexX = dotMap.Select(x => x[0]).Max();
            var maxIndexY = dotMap.Select(x => x[1]).Max();

            var grid = new int[maxIndexX + 1, maxIndexY + 1];
            foreach (var dot in dotMap)
            {
                grid[dot[0], dot[1]] = 1;
            }

            // get fold instructions
            var foldInfo = input.TakeLast(input.Length - (separatorIndex + 1)).ToArray();
            
            if (doJustOneFold)
            {
                foldInfo = foldInfo.Take(1).ToArray();
            }
            
            int count = 0;
            for (int i = 0; i < foldInfo.Length; i++)
            {
                string foldInstruction = foldInfo[i];
                var newGridLengthX = maxIndexX + 1;
                var newGridLengthY = maxIndexY + 1;
                var startIndexX = 0;
                var startIndexY = 0;
                bool foldUp = false;
                if (foldInstruction.Contains("x"))
                {
                    newGridLengthX = int.Parse(foldInstruction.Remove(0, 13));
                    startIndexX = newGridLengthX - (maxIndexX - newGridLengthX);
                }
                else
                {
                    newGridLengthY = int.Parse(foldInstruction.Remove(0, 13));
                    startIndexY = newGridLengthY - (maxIndexY - newGridLengthY);
                    foldUp = true;
                }

                var newFoldedUpGrid = new int[newGridLengthX, newGridLengthY];

                var incrementY = 0;
                for (int y = startIndexY; y < newGridLengthY; y++)
                {
                    var incrementX = 0;
                    for (int x = startIndexX; x < newGridLengthX; x++)
                    {
                        if (foldUp)
                        {
                            newFoldedUpGrid[x, y] = grid[x, y] == 1 || grid[x, maxIndexY - incrementY] == 1 ? 1 : 0;
                        }
                        else
                        {
                            newFoldedUpGrid[x, y] = grid[x, y] == 1 || grid[maxIndexX - incrementX, y] == 1 ? 1 : 0;
                        }
                        incrementX++;
                    }
                    incrementY++;
                }

                // count dots after fold and optionally print dots
                bool print = !doJustOneFold && i == foldInfo.Length - 1;
                count = CountAndPrint(newFoldedUpGrid, print);

                maxIndexX = newGridLengthX - 1;
                maxIndexY = newGridLengthY - 1;
                grid = newFoldedUpGrid;
            }

            return count;
        }

        private static int CountAndPrint(int[,] newFoldedUpGrid, bool print)
        {
            int count = 0;
            for (int y = 0; y < newFoldedUpGrid.GetLength(1); y++)
            {
                var sb = new StringBuilder();
                for (int x = 0; x < newFoldedUpGrid.GetLength(0); x++)
                {
                    if (print)
                    {
                        sb.Append(newFoldedUpGrid[x, y] == 1 ? "#" : " ");
                    }

                    if (newFoldedUpGrid[x, y] == 1)
                    {
                        count++;
                    }
                }

                if (print)
                {
                    Debug.WriteLine(sb);
                }
            }

            return count;
        }
    }
}

