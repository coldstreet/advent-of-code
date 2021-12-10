namespace AdventOfCode2021
{
    public class Day03_PowerConsumptionUtilities
    {
        // Day 3, part I
        public static int GetPowerConsumption(string[] binaryNumbers)
        {
            if (binaryNumbers == null || binaryNumbers.Length == 0)
            {
                return 0; 
            }

            var lengthOfBinaryNumber = binaryNumbers[0].Length;
            var bitCounts = BuildBitCountDictionary(binaryNumbers, lengthOfBinaryNumber);
            
            var gamma = "";
            var epsilon = "";
            for (int i = 0; i < lengthOfBinaryNumber; i++)
            {
                gamma += (bitCounts[i] >= binaryNumbers.Length - bitCounts[i]) ? "1" : "0";
                epsilon += (bitCounts[i] >= binaryNumbers.Length - bitCounts[i]) ? "0" : "1";
            }

            var gammaRate = Convert.ToInt32(gamma, 2);
            var epsilonRate = Convert.ToInt32(epsilon, 2);

            return gammaRate * epsilonRate;
        }

        // Day 3, part II
        public static int GetLifeSupportRating(string[] binaryNumbers)
        {
            if (binaryNumbers == null || binaryNumbers.Length == 0)
            {
                return 0;
            }
                        
            // determine oxygen generator rating  
            string[] oxygenBinaryNumbers = ReduceOnBitCriteria(binaryNumbers, true);

            // determine co2 generator rating  
            var co2BinaryNumbers = ReduceOnBitCriteria(binaryNumbers, false);
            
            var oxygenRate = Convert.ToInt32(oxygenBinaryNumbers[0], 2);
            var co2Rate = Convert.ToInt32(co2BinaryNumbers[0], 2);

            return oxygenRate * co2Rate;
        }

        private static string[] ReduceOnBitCriteria(string[] binaryNumbers, bool useMostCommon = true)
        {
            var lengthOfBinaryNumber = binaryNumbers[0].Length;
            var elementBinaryNumbers = new string[binaryNumbers.Length];
            binaryNumbers.CopyTo(elementBinaryNumbers, 0);
            var elementBitCounts = BuildBitCountDictionary(binaryNumbers, lengthOfBinaryNumber);
            for (int i = 0; i < lengthOfBinaryNumber; i++)
            {
                int removeCount = 0;
                var newOxygenList = elementBinaryNumbers.Select(x => x.Clone()).ToList();
                for (int j = 0; j < elementBinaryNumbers.Length; j++)
                {
                    char bitCriteria1 = useMostCommon ? '0' : '1';
                    char bitCriteria2 = useMostCommon ? '1' : '0';
                    var elementBinaryNumber = elementBinaryNumbers[j];
                    if (elementBitCounts[i] == elementBinaryNumbers.Length - elementBitCounts[i] &&
                        elementBinaryNumber[i] == bitCriteria1)
                    {
                        newOxygenList.RemoveAt(j - removeCount);
                        removeCount++;
                        if (newOxygenList.Count == 1)
                        {
                            break;
                        }
                    }
                    else if (elementBitCounts[i] >= elementBinaryNumbers.Length - elementBitCounts[i] &&
                        elementBinaryNumber[i] == bitCriteria1)
                    {
                        newOxygenList.RemoveAt(j - removeCount);
                        removeCount++;
                        if (newOxygenList.Count == 1)
                        {
                            break;
                        }
                    }
                    else if (elementBitCounts[i] < elementBinaryNumbers.Length - elementBitCounts[i] &&
                             elementBinaryNumber[i] == bitCriteria2)
                    {
                        newOxygenList.RemoveAt(j - removeCount);
                        removeCount++;
                        if (newOxygenList.Count == 1)
                        {
                            break;
                        }
                    }
                }

                elementBinaryNumbers = new string[newOxygenList.Count];
                newOxygenList.CopyTo(elementBinaryNumbers, 0);
                if (newOxygenList.Count == 1)
                {
                    break;
                }

                elementBitCounts = BuildBitCountDictionary(elementBinaryNumbers, lengthOfBinaryNumber);
            }

            return elementBinaryNumbers;
        }

        private static Dictionary<int, int> BuildBitCountDictionary(string[] binaryNumbers, int lengthOfBinaryNumber)
        {
            var bitCounts = new Dictionary<int, int>(lengthOfBinaryNumber);
            for (int i = 0; i < lengthOfBinaryNumber; i++)
            {
                bitCounts[i] = 0;
            }

            foreach (var binaryNumber in binaryNumbers)
            {
                for (int i = 0; i < lengthOfBinaryNumber; i++)
                {
                    char bit = binaryNumber[i];
                    if (bit == '1')
                    {
                        bitCounts[i]++;
                    }
                }
            }

            return bitCounts;
        }
    }
}
