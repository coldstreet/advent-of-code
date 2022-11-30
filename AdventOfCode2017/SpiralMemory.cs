namespace AdventOfCode2017
{
    public class SpiralMemory
    {
        public static int CountMovesV1(int fromNumber)
        {
            Dictionary<int, int> lookup = new Dictionary<int, int>
            {
                {1, 0},
                {2, 1},
                {3, 2},
                {4, 1},
                {5, 2},
                {6, 1},
                {7, 2},
                {8, 1},
                {9, 2}
            };

            int squareCount = 2;
            int itemsInSquareBoarder = 16;
            int itemCountForBoarder = 0;
            int movesPerSide = itemsInSquareBoarder / 4;
            int movesToCenter = movesPerSide - 1;
            bool decrease = true;
            for (int i = 10; i <= fromNumber; i++)
            {
                if (itemCountForBoarder >= itemsInSquareBoarder)
                {
                    squareCount++;
                    itemsInSquareBoarder += 8;
                    itemCountForBoarder = 0;
                    movesPerSide = itemsInSquareBoarder / 4;
                    movesToCenter = movesPerSide - 1;
                    decrease = true;
                }

                lookup.Add(i, movesToCenter);
                if (decrease)
                {
                    movesToCenter--;
                    if (movesToCenter == squareCount)
                    {
                        decrease = false;
                    }
                }
                else
                {
                    movesToCenter++;
                    if (movesToCenter == movesPerSide)
                    {
                        decrease = true;
                    }
                }

                itemCountForBoarder++;
            }

            return lookup[fromNumber];
        }

        public static Dictionary<int, int> BuildSumBasedSpiralLookup(int maxSquareCount)
        {
            Dictionary<int, int> lookup = new Dictionary<int, int>
            {
                {1, 1},
                {2, 1},
                {3, 2},
                {4, 4},
                {5, 5},
                {6, 10},
                {7, 11},
                {8, 23},
                {9, 25}
            };

            int itemsInSquareBoarder = 16;
            int itemBoarderCount = 1;
            int movesPerSide = itemsInSquareBoarder / 4;
            int perSideIndex = 1;
            int backspaceIndex = 6;
            for (int number = 10; number <= maxSquareCount; number++)
            {
                int sum = 0;
                if (itemBoarderCount > itemsInSquareBoarder)
                {
                    itemsInSquareBoarder += 8;
                    itemBoarderCount = 1;
                    movesPerSide = itemsInSquareBoarder / 4;
                    sum = 0;
                    perSideIndex = 1;
                }

                if (perSideIndex == 1)
                {
                    backspaceIndex += 2;
                }

                Console.WriteLine($"{number}");
                Console.WriteLine($"itemBoarderCount is {itemBoarderCount}");
                Console.WriteLine($"perSideIndex is {perSideIndex}");
                Console.WriteLine($"backspaceIndex is {backspaceIndex}");
                Console.WriteLine($"itemsInSquareBoarder is {itemsInSquareBoarder}");
                Console.WriteLine($"movesPerSide is {movesPerSide}");
                

                // handle start
                if (itemBoarderCount == 1)
                {
                    Console.WriteLine($"{number}: Start");
                    sum += lookup[number - 1];
                    sum += lookup[number - backspaceIndex];
                }

                // handle one after start and one after corner
                else if (itemBoarderCount == 2 || perSideIndex == 1)
                {
                    Console.WriteLine($"{number}: One after start or one after corner");
                    sum += lookup[number - 1];
                    sum += lookup[number - 2];
                    sum += lookup[number - (backspaceIndex + 1)];
                    sum += lookup[number - backspaceIndex];
                }

                // handle last corner
                else if (itemBoarderCount == itemsInSquareBoarder)
                {
                    Console.WriteLine($"{number}: last corner");
                    sum += lookup[number - 1];
                    sum += lookup[number - (backspaceIndex + 2)];
                    sum += lookup[number - (backspaceIndex + 1)];
                }

                // handle one before corner (first 3) 
                else if (perSideIndex == (movesPerSide - 1) && itemBoarderCount < itemsInSquareBoarder - 1)
                {
                    Console.WriteLine($"{number}: One before corner");
                    sum += lookup[number - 1];
                    sum += lookup[number - (backspaceIndex + 2)];
                    sum += lookup[number - (backspaceIndex + 1)];
                }

                // handle first 3 corners
                else if (perSideIndex == 0 && itemBoarderCount != itemsInSquareBoarder - 1)
                {
                    Console.WriteLine($"{number}: Corner");
                    sum += lookup[number - 1];
                    sum += lookup[number - (backspaceIndex + 2)];
                }

                // handler everything else and one before last corner
                else  
                {
                    Console.WriteLine($"{number}: Everything else or one before last corner");
                    sum += lookup[number - 1];
                    sum += lookup[number - (backspaceIndex + 2)];
                    sum += lookup[number - (backspaceIndex + 1)];
                    sum += lookup[number - backspaceIndex];
                }

                lookup.Add(number, sum);
                
                itemBoarderCount++;
                if (perSideIndex == movesPerSide - 1)
                {
                    perSideIndex = 0;
                }
                else
                {
                    perSideIndex++;
                }

                Console.WriteLine("-----------------------------------------------");
            }

            return lookup;
        }
    }
}
