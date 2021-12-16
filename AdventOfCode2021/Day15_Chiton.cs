namespace AdventOfCode2021
{
    public class Day15_Chiton
    {
        private int[,] _caveRiskGrid;
        private int _minPathSum = int.MaxValue;

        public long FindMinRiskLevelPath(int[,] caveRiskGrid)
        {
            _caveRiskGrid = caveRiskGrid;
            var lengthX = _caveRiskGrid.GetLength(1);
            var lengthY = _caveRiskGrid.GetLength(0);

            VisitSquare(0, 0, 0, new HashSet<(int, int)>());

            return _minPathSum - _caveRiskGrid[0, 0];
        }

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

        private bool IsValidSquareToVisit(int x, int y)
        {
            var lengthX = _caveRiskGrid.GetLength(1);
            var lengthY = _caveRiskGrid.GetLength(0);

            if (x == 0 && y == 0)
            {
                return false; // this is starting location
            }

            bool result = y >= 0 && y < lengthY && x >= 0 && x < lengthX;

            return result;
        }
    }
}

