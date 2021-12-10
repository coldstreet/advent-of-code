namespace AdventOfCode2021
{
    public class Day02_PositionUtilities
    {
        public static (int, int) DetermineFinalPosition((string, int)[] commandsAndPositions)
        {
            int x = 0;
            int y = 0;
            foreach(var (command, position) in commandsAndPositions)
            {
                if (command == "forward")
                {
                    x = x + position;
                    continue;
                }

                if (command == "down")
                {
                    y = y + position;
                    continue;
                }

                if (command == "up")
                {
                    y = y - position;
                }
            }

            return (x, y);
        }

        public static (int, int) DetermineFinalPositionUsingAim((string, int)[] commandsAndPositions)
        {
            int x = 0;
            int y = 0;
            int aim = 0;
            foreach (var (command, position) in commandsAndPositions)
            {
                if (command == "forward")
                {
                    x = x + position;
                    y = y + aim * position;
                    continue;
                }

                if (command == "down")
                {
                    aim = aim + position;
                    continue;
                }

                if (command == "up")
                {
                    aim = aim - position;
                }
            }

            return (x, y);
        }
    }
}
