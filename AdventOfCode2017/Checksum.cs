namespace AdventOfCode2017
{
    public class Checksum
    {
        public static int CalcChecksumV1(IList<int[]> input)
        {
            return (from row in input
                    let min = row.Min()
                    let max = row.Max()
                    select max - min).Sum();
        }

        public static int CalcChecksumV2(IList<int[]> input)
        {
            int sum = 0;
            foreach (var row in input)
            {
                int oldSum = sum;
                foreach (var number in row)
                {
                    if (sum > oldSum)
                    {
                        oldSum = sum;
                        break;
                    }

                    foreach (var otherNumber in row)
                    {
                        if (number == otherNumber)
                        {
                            continue;
                        }

                        var max = (number > otherNumber) ? number : otherNumber;
                        var min = (number < otherNumber) ? number : otherNumber;
                        if (max % min != 0)
                        {
                            continue;
                        }

                        sum += max / min;
                        break;
                    }
                }
            }
            return sum;
        }
    }
}
