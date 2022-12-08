namespace AdventOfCode2022
{
    public static class Day07_NoSpaceLeftOnDevice
    {
        public static long FindSumOfDirsLessThanSpecifiedSize(string[] input)
        {
            var rootNode = ParseFileStucture(input);

            rootNode.Size = CalculateDirSize(rootNode);

            return FindSumOfDirsWithSizeLessThanEqual(rootNode, 100000);
        }

        private static Node ParseFileStucture(string[] input)
        {
            var rootNode = new Node("/", null);
            var currentDirNode = rootNode;
            foreach (var item in input)
            {
                if (item.StartsWith("$ cd /"))
                {
                    continue;
                }

                if (item.StartsWith("$ ls"))
                {
                    continue;
                }

                if (item.StartsWith("dir"))
                {
                    currentDirNode.Nodes.Add(new Node(item.Split(' ')[1], currentDirNode));
                    continue;
                }

                if (!item.StartsWith("$ cd ..") && item.StartsWith("$ cd "))
                {
                    var folder = item.Substring(5);
                    currentDirNode = currentDirNode.Nodes.Where(x => x.Name == folder).First();
                    continue;
                }

                if (item.StartsWith("$ cd .."))
                {
                    currentDirNode = currentDirNode.ParentNode;
                    continue;
                }

                var fileInfo = item.Split(' ');
                int size = int.Parse(fileInfo[0]);
                currentDirNode.Files.Add((fileInfo[1], size));
            }

            return rootNode;
        }

        private static int CalculateDirSize(Node rootNode)
        {
            int size = rootNode.Files.Sum(file => file.Item2);
            size += rootNode.Nodes.Sum(node => CalculateDirSize(node));

            rootNode.Size = size;
            return size;
        }

        private static int FindSumOfDirsWithSizeLessThanEqual(Node rootNode, int limit)
        {
            int sum = rootNode.Size <= limit ? rootNode.Size : 0;

            foreach (var node in rootNode.Nodes)
            {
                sum += FindSumOfDirsWithSizeLessThanEqual(node, limit);
            }

            return sum;
        }
    }

    public class Node
    {
        public Node? ParentNode { get; set; }

        public string Name { get; set; }

        public int Size { get; set; }

        public IList<Node> Nodes { get; set; }

        public IList<(string, int)> Files { get; set; }

        public Node(string name, Node? parent, int size = 0)
        {
            Nodes = new List<Node>();
            Files = new List<(string, int)>();
            ParentNode = parent;
            Name = name;
            Size = size;
        }
    }
}

