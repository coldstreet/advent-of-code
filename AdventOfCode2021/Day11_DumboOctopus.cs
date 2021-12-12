namespace AdventOfCode2021
{
    public static class Day11_DumboOctopus
    {
        public static int CountFlashesAfterSteps(int[,] energyLevelGrid, int numberofSteps)
        {
            int totalFlashes = 0;          
            for (int steps = 0; steps < numberofSteps; steps++)
            {
                totalFlashes += PerformStep(energyLevelGrid);
            }

            return totalFlashes;
        }

        public static int FindStepWhenAllFlashingSynchronize(int[,] energyLevelGrid)
        {
            int step = 0;
            bool allFlashesSynchronized = true;
            for (int x = 0; x < energyLevelGrid.GetLength(0); x++)
            {
                for (int y = 0; y < energyLevelGrid.GetLength(1); y++)
                {
                    if (energyLevelGrid[x, y] != 0)
                    {
                        allFlashesSynchronized = false;
                        break;
                    }
                }
            }

            while(!allFlashesSynchronized)
            {
                PerformStep(energyLevelGrid);
                step++;
                allFlashesSynchronized = true;
                for (int x = 0; x < energyLevelGrid.GetLength(0); x++)
                {
                    for (int y = 0; y < energyLevelGrid.GetLength(1); y++)
                    {
                        if (energyLevelGrid[x, y] != 0)
                        {
                            allFlashesSynchronized = false;
                            break;
                        }
                    }
                }
            }

            return step;
        }

        private static int PerformStep(int[,] energyLevelGrid)
        {
            int totalFlashes = 0;
            int newFlashes = IncrementAllGridSquares(energyLevelGrid);
            if (newFlashes == 0)
            {
                return 0;
            }

            totalFlashes += newFlashes;

            // Now account for adjacent squares that flashed (i.e., just turned zero) and count any subsequent new flashes
            var flashSquaresProcessed = new bool[energyLevelGrid.GetLength(0), energyLevelGrid.GetLength(1)];
            newFlashes = IncrementAdjacentSquaresForNewFlashes(energyLevelGrid, flashSquaresProcessed);
            totalFlashes += newFlashes;
            while (newFlashes > 0)
            {
                newFlashes = IncrementAdjacentSquaresForNewFlashes(energyLevelGrid, flashSquaresProcessed);
                totalFlashes += newFlashes;
            }

            return totalFlashes;
        }

        private static void MarkFlashSquaresProcessed(int[,] energyLevelGrid, bool[,] flashSquaresProcessed)
        {
            for (int x = 0; x < energyLevelGrid.GetLength(0); x++)
            {
                for (int y = 0; y < energyLevelGrid.GetLength(1); y++)
                {
                    if (energyLevelGrid[x, y] == 0)
                    {
                        flashSquaresProcessed[x, y] = true;
                    }
                }
            }
        }

        private static int IncrementAllGridSquares(int[,] energyLevelGrid)
        {
            // Increment each grid by one; reset if at 9
            int newFlashes = 0;
            for (int x = 0; x < energyLevelGrid.GetLength(0); x++)
            {
                for (int y = 0; y < energyLevelGrid.GetLength(1); y++)
                {
                    if (energyLevelGrid[x, y] == 9)
                    {
                        energyLevelGrid[x, y] = 0;
                        newFlashes++;
                    }
                    else
                    {
                        energyLevelGrid[x, y]++;
                    }
                }
            }

            return newFlashes;
        }

        private static int IncrementAdjacentSquaresForNewFlashes(int[,] energyLevelGrid, bool[,] flashSquaresProcessed)
        {
            var copyOfEnergyLevelGrid = new int[energyLevelGrid.GetLength(0), energyLevelGrid.GetLength(1)];
            for (int x = 0; x < energyLevelGrid.GetLength(0); x++)
            {
                for (int y = 0; y < energyLevelGrid.GetLength(1); y++)
                {
                    copyOfEnergyLevelGrid[x, y] = energyLevelGrid[x, y];
                }
            }

            // Increment based on adjacent flashed (i.e., zeroed) squares
            int newFlashes = 0;
            int upperXIndex = energyLevelGrid.GetLength(0) - 1;
            int upperYIndex = energyLevelGrid.GetLength(1) - 1;
            for (int x = 0; x <= upperXIndex; x++)
            {
                for (int y = 0; y <= upperYIndex; y++)
                {
                    if (energyLevelGrid[x, y] == 0)
                    {
                        continue;
                    }

                    // Does this square have an adjacent 0?
                    int newIncrement = 0;

                    if (x - 1 >= 0 && y - 1 >= 0 && copyOfEnergyLevelGrid[x - 1, y - 1] == 0 && !flashSquaresProcessed[x - 1, y - 1])
                    {
                        newIncrement++;
                    }

                    if (x - 1 >= 0 && copyOfEnergyLevelGrid[x - 1, y] == 0 && !flashSquaresProcessed[x - 1, y])
                    {
                        newIncrement++;
                    }

                    if (x - 1 >= 0 && y + 1 <= upperYIndex && copyOfEnergyLevelGrid[x - 1, y + 1] == 0 && !flashSquaresProcessed[x - 1, y + 1])
                    {
                        newIncrement++;
                    }

                    if (y - 1 >= 0 && copyOfEnergyLevelGrid[x, y - 1] == 0 && !flashSquaresProcessed[x, y - 1])
                    {
                        newIncrement++;
                    }

                    if (y + 1 <= upperYIndex && copyOfEnergyLevelGrid[x, y + 1] == 0 && !flashSquaresProcessed[x, y + 1])
                    {
                        newIncrement++;
                    }

                    if (x + 1 <= upperXIndex && y - 1 >= 0 && copyOfEnergyLevelGrid[x + 1, y - 1] == 0 && !flashSquaresProcessed[x + 1, y - 1])
                    {
                        newIncrement++;
                    }

                    if (x + 1 <= upperXIndex && copyOfEnergyLevelGrid[x + 1, y] == 0 && !flashSquaresProcessed[x + 1, y])
                    {
                        newIncrement++;
                    }

                    if (x + 1 <= upperXIndex && y + 1 <= upperYIndex && copyOfEnergyLevelGrid[x + 1, y + 1] == 0 && !flashSquaresProcessed[x + 1, y + 1])
                    {
                        newIncrement++;
                    }

                    energyLevelGrid[x, y] = energyLevelGrid[x, y] + newIncrement > 9 ? 0 : energyLevelGrid[x, y] + newIncrement;
                    if (energyLevelGrid[x, y] == 0)
                    {
                        newFlashes++;
                    }
                }              
            }

            MarkFlashSquaresProcessed(copyOfEnergyLevelGrid, flashSquaresProcessed);

            return newFlashes;
        }
    }}
