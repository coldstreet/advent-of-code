namespace AdventOfCode2021
{
    public static class Day15_Chiton
    {
        public class Square
        {
            public int RiskLevel { get; }
            public int[,] ConnectedSquaresRiskLevel { get;}
            public bool IsEndSquare { get; }

            public Square(int riskLevel, bool isEndSquare, int[,] connectedSquares)
            {
                RiskLevel = riskLevel;
                IsEndSquare = isEndSquare;
                ConnectedSquaresRiskLevel = connectedSquares;
            }
        }

        private static IDictionary<(int, int), Square> _squareDictionary;
        private static int[,] _caveRiskGrid;
        private static int _minPathSum = int.MaxValue;

        public static long FindMinRiskLevelPath(int[,] caveRiskGrid)
        {
            _caveRiskGrid = caveRiskGrid;
            var lengthX = _caveRiskGrid.GetLength(1);
            var lengthY = _caveRiskGrid.GetLength(0);

            // build details about squares (i.e., connected squares excluding connecting to the start square and building a square detail for the end)
            _squareDictionary = new Dictionary<(int, int), Square>(lengthX*lengthY);
            for (int y = 0; y < lengthY; y++)
            {
                for (int x = 0; x < lengthX; x++)
                {
                    if (x == lengthX - 1 && y == lengthY - 1) // end
                    {
                        _squareDictionary.Add((x, y), new Square(1, true, new int[3, 3]));
                        break;
                    }

                    if (x == 0 && y == 0) // start
                    {
                        _squareDictionary.Add((x, y), new Square(1, false, new int[3, 3]));
                        break;
                    }

                    _squareDictionary.Add((x, y), new Square(_caveRiskGrid[x, y], false, new int[3, 3]));
                }
            }

            var visitedGrid = new bool[lengthX, lengthY];
            VisitSquare(0, 0, 0, visitedGrid);

            return _minPathSum - _caveRiskGrid[0, 0];
        }

        private static void VisitSquare(int x, int y, int currentRiskAccumulation, bool[,] visitedGrid)
        {
            if (visitedGrid[x, y])
            {
                return;
            }

            visitedGrid[x, y] = true;

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

            // up
            if (IsValidSquareToVisit(x, y - 1))
            {
                VisitSquare(x, y - 1, updatedRiskLevel, visitedGrid.Clone() as bool[,]);
            }

            // right
            if (IsValidSquareToVisit(x + 1, y))
            {
                VisitSquare(x + 1, y, updatedRiskLevel, visitedGrid.Clone() as bool[,]);
            }

            // down
            if (IsValidSquareToVisit(x, y + 1))
            {
                VisitSquare(x, y + 1, updatedRiskLevel, visitedGrid.Clone() as bool[,]);
            }

            // left
            if (IsValidSquareToVisit(x - 1, y))
            {
                VisitSquare(x - 1, y, updatedRiskLevel, visitedGrid.Clone() as bool[,]);
            }   

            return;
        }

        private static bool IsValidSquareToVisit(int x, int y)
        {
            var lengthX = _caveRiskGrid.GetLength(1);
            var lengthY = _caveRiskGrid.GetLength(0);

            if (x == 0 && y == 0)
            {
                return false; // this is starting location
            }

            bool result = y >= 0 && y < lengthY && x >= 0 && x < lengthX;
            //connectedSquaresRiskLevel[0, 0] = (y - 1 >= 0 && x - 1 >= 0) ? _caveRiskGrid[y - 1, x - 1] : -1;
            //connectedSquaresRiskLevel[0, 0] = -1;
            //connectedSquaresRiskLevel[1, 0] = (y - 1 >= 0) ? _caveRiskGrid[y - 1, x] : -1;
            ////connectedSquaresRiskLevel[2, 0] = (y - 1 >= 0 && x + 1 < lengthX) ? _caveRiskGrid[y - 1, x + 1] : -1;
            //connectedSquaresRiskLevel[2, 0] = -1;
            //connectedSquaresRiskLevel[0, 1] = (x - 1 >= 0) ? _caveRiskGrid[y, x - 1] : -1;
            //connectedSquaresRiskLevel[1, 1] = -1;
            //connectedSquaresRiskLevel[2, 1] = (x + 1 < lengthX) ? _caveRiskGrid[y, x + 1] : -1;
            ////connectedSquaresRiskLevel[0, 2] = (y + 1 < lengthY && x - 1 >= 0) ? _caveRiskGrid[y + 1, x - 1] : -1;
            //connectedSquaresRiskLevel[0, 2] = -1;
            //connectedSquaresRiskLevel[1, 2] = (y + 1 < lengthY) ? _caveRiskGrid[y + 1, x] : -1;
            ////connectedSquaresRiskLevel[2, 2] = (y + 1 < lengthY && x + 1 < lengthX) ? _caveRiskGrid[y + 1, x + 1] : -1;
            //connectedSquaresRiskLevel[2, 2] = -1;

            return result;
        }
    }
}

