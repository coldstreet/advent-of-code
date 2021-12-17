﻿namespace AdventOfCode2021
{
    public class Day15_Chiton
    {
        private int[,] _caveRiskGrid = new int[0,0];

        private HashSet<(int, int)> _visited = new HashSet<(int, int)>();
        private int[,] _weightedRiskGrid = new int[0, 0];

        public long FindMinRiskLevelPath(int[,] caveRiskGrid, bool enlargeInput = false)
        {
            _caveRiskGrid = caveRiskGrid;
            if (enlargeInput)
            {
                EnlargeInput(caveRiskGrid);
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

        // old inefficient, first solution
        //private void VisitSquare(int x, int y, int currentRiskAccumulation, HashSet<(int, int)> visited)
        //{
        //    if (visited.Contains((x, y)))
        //    {
        //        return;
        //    }

        //    visited.Add((x, y));

        //    var updatedRiskLevel = currentRiskAccumulation + _caveRiskGrid[x, y];
        //    if (updatedRiskLevel >= _minPathSum)
        //    {
        //        // abandon path
        //        return;
        //    }

        //    if (x == _caveRiskGrid.GetLength(1) - 1 && y == _caveRiskGrid.GetLength(0) - 1)
        //    {
        //        // reached end square
        //        if (updatedRiskLevel < _minPathSum)
        //        {
        //            _minPathSum = updatedRiskLevel;
        //        }

        //        return;
        //    }

        //    // right
        //    if (IsValidSquareToVisit(x + 1, y))
        //    {
        //        VisitSquare(x + 1, y, updatedRiskLevel, new HashSet<(int, int)>(visited));
        //    }

        //    // down
        //    if (IsValidSquareToVisit(x, y + 1))
        //    {
        //        VisitSquare(x, y + 1, updatedRiskLevel, new HashSet<(int, int)>(visited));
        //    }

        //    // up
        //    if (IsValidSquareToVisit(x, y - 1))
        //    {
        //        VisitSquare(x, y - 1, updatedRiskLevel, new HashSet<(int, int)>(visited));
        //    }

        //    // left
        //    if (IsValidSquareToVisit(x - 1, y))
        //    {
        //        VisitSquare(x - 1, y, updatedRiskLevel, new HashSet<(int, int)>(visited));
        //    }

        //    return;
        //}
    }
}

