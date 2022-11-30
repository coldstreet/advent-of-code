namespace AdventOfCode2015
{
    public class Day01
    {
        public Tuple<int, int> DetermineFloor(string input)
        {
            int floorCount = 0;
            int position = 0;
            int? firstBasementPosition = null;
            foreach (char c in input)
            {
                switch (c)
                {
                    case '(':
                        floorCount++;
                        break;
                    case ')':
                        floorCount--;
                        break;
                }

                position++;
                if (firstBasementPosition.HasValue)
                {
                    continue;
                }
                
                if (floorCount == -1)
                {
                    firstBasementPosition = position;
                }
            }

            return new Tuple<int, int>(floorCount, firstBasementPosition ?? 0);
        }
    }
}
