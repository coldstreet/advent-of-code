using System.Text.Json;

namespace AdventOfCode2022
{
    public static class Day13_DistressSignal
    {
        public static long SumIndicesWithCorrectOrder(string[] input)
        {
            // parse input
            var signals = new List<(string, string)>();
            for (var i = 0; i < input.Length; i+=3)
            {
                signals.Add((input[i], input[i+1]));
            }

            var validIndicesSum = 0;
            for (var i = 0; i < signals.Count; i++)
            {
                var left = signals[i].Item1;
                var right = signals[i].Item2;

                // Parse lists
                string[] splitOn = { ",[", "]," };
                var leftStringList = left
                    .Substring(1, left.Length - 2)
                    .Split(splitOn, System.StringSplitOptions.None)
                    .ToList();

               
                var rightStringList = right
                    .Substring(1, right.Length - 2)
                    .Split(splitOn, System.StringSplitOptions.None)
                    .ToList();

                if (leftStringList.Count > rightStringList.Count)
                {
                    continue;
                }

                var leftItems = new List<int[]>();
                foreach (var s in leftStringList)
                {
                    bool isList = s.Contains('[') || s.Contains(']');
                    var leftString = s.Replace("[", string.Empty);
                    leftString = leftString.Replace("]", string.Empty);

                    if (leftString.Contains(','))
                    {
                        if (isList)
                        {
                            leftItems.Add(leftString.Split(',').Select(int.Parse).ToArray());
                        }
                        else
                        {
                            foreach (var x in leftString.Split(',').Select(int.Parse).ToArray())
                            {
                                leftItems.Add(new [] { x });
                            }
                        }
                    }
                    else if (leftString.Length > 0)
                    {
                        leftItems.Add(new[] {int.Parse(leftString) });
                    }
                    else
                    {
                        leftItems.Add(new int[] { });
                    }
                }

                var rightItems = new List<int[]>();
                foreach (var s in rightStringList)
                {
                    bool isList = s.Contains('[') || s.Contains(']');
                    var rightString = s.Replace("[", string.Empty);
                    rightString = rightString.Replace("]", String.Empty);

                    if (rightString.Contains(','))
                    {
                        if (isList)
                        {
                            rightItems.Add(rightString.Split(',').Select(int.Parse).ToArray());
                        }
                        else
                        {
                            foreach (var x in rightString.Split(',').Select(int.Parse).ToArray())
                            {
                                rightItems.Add(new[] { x });
                            }
                        }
                    }
                    else if (rightString.Length > 0)
                    {
                        rightItems.Add(new[] { int.Parse(rightString) });
                    }
                    else
                    {
                        rightItems.Add(new int[] { });
                    }
                }

                if (leftItems.Count > rightItems.Count)
                {
                    continue;
                }
                
                bool valid = true;
                for (var j = 0; j < leftItems.Count; j++)
                {
                    if (!ValidSignalPart(leftItems[j], rightItems[j]))
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid)
                {
                    validIndicesSum += i + 1;
                }
            }

            return validIndicesSum;
        }

        private static bool ValidSignalPart(int[] left, int[] right)
        {
            var length = Math.Min(left.Length, right.Length);
            for (var i = 0; i < length; i++)
            {
                if (left[i] > right[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}

