using System.Diagnostics;

namespace AdventOfCode2021
{
    public class Day01_DepthReadingUtilities
    {
        public static int CountDepthIncreases(int[] readings)
        {
            if (readings.Length == 0)
            {
                return 0;
            }

            int count = -1;
            int previous = int.MinValue;
            foreach (var reading in readings)
            {
                if (reading > previous)
                {
                    count++;
                }

                previous = reading;
            }

            return count;
        }

        public static int CountDepthIncreasesByWindow(int[] readings)
        {
            if (readings.Length == 0)
            {
                return 0;
            }

            // Group measurements by "windows" and sum readings per window
            Dictionary<int, int> sums = new Dictionary<int, int>();
            for (int i = 0; i < readings.Length; i++)
            {
                sums.Add(i, 0);
            }

            sums[0] = readings[0] + readings[1] + readings[2];
            sums[1] = readings[1] + readings[2];
            sums[2] = readings[2];

            int alphaKey = 0;
            int betaKey = 1;
            int gammaKey = 2;

            int alphaKeyUsed = 3;
            int betaKeyUsed = 2;
            int gammaKeyUsed = 1;
            for (int i = 3; i < readings.Length-2; i++)
            {
                (alphaKeyUsed, alphaKey) = AdjustKeyForWinows(alphaKeyUsed, alphaKey);
                (betaKeyUsed, betaKey) = AdjustKeyForWinows(betaKeyUsed, betaKey);
                (gammaKeyUsed, gammaKey) = AdjustKeyForWinows(gammaKeyUsed, gammaKey);

                sums[alphaKey] = sums[alphaKey] + readings[i];
                sums[betaKey] = sums[betaKey] + readings[i];
                sums[gammaKey] = sums[gammaKey] + readings[i];

                alphaKeyUsed++;
                betaKeyUsed++;
                gammaKeyUsed++;
            }

            if (alphaKeyUsed == 1)
            {
                sums[alphaKey] = sums[alphaKey] + readings[readings.Length - 2] + readings[readings.Length-1];
                sums[gammaKey] = sums[gammaKey] + readings[readings.Length - 2];
            }

            if (betaKeyUsed == 1)
            {
                sums[betaKey] = sums[betaKey] + readings[readings.Length - 2] + readings[readings.Length-1];
                sums[alphaKey] = sums[alphaKey] + readings[readings.Length - 2];
            }

            if (gammaKeyUsed == 1)
            {
                sums[gammaKey] = sums[gammaKey] + readings[readings.Length - 2] + readings[readings.Length-1];
                sums[betaKey] = sums[betaKey] + readings[readings.Length - 2];
            }

            int count = -1;
            int previous = int.MinValue;
            foreach (int sum in sums.Values)
            {
                // Debug.WriteLine($"Sum: {sum}");
                if (sum > previous)
                {
                    count++;
                }

                previous = sum;
            }

            return count;
        }

        private static (int, int) AdjustKeyForWinows(int keyUsed, int key)
        {
            if (keyUsed == 3)
            {
                keyUsed = 0;
                key = key + 3;
            }

            return (keyUsed, key);
        }
    }
}
