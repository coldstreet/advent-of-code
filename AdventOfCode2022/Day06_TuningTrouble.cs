namespace AdventOfCode2022
{
    public static class Day06_TuningTrouble
    {
        public static long FindCharCountAtEndOfFirstMarker(string input, int distinctMarkerCount)
        {
            int lastMarkerPosition = distinctMarkerCount;
            for (var i = 0; i < input.Length - distinctMarkerCount; i++)
            {
                var marker = input.ToCharArray().Take(new Range(i, i + distinctMarkerCount));
                if (marker.Distinct().Count() == distinctMarkerCount)
                {
                    break;
                }

                lastMarkerPosition++;
            }

            return lastMarkerPosition;
        }
    }
}

