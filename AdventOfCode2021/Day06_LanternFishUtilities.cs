namespace AdventOfCode2021
{
    public static class Day06_LanternFishUtilities
    {
        public static long GetNumberOfFishAfterTime(int[] currentFishAges, int days)
        {
            long[] fishPerAge = new long[9];
            foreach(int age in currentFishAges)
            {
                fishPerAge[age]++;
            }

            for(int i = 0; i < days; i++)
            {
                long fishWithNoTimerLeft = fishPerAge[0];
                for (int ageIndex = 1; ageIndex < 9; ageIndex++)
                {
                    // shift population down in array
                    fishPerAge[ageIndex - 1] = fishPerAge[ageIndex];
                }

                fishPerAge[6] += fishWithNoTimerLeft;
                fishPerAge[8] = fishWithNoTimerLeft;
            }

            return fishPerAge.Sum();
        }
    }
}

