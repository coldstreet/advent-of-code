namespace AdventOfCode2021
{
    public static class Day17_TrickShot
    {
        public static (int MaxHeight, int VelocityThatHitsTargetCount) FindVelocityThatMaximizesHeight(string input)
        {
            // input format: target area: x=20..30, y=-10..-5
            var ranges = input
                .Replace("target area: x=", "")
                .Replace(" y=", "")
                .Split(',');
            var minIndexX = int.Parse(ranges[0].Split("..")[0]);
            var maxIndexX = int.Parse(ranges[0].Split("..")[1]);
            var maxIndexY = int.Parse(ranges[1].Split("..")[0]); // assumes negative
            var minIndexY = int.Parse(ranges[1].Split("..")[1]); // assumes negative

            int maxHeight = 0;
            var velocityThatHitsTarget = new HashSet<(int, int)>();
            for (int y = maxIndexY; y <= Math.Abs(maxIndexY); y++)
            {
                for (int x = 1; x <= maxIndexX; x++)
                {
                    var testHeight = TestLaunch(x, y, minIndexX, minIndexY, maxIndexX, maxIndexY);
                    if (testHeight == -1)
                    {
                        continue;
                    }

                    maxHeight = testHeight > maxHeight ? testHeight : maxHeight;
                    if (!velocityThatHitsTarget.Contains((x, y)))
                    {
                        velocityThatHitsTarget.Add((x, y));
                    }
                }
            }

            return (maxHeight, velocityThatHitsTarget.Count());
        }

        private static int TestLaunch(int velocityX, int velocityY, int minX, int minY, int maxX, int maxY)
        {
            int step = 1;
            int currentX = 0;
            int currentY = 0;
            int maxHeight = 0;
            bool validLaunch = false;
            while (true)
            {
                currentX = currentX + velocityX < currentX ? currentX : currentX + velocityX;
                currentY += velocityY;  // assumes we always launch upwards
                maxHeight = currentY > maxHeight ? currentY : maxHeight;
                velocityX--;
                velocityY--;

                if (IsProbeWithinTarget(currentX, currentY, minX, minY, maxX, maxY))
                {
                    validLaunch = true;
                }

                if (IsProbeBeyondTarget(currentX, currentY, minX, minY, maxX, maxY))
                {
                    if (validLaunch)
                    {
                        return maxHeight;
                    }

                    return -1;
                }

                step++;
            }
        }

        private static bool IsProbeWithinTarget(int currentX, int currentY, int minX, int minY, int maxX, int maxY)
        {
            return (currentX >= minX && currentX <= maxX && currentY <= minY && currentY >= maxY) ? true : false; // assumes minY and maxY is negative
        }

        private static bool IsProbeBeyondTarget(int currentX, int currentY, int minX, int minY, int maxX, int maxY)
        {
            return (currentX > maxX || currentY < maxY) ? true : false;  // assumes maxY is negative
        }
    }
}

