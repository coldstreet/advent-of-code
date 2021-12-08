namespace AdventOfCode2021
{
    public static class Day8_SignalDisplay
    {
        public static int CountDigits_1_4_7_8(string[] signalsAndOutputs)
        {
            var outputs = new List<string[]>();
            foreach (var signalAndOutput in signalsAndOutputs)
            {
                var output = signalAndOutput.Split('|', StringSplitOptions.TrimEntries).Last();
                if (output != null)
                {
                    outputs.Add(output.Split(' ', StringSplitOptions.TrimEntries));
                }
            }

            int count = 0;
            foreach(var output in outputs)
            {
                foreach(var digit in output)
                {
                    var length = digit.Length;
                    if (length == 2 || length == 3 || length == 4 || length == 7)
                    {
                        count++;
                    }
                }
            }
                
            return count;
        }

        public static int SumAllDigits(string[] signalsAndOutputs)
        {
            var keyAndOutputItems = new List<string[]>();
            foreach (var signalAndOutput in signalsAndOutputs)
            {
                keyAndOutputItems.Add(signalAndOutput.Split('|', StringSplitOptions.TrimEntries));
            }

            int sum = 0;
            foreach (var keyAndOutput in keyAndOutputItems)
            {
                var key = keyAndOutput[0];
                var output = keyAndOutput[1];
                var digits = new Dictionary<int, char[]>(10);
                var keyItems = key.Split(' ', StringSplitOptions.TrimEntries);
                foreach (var keyItem in keyItems)
                {
                    var length = keyItem.Length;
                    if (length == 2)
                    {
                        digits.Add(1, keyItem.ToArray());
                        continue;
                    }

                    if (length == 3)
                    {
                        digits.Add(7, keyItem.ToArray());
                        continue;
                    }

                    if (length == 4)
                    {
                        digits.Add(4, keyItem.ToArray());
                        continue;
                    }

                    if (length == 7)
                    {
                        digits.Add(8, keyItem.ToArray());
                    }
                }

                // Positions
                //  1 
                // 2 3
                //  4
                // 5 6
                //  7

                // look at 1 and 7 to find the "1st" position
                var position1 = digits[7].Except(digits[1]).First();
                var rightPositions = digits[1];

                // look at the ones with length 6 to find the "7th" position
                var charsFromLength6Set = new List<char>();
                foreach (var keyItem in keyItems)
                {
                    var length = keyItem.Length;
                    if (length == 6)
                    {
                        charsFromLength6Set.AddRange(keyItem
                            .ToCharArray()
                            .ToList());
                    }
                }

                var position7 = charsFromLength6Set
                    .GroupBy(i => i)
                    .OrderByDescending(grp => grp.Count())
                    .Select(grp => grp.Key)
                    .Except(digits[4])
                    .Except(digits[7])
                    .First();

                // look at the ones with length of 5 to find the "4th" position
                var charsFromLength5Set = new List<char>();
                foreach (var keyItem in keyItems)
                {
                    var length = keyItem.Length;
                    if (length == 5)
                    {
                        charsFromLength5Set.AddRange(keyItem
                            .ToCharArray()
                            .ToList());
                    }
                }

                var position4 = charsFromLength5Set
                    .GroupBy(i => i)
                    .OrderByDescending(grp => grp.Count())
                    .Select(grp => grp.Key)
                    .Except(new char[] { position1 })
                    .Except(new char[] { position7 })
                    .First();

                var charsForZero = charsFromLength6Set
                    .Distinct()
                    .Except(new char[] { position4 });
                digits.Add(0, charsForZero.ToArray());
                digits.Add(3, new char[] { position1, position4, position7, rightPositions[0], rightPositions[1] });

                var charsForLeftPositions = charsForZero
                    .Except(digits[3]);

                var position2 = charsFromLength6Set
                    .GroupBy(i => i)
                    .OrderByDescending(grp => grp.Count())
                    .Select(grp => grp.Key)
                    .Except(new char[] { position1 })
                    .Except(new char[] { position4 })
                    .Except(new char[] { position7 })
                    .Except(new char[] { rightPositions[0] })
                    .Except(new char[] { rightPositions[1] })
                    .First();

                var position5 = charsForLeftPositions.Except(new char[] { position2 }).First();
                var position6 = charsFromLength6Set
                    .GroupBy(i => i)
                    .OrderByDescending(grp => grp.Count())
                    .Select(grp => grp.Key)
                    .Except(new char[] { position1 })
                    .Except(new char[] { position4 })
                    .Except(new char[] { position7 })
                    .Except(new char[] { position2 })
                    .Except(new char[] { position5 })
                    .First();
                var position3 = rightPositions.Except(new char[] { position6 }).First();

                digits.Add(2, new char[] { position1, position4, position7, position3, position5 });
                digits.Add(5, new char[] { position1, position4, position7, position2, position6 });
                digits.Add(6, new char[] { position1, position4, position7, position2, position5, position6 });
                digits.Add(9, new char[] { position1, position4, position7, position2, position3, position6 });

                var numberCodes = new Dictionary<string, int>();
                numberCodes.Add(string.Concat(digits[0].OrderBy(c => c)), 0);
                numberCodes.Add(string.Concat(digits[1].OrderBy(c => c)), 1);
                numberCodes.Add(string.Concat(digits[2].OrderBy(c => c)), 2);
                numberCodes.Add(string.Concat(digits[3].OrderBy(c => c)), 3);
                numberCodes.Add(string.Concat(digits[4].OrderBy(c => c)), 4);
                numberCodes.Add(string.Concat(digits[5].OrderBy(c => c)), 5);
                numberCodes.Add(string.Concat(digits[6].OrderBy(c => c)), 6);
                numberCodes.Add(string.Concat(digits[7].OrderBy(c => c)), 7);
                numberCodes.Add(string.Concat(digits[8].OrderBy(c => c)), 8);
                numberCodes.Add(string.Concat(digits[9].OrderBy(c => c)), 9);

                var outputDigits = output
                    .Split(' ', StringSplitOptions.TrimEntries)
                    .Select(x => string.Concat(x.OrderBy(c => c)))
                    .ToArray();
                sum += numberCodes[outputDigits[0]] * 1000
                    + numberCodes[outputDigits[1]] * 100
                    + numberCodes[outputDigits[2]] * 10
                    + numberCodes[outputDigits[3]];
            }

            return sum;
        }
    }
}
