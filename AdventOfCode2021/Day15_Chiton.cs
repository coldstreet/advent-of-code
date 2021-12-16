namespace AdventOfCode2021
{
    public class Day15_Chiton
    {
        private int[,] _caveRiskGrid = new int[0,0];
        private int _minPathSum = int.MaxValue; // old

        private HashSet<(int, int)> _visited = new HashSet<(int, int)>();
        private int[,] _weightedRiskGrid = new int[0, 0];

        public long FindMinRiskLevelPath(int[,] caveRiskGrid)
        {
            _caveRiskGrid = caveRiskGrid;
            var lengthX = _caveRiskGrid.GetLength(1);
            var lengthY = _caveRiskGrid.GetLength(0);

            //VisitSquare(0, 0, 0, new HashSet<(int, int)>());

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
            for(int y = 0; y < lengthY; y++)
            {
                for(int x = 0; x < lengthX; x++)
                {
                    // right
                    if (IsValidSquareToVisit(x + 1, y))
                    {
                        UpdateWeightedRiskGrid(x + 1, y, _weightedRiskGrid[x, y]);
                    }

                    // down
                    if (IsValidSquareToVisit(x, y + 1))
                    {
                        UpdateWeightedRiskGrid(x, y + 1, _weightedRiskGrid[x, y]);
                    }

                    // up
                    if (IsValidSquareToVisit(x, y - 1))
                    {
                        UpdateWeightedRiskGrid(x, y - 1, _weightedRiskGrid[x, y]);
                    }

                    // left
                    if (IsValidSquareToVisit(x - 1, y))
                    {
                        UpdateWeightedRiskGrid(x - 1, y, _weightedRiskGrid[x, y]);
                    }

                    _visited.Add((x, y));
                }
            }
        }

        private void UpdateWeightedRiskGrid(int x, int y, int weightedRiskLevel)
        {
            if (weightedRiskLevel + _caveRiskGrid[x, y] < _weightedRiskGrid[x, y])
            {
                _weightedRiskGrid[x, y] = weightedRiskLevel + _caveRiskGrid[x, y];
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

        // inefficient, first solution
        private void VisitSquare(int x, int y, int currentRiskAccumulation, HashSet<(int, int)> visited)
        {
            if (visited.Contains((x, y)))
            {
                return;
            }

            visited.Add((x, y));

            var updatedRiskLevel = currentRiskAccumulation + _caveRiskGrid[x, y];
            if (updatedRiskLevel >= _minPathSum)
            {
                // abandon path
                return;
            }

            if (x == _caveRiskGrid.GetLength(1) - 1 && y == _caveRiskGrid.GetLength(0) - 1)
            {
                // reached end square
                if (updatedRiskLevel < _minPathSum)
                {
                    _minPathSum = updatedRiskLevel;
                }

                return;
            }

            // right
            if (IsValidSquareToVisit(x + 1, y))
            {
                VisitSquare(x + 1, y, updatedRiskLevel, new HashSet<(int, int)>(visited));
            }

            // down
            if (IsValidSquareToVisit(x, y + 1))
            {
                VisitSquare(x, y + 1, updatedRiskLevel, new HashSet<(int, int)>(visited));
            }

            // up
            if (IsValidSquareToVisit(x, y - 1))
            {
                VisitSquare(x, y - 1, updatedRiskLevel, new HashSet<(int, int)>(visited));
            }

            // left
            if (IsValidSquareToVisit(x - 1, y))
            {
                VisitSquare(x - 1, y, updatedRiskLevel, new HashSet<(int, int)>(visited));
            }

            return;
        }
    }
}

