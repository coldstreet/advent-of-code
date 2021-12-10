namespace AdventOfCode2021
{
    public static class Day07_CrabUtilities
    {
        public static int FindFuelToMinimizeAlignmentPosition(int[] currentPositions, bool useHighFuelBurn = false)
        {
            // reduce the range of positions to test by only using the range of avg +- std
            double average = currentPositions.Average();
            double sum = currentPositions.Sum(d => Math.Pow(d - average, 2));
            int std = (int) Math.Sqrt((sum) / (currentPositions.Count() - 1));

            int lowerPosition = std - (int) average;
            lowerPosition = lowerPosition < 0 ? 0 : lowerPosition;
            int upperPosition = std + (int) average;
            upperPosition = upperPosition > currentPositions.Max() ? currentPositions.Max() : upperPosition;

            int minFuel = int.MaxValue;
            for(int i = lowerPosition; i <= upperPosition; i++)
            {
                int fuel = 0;
                if (useHighFuelBurn)
                {
                    fuel = CalculateFuelUsedInMovesToPositionUsingHighFuelBurn(currentPositions, i);
                }
                else
                {
                    fuel = CalculateFuelUsedInMovesToPosition(currentPositions, i);
                }

                if (fuel < minFuel)
                {
                    minFuel = fuel;
                }
            }

            return minFuel;
        }

        private static int CalculateFuelUsedInMovesToPosition(int[] currentPositions, int position)
        {
            int fuel = 0;
            for (int i = 0; i < currentPositions.Length; i++)
            {
                int currentPosition = currentPositions[i];
                fuel += Math.Abs(position - currentPosition);
            }

            return fuel;
        }

        private static int CalculateFuelUsedInMovesToPositionUsingHighFuelBurn(int[] currentPositions, int position)
        {
            int fuel = 0;
            for (int i = 0; i < currentPositions.Length; i++)
            {
                int currentPosition = currentPositions[i];
                int positionsToTravel = Math.Abs(currentPosition - position);
                fuel += positionsToTravel * (positionsToTravel + 1) / 2;
            }

            return fuel;
        }
    }
}
