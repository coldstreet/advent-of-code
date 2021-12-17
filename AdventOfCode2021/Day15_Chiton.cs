namespace AdventOfCode2021
{
    public class Day15_Chiton
    {
        // todo - this can be a static class as these variables don't need to be global
        private int[,] _caveRiskGrid = new int[0,0];
        private HashSet<(int, int)> _visited = new HashSet<(int, int)>();
        private int[,] _weightedRiskGrid = new int[0, 0];

        public long FindMinRiskLevelPath(int[,] startingCaveRiskGrid, bool enlargeInput = false)
        {
            _caveRiskGrid = startingCaveRiskGrid;
            if (enlargeInput)
            {
                EnlargeInput(startingCaveRiskGrid);
            }

            var lengthX = _caveRiskGrid.GetLength(1);
            var lengthY = _caveRiskGrid.GetLength(0);

            // initialize weightedRiskGrid
            _weightedRiskGrid = new int[lengthX, lengthY];
            for (int y = 0; y < lengthY; y++)
            {
                for (int x = 0; x < lengthX; x++)
                {
                    _weightedRiskGrid[x, y] = int.MaxValue;
                }
            }
            _weightedRiskGrid[0, 0] = 0;
             
            FindLeastEnergyPath();

            return _weightedRiskGrid[lengthX - 1, lengthY - 1];
        }

        private void FindLeastEnergyPath()
        {
            var lengthX = _caveRiskGrid.GetLength(1);
            var lengthY = _caveRiskGrid.GetLength(0);

            var priorityQueue = new PriorityQueue<(int, int), int>();
            priorityQueue.Enqueue((0, 0), 0);

            while (true)
            {
                (int x, int y) = priorityQueue.Dequeue();
                if (x == lengthX - 1 && y == lengthY - 1)
                {
                    break;
                }

                // right
                if (IsValidSquareToVisit(x + 1, y))
                {
                    UpdateWeightedRiskGridAndQueue(x + 1, y, _weightedRiskGrid[x, y], priorityQueue);
                }

                // down
                if (IsValidSquareToVisit(x, y + 1))
                {
                    UpdateWeightedRiskGridAndQueue(x, y + 1, _weightedRiskGrid[x, y], priorityQueue);
                }

                // up
                if (IsValidSquareToVisit(x, y - 1))
                {
                    UpdateWeightedRiskGridAndQueue(x, y - 1, _weightedRiskGrid[x, y], priorityQueue);
                }

                // left
                if (IsValidSquareToVisit(x - 1, y))
                {
                    UpdateWeightedRiskGridAndQueue(x - 1, y, _weightedRiskGrid[x, y], priorityQueue);
                }

                _visited.Add((x, y));
            }
        }

        private void UpdateWeightedRiskGridAndQueue(int x, int y, int weightedRiskLevel, PriorityQueue<(int, int), int> priorityQueue)
        {
            if (weightedRiskLevel + _caveRiskGrid[x, y] < _weightedRiskGrid[x, y])
            {
                _weightedRiskGrid[x, y] = weightedRiskLevel + _caveRiskGrid[x, y];
                priorityQueue.Enqueue((x, y), _weightedRiskGrid[x, y]);
            }
        }

        private bool IsValidSquareToVisit(int x, int y)
        {
            if (_visited.Contains((x, y)))
            {
                return false;
            }

            var lengthX = _caveRiskGrid.GetLength(1);
            var lengthY = _caveRiskGrid.GetLength(0);
            return y >= 0 && y < lengthY && x >= 0 && x < lengthX;
        }

        private void EnlargeInput(int[,] caveRiskGrid)
        {
            // orginal grid
            var lengthX = caveRiskGrid.GetLength(1);
            var lengthY = caveRiskGrid.GetLength(0);

            // now build expand grid (orginal times 5)
            _caveRiskGrid = new int[lengthX * 5, lengthY * 5];
            for (int y = 0; y < lengthY * 5; y++)
            {
                for (int x = 0; x < lengthX * 5; x++)
                {
                    if (x < lengthX && y < lengthY)
                    {
                        _caveRiskGrid[x, y] = caveRiskGrid[x, y];
                        continue;
                    }

                    var overallY = (y / lengthY);
                    var overallX = (x / lengthX);

                    var originalX = x < lengthX ? x : x - lengthX * overallX;
                    var originalY = y < lengthY ? y : y - lengthY * overallY;
                    var originalValue = caveRiskGrid[originalX, originalY];
                    
                    var newValue = 0;
                    if (originalValue == 9)
                    {
                        newValue = overallX + overallY;
                    }
                    else if (originalValue + overallX + overallY > 9)
                    {
                        newValue = originalValue + overallX + overallY - 9;
                    }
                    else
                    {
                        newValue = originalValue + overallX + overallY;
                    }

                    _caveRiskGrid[x, y] = newValue;
                }
            }
        }
    }
}

