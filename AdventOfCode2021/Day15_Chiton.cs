namespace AdventOfCode2021
{
    public static class Day15_Chiton
    {
        public class Square
        {
            public int RiskLevel { get; }
            public int[,] ConnectedSquaresRiskLevel { get;}
            public bool IsEndSquare { get; }
            public bool Visited { get; }

            public Square(int riskLevel, bool isEndSquare, int[,] connectedSquares)
            {
                RiskLevel = riskLevel;
                IsEndSquare = isEndSquare;
                ConnectedSquaresRiskLevel = connectedSquares;
            }
        }

        public static long FindMinRiskLevelPath(int[,] caveRiskGrid)
        {
            var lengthX = caveRiskGrid.GetLength(1);
            var lengthY = caveRiskGrid.GetLength(0);

            // build details about squares (i.e., connected squares excluding connecting to the start square and building a square detail for the end)
            var squareDictionary = new Dictionary<(int, int), Square>(lengthX*lengthY);
            for (int y = 0; y < lengthY; y++)
            {
                for (int x = 0; x < lengthX; x++)
                {
                    if (x == lengthX - 1 && y == lengthY - 1)
                    {
                        squareDictionary.Add((x, y), new Square(1, true, new int[3, 3]));
                        break;
                    }

                    // -1 is invalid square (i.e., outside grid)
                    var connectedSquaresRiskLevel = new int[3, 3];
                    connectedSquaresRiskLevel[0, 0] = (y - 1 >= 0 && x - 1 >= 0) ? caveRiskGrid[y - 1, x - 1] : -1;
                    connectedSquaresRiskLevel[1, 0] = (y - 1 >= 0) ? caveRiskGrid[y - 1, x] : -1;
                    connectedSquaresRiskLevel[2, 0] = (y - 1 >= 0 && x + 1 < lengthX) ? caveRiskGrid[y - 1, x + 1] : -1;
                    connectedSquaresRiskLevel[0, 1] = (x - 1 >= 0) ? caveRiskGrid[y, x - 1] : -1;
                    connectedSquaresRiskLevel[2, 1] = (x + 1 < lengthX) ? caveRiskGrid[y, x + 1] : -1;
                    connectedSquaresRiskLevel[0, 2] = (y + 1 < lengthY && x - 1 >= 0) ? caveRiskGrid[y + 1, x - 1] : -1;
                    connectedSquaresRiskLevel[1, 2] = (y + 1 < lengthY) ? caveRiskGrid[y + 1, x] : -1;
                    connectedSquaresRiskLevel[2, 2] = (y + 1 < lengthY && x + 1 < lengthX) ? caveRiskGrid[y + 1, x + 1] : -1;

                    squareDictionary.Add((x, y), new Square(caveRiskGrid[x, y], false, connectedSquaresRiskLevel));
                }
            }

            return 0;
        }
    }
}

