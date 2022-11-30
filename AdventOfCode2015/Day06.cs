namespace AdventOfCode2015
{
    public class Day06
    {
        public class LightCommand
        {
            public string Command { get; }
            public int FromX { get; }
            public int FromY { get; }
            public int ToX { get; }
            public int ToY { get; }

            public LightCommand(string command, int fromX, int fromY, int toX, int toY)
            {
                Command = command;
                FromX = fromX;
                FromY = fromY;
                ToX = toX;
                ToY = toY;
            }
        }

        public class LightManager
        {
            private readonly int[,] _lights = new int[1000, 1000];

            public void ProcessListOfCommands(LightCommand[] commands, bool useLevelMethod = false)
            {
                foreach (var c in commands)
                {
                    ChangeStateOfLight(c.Command, c.FromX, c.FromY, c.ToX, c.ToY, useLevelMethod);
                }
            }

            public void ChangeStateOfLight(string command, int fromX, int fromY, int toX, int toY, bool useLevelMethod = false)
            {
                for (int x = fromX; x <= toX; x++)
                {
                    for (int y = fromY; y <= toY; y++)
                    {
                        var onLevel = _lights[x, y];
                        switch (command)
                        {
                            case "toggle":
                                if (useLevelMethod)
                                {
                                    onLevel = onLevel + 2;
                                }
                                else
                                {
                                    onLevel = (onLevel == 1) ? 0 : 1;
                                }
                                break;
                            case "turn off":
                                if (useLevelMethod)
                                {
                                    onLevel = (onLevel >= 1) ? onLevel - 1 : 0;
                                }
                                else
                                {
                                    onLevel = 0;
                                }
                                break;
                            case "turn on":
                                if (useLevelMethod)
                                {
                                    onLevel = onLevel + 1;
                                }
                                else
                                {
                                    onLevel = 1;
                                }
                                break;
                            default:
                                continue;
                        }

                        _lights[x, y] = onLevel;
                    }
                }
            }

            public int MeasureLightLevel()
            {
                int count = 0;

                for (int x = 0; x <= 999; x++)
                {
                    for (int y = 0; y <= 999; y++)
                    {
                        int onLevel = _lights[x, y];
                        count = count + onLevel;
                    }
                }

                return count;
            }
        }
    }
}
