namespace AdventOfCode2017
{
    public class RecursiveCircus
    {
        public static TowerBlock ConvertInput(string[] input)
        {
            Dictionary<string, TowerBlock> towerBlocks = new Dictionary<string, TowerBlock>();
            Dictionary<string, string[]> withChildren = new Dictionary<string, string[]>();
            char[] delimiterChars = { ' ', ',', '(', ')', '-', '>', ',' };
            foreach (var row in input)
            {
                var items = row.Split(delimiterChars).Where(s => s.Length > 0).ToArray();
                if (items.Length > 2)
                {
                    var children = new List<string>();
                    for (int i = 2; i < items.Length; i++)
                    {
                        children.Add(items[i]);
                    }
                    withChildren.Add(items[0], children.ToArray());
                }

                towerBlocks.Add(items[0], new TowerBlock(name: items[0], weight: int.Parse(items[1])));
            }

            foreach (string parentName in withChildren.Keys)
            {
                foreach (var childName in withChildren[parentName])
                {
                    towerBlocks[parentName].Children.Add(childName, towerBlocks[childName]);
                }
            }

            var highCount = 0;
            TowerBlock bottomTowerBlock = null;
            foreach (var name in withChildren.Keys)
            {
                int depthCount = towerBlocks[name].GetNodesDepth();
                if (depthCount > highCount)
                {
                    bottomTowerBlock = towerBlocks[name];
                    highCount = depthCount;
                }
            }

            return bottomTowerBlock;
        }

        public static int BalanceTower(TowerBlock baseTowerBlock, int lastAdjustedWeight = 0)
        {
            Console.WriteLine($"Name: {baseTowerBlock.Name}, IsBalanced: {baseTowerBlock.IsBalanced()}");
            if (baseTowerBlock.IsBalanced())
            {
                return lastAdjustedWeight;
            }

            foreach (TowerBlock child in baseTowerBlock.Children.Values)
            {
                Console.WriteLine($"Child Name: {child.Name}, Weight: {child.Weight}, IsBalanced: {child.IsBalanced()}");
                if (!child.IsBalanced())
                {
                    lastAdjustedWeight += BalanceTower(child);
                }
            }

            Console.WriteLine($"Adjust weight for: {baseTowerBlock.Name}, Weight: {baseTowerBlock.Weight}, IsBalanced: {baseTowerBlock.IsBalanced()}");
            lastAdjustedWeight += AdjustWeightToBalanceTower(baseTowerBlock);

            return lastAdjustedWeight;
        }

        public static int AdjustWeightToBalanceTower(TowerBlock towerBlockWithChildren)
        {
            Dictionary<string, int> weights = new Dictionary<string, int>();
            foreach (var child in towerBlockWithChildren.Children)
            {
                weights.Add(child.Value.Name, child.Value.GetTotalWeight());
            }

            // find outlier 
            var weightClasses = weights.Values.Distinct().ToArray();
            if (weightClasses.Length != 2)
            {
                return 0;
                // throw new ApplicationException("Expecting an unbalanced block but this block appears to be balanced.");
            }

            var weightOneCount = weights.Values.Count(x => x == weightClasses[0]);
            var weightTwoCount = weights.Values.Count(x => x == weightClasses[1]);

            int wrongWeight = (weightOneCount < weightTwoCount) ? weightClasses[0] : weightClasses[1];
            int correctWeight = (weightOneCount < weightTwoCount) ? weightClasses[1] : weightClasses[0];

            int diff = correctWeight - wrongWeight;
            int newWeight = 0;
            foreach (var key in weights.Keys)
            {
                if (weights[key] != wrongWeight)
                {
                    continue;
                }

                newWeight = towerBlockWithChildren.Children[key].Weight + diff;
                towerBlockWithChildren.Children[key].Weight = newWeight;
                break;
            }

            return newWeight;
        }

        public class TowerBlock
        {
            public string Name { get; }

            public int Weight { get; set; }

            public Dictionary<string, TowerBlock> Children { get; }

            public TowerBlock(string name, int weight, TowerBlock[] children = null)
            {
                Name = name;
                Weight = weight;
                Children = new Dictionary<string, TowerBlock>();
                if (children != null)
                {
                    foreach (var child in children)
                    {
                        AddChild(child);
                    }
                }
            }

            public void AddChild(TowerBlock child)
            {
                Children.Add(child.Name, child);
            }

            public int GetNodesDepth()
            {
                return CountNodeDepth(this, 0);
            }

            public int GetTotalWeight()
            {
                return CalculateWeight(this, Weight);
            }

            public bool IsBalanced()
            {
                if (Children.Count == 0)
                {
                    return true;
                }

                int weightClasses = Children.Values.Select(x => x.Weight).Distinct().ToArray().Length;
                return weightClasses == 1;
            }

            private int CountNodeDepth(TowerBlock towerBlock, int count)
            {
                if (towerBlock.Children.Count == 0)
                {
                    return count;
                }

                count++;
                foreach (var child in towerBlock.Children.Values)
                {
                    count = CountNodeDepth(child, count);
                }

                return count;
            }

            private int CalculateWeight(TowerBlock towerBlock, int weight)
            {
                if (towerBlock.Children.Count == 0)
                {
                    return weight;
                }
                
                foreach (var child in towerBlock.Children.Values)
                {
                    weight += CalculateWeight(child, child.Weight);
                }

                return weight;
            }
        }
    }
}
