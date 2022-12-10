namespace AdventOfCode2022
{
    public static class Day08_TreetopTreeHouse
    {
        public static long CountVisibleTrees(string[] input)
        {
            var treeGrid = ParseInput(input);

            MarkVisibleTrees(treeGrid);
            
            int visibleTrees = 0;
            foreach (var row in treeGrid)
            {
                visibleTrees += row.Count(x => x.Visible);
            }

            return visibleTrees;
        }

        public static long FindMaxScenicView(string[] input)
        {
            var treeGrid = ParseInput(input);

            for (var i = 0; i < treeGrid.Length; i++)
            {
                for (int j = 0; j < treeGrid[i].Length - 1; j++)
                {
                    // from the left, looking right
                    var subRow = treeGrid[i].Take(new Range(j, treeGrid[i].Length)).ToArray();
                    var scenicScore = ScoreInOneDirection(subRow);
                    FactorInScore(treeGrid, i, j, scenicScore);

                    // from the right, looking left
                    subRow = treeGrid[i].Reverse().Take(new Range(j, treeGrid[i].Length)).ToArray();
                    scenicScore = ScoreInOneDirection(subRow);
                    FactorInScore(treeGrid, i, treeGrid[i].Length - 1 - j, scenicScore);
                }
            }

            for (var j = 0; j < treeGrid[0].Length; j++)
            {
                var column = GetColumn(treeGrid, j);
                for (int i = 0; i < treeGrid.Length - 1; i++)
                {
                    // from the top, looking down
                    var subColumn = column.Take(new Range(i, treeGrid.Length)).ToArray();
                    var scenicScore = ScoreInOneDirection(subColumn);
                    FactorInScore(treeGrid, i, j, scenicScore);

                    // from the bottom, looking up
                    subColumn = column.Reverse().Take(new Range(i, treeGrid.Length)).ToArray();
                    scenicScore = ScoreInOneDirection(subColumn);
                    FactorInScore(treeGrid, treeGrid.Length - 1 - i, j, scenicScore);
                }
            }

            var maxScenicView = 0;
            foreach (var row in treeGrid)
            {
                var maxForRow = row.Max(x => x.ScenicScore);
                maxScenicView = maxForRow > maxScenicView ? maxForRow : maxScenicView;
            }

            return maxScenicView;
        }

        private static void FactorInScore(Tree[][] treeGrid, int row, int column, int scenicScore)
        {
            if (treeGrid[row][column].ScenicScore == 0)
            {
                treeGrid[row][column].ScenicScore = scenicScore;
            }
            else
            {
                treeGrid[row][column].ScenicScore *= scenicScore;
            }
        }

        private static int ScoreInOneDirection(Tree[] treeGrid)
        {
            var scenicScore = 0;
            for (int i = 1; i < treeGrid.Length; i++)
            {
                // [home][...][a][b]
                var a = treeGrid[i - 1].Height;
                var b = treeGrid[i].Height;
                var home = treeGrid[0].Height;

                // if first adjacent 
                if (i - 1 == 0)
                {
                    scenicScore = 1;
                    if (home > b)
                    {
                        continue;
                    }

                    break;
                }

                if (a <= b)
                {
                    scenicScore++;
                }
                else
                {
                    break;
                }

                if (home < b)
                {
                    break;
                }
            }

            return scenicScore;
        }

        private static void MarkVisibleTrees(Tree[][] treeGrid)
        {
            // mark top and bottom rows as visible
            foreach (var tree in treeGrid[0])
            {
                tree.Visible = true;
            }

            foreach (var tree in treeGrid[^1])
            {
                tree.Visible = true;
            }

            // mark first and column as visible
            for (var i = 0; i < treeGrid[0].Length; i++)
            {
                treeGrid[i][0].Visible = true;
                treeGrid[i][^1].Visible = true;
            }

            // "viewed" from left 
            var previousHeight = 0;
            foreach (var row in treeGrid)
            {
                previousHeight = 0;
                foreach (var tree in row)
                {
                    if (tree.Height > previousHeight)
                    {
                        tree.Visible = true;
                        previousHeight = tree.Height;
                    }
                }
            }

            // "viewed" from right 
            foreach (var row in treeGrid)
            {
                previousHeight = 0;
                for (var j = row.Length - 1; j >= 0; j--)
                {
                    var tree = row[j];
                    if (tree.Height > previousHeight)
                    {
                        tree.Visible = true;
                        previousHeight = tree.Height;
                    }
                }
            }

            // "viewed" from top
            for (var j = 0; j < treeGrid[0].Length; j++)
            {
                previousHeight = 0;
                for (var i = 0; i < treeGrid[j].Length; i++)
                {
                    var tree = treeGrid[i][j];
                    if (tree.Height > previousHeight)
                    {
                        tree.Visible = true;
                        previousHeight = tree.Height;
                    }
                }
            }
            
            // "viewed" from bottom
            for (var j = 0; j < treeGrid[0].Length; j++)
            {
                previousHeight = 0;
                for (var i = treeGrid[j].Length - 1; i >= 0; i--)
                {
                    var tree = treeGrid[i][j];
                    if (tree.Height > previousHeight)
                    {
                        tree.Visible = true;
                        previousHeight = tree.Height;
                    }
                }
            }
        }

        private static Tree[] GetColumn(Tree[][] matrix, int j)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x][j])
                .ToArray();
        }

        private static Tree[][] ParseInput(string[] input)
        {
            var trees = new List<List<Tree>>();
            foreach (var row in input)
            {
                var newRowOfTrees = row
                    .ToCharArray()
                    .Select(x => new Tree((int)char.GetNumericValue(x)))
                    .ToList();
                trees.Add(newRowOfTrees);
            }

            Tree[][] treeGrid = trees.Select(x => x.ToArray()).ToArray();
            return treeGrid;
        }

        public class Tree
        {
            public bool Visible { get; set; }
            public int Height { get; }

            public int ScenicScore { get; set; }

            public Tree(int height)
            {
                Height = height;
            }
        }
    }
}

