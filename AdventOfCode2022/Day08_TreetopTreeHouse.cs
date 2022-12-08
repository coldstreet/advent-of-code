namespace AdventOfCode2022
{
    public static class Day08_TreetopTreeHouse
    {
        public static long CountVisibleTrees(string[] input)
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
            for (var i = 0; i < treeGrid.LongLength; i++)
            {
                treeGrid[i][0].Visible = true;
                treeGrid[i][^1].Visible = true;
            }

            // "viewed" from left 
            var previousHeight = 0;
            foreach (var row in trees)
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
            foreach (var row in trees)
            {
                previousHeight = 0;
                for (var j = row.Count - 1; j >= 0; j--)
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


            int visibleTrees = 0;
            foreach (var row in trees)
            {
                visibleTrees += row.Count(x => x.Visible);
            }

            return visibleTrees;
        }

        public class Tree
        {
            public bool Visible { get; set; }
            public int Height { get; }

            public Tree(int height)
            {
                Height = height;
            }
        }
    }
}

