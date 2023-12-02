namespace AdventOfCode2023
{
    public static class Day02_CubeConundrum
    {
        public static long SumIdsOfPossibleGames(string[] input)
        {
            // possible: 12 red cubes, 13 green cubes, and 14 blue cubes
            // not possible: if any hand contains value greater (e.g., 20 red cubes)
            // not possible: if any hand contains more cubes than 39

            var sum = 0;
            foreach (var game in input)
            {
                var delimiters = new string[] { "Game ", ": ", "; " };
                var parts = game.Split(delimiters, StringSplitOptions.None);
                int id = int.Parse(parts[1]);
                bool validGame = true;
                for (int i = 2; i < parts.Length; i++)
                {
                    var blue = 0;
                    var red = 0;
                    var green = 0;
                    var diceTotalsPerRound = parts[i].Split(", ");
                    foreach( var dice in diceTotalsPerRound)
                    {
                        var numberAndColor = dice.Split(' ');
                        if (numberAndColor[1] == "blue")
                        {
                            blue = int.Parse(numberAndColor[0]);
                        }

                        if (numberAndColor[1] == "red")
                        {
                            red = int.Parse(numberAndColor[0]);
                        }

                        if (numberAndColor[1] == "green")
                        {
                            green = int.Parse(numberAndColor[0]);
                        }
                    }
                    

                    if (blue + red + green > 39 ||
                        blue > 14 ||
                        green > 13 ||
                        red > 12)
                    {
                        validGame = false;
                        break;
                    }
                }

                if (validGame)
                {
                    sum += id;
                }
            }

            return sum;
        }

        public static long SumProductOfMinDiceNeededForGame(string[] input)
        {
            var sum = 0;
            foreach (var game in input)
            {
                var delimiters = new string[] { "Game ", ": ", "; " };
                var parts = game.Split(delimiters, StringSplitOptions.None);
                int id = int.Parse(parts[1]);

                var maxBlue = 1;
                var maxRed = 1;
                var maxGreen = 1;
                for (int i = 2; i < parts.Length; i++)
                {
                    var blue = 0;
                    var red = 0;
                    var green = 0;
                    
                    var diceTotalsPerRound = parts[i].Split(", ");
                    foreach (var dice in diceTotalsPerRound)
                    {
                        var numberAndColor = dice.Split(' ');
                        if (numberAndColor[1] == "blue")
                        {
                            blue = int.Parse(numberAndColor[0]);
                            maxBlue = Math.Max(maxBlue, blue);
                        }

                        if (numberAndColor[1] == "red")
                        {
                            red = int.Parse(numberAndColor[0]);
                            maxRed = Math.Max(maxRed, red);
                        }

                        if (numberAndColor[1] == "green")
                        {
                            green = int.Parse(numberAndColor[0]);
                            maxGreen = Math.Max(maxGreen, green);
                        }
                    }
                }

                sum += maxRed * maxBlue * maxGreen;
            }

            return sum;
        }
    }
}

