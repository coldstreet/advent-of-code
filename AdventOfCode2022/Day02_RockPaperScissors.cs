namespace AdventOfCode2022
{
    public static class Day02_RockPaperScissors
    {
        public static long DetermineP2ScoreVersion1(string[] input)
        {
            var plays = ParseInput(input);
            
            var scoringRules = new[,]
            {
                {(4,4), (1,8), (7,3)},
                {(8,1), (5,5), (2,9)},
                {(3,7), (9,2), (6,6)}
            };

            return plays.Sum(play => scoringRules[play.Item1, play.Item2].Item2);
        }

        public static long DetermineP2ScoreVersion2(string[] input)
        {
            var plays = ParseInput(input);

            var scoringRules = new[,]
            {
                {(7,3), (4,4), (1,8)},
                {(8,1), (5,5), (2,9)},
                {(9,2), (6,6), (3,7)}
            };

            // P2
            // 0 (rock) means lose
            // 1 (paper) means draw
            // 2 (scissors) means win

            return plays.Sum(play => scoringRules[play.Item1, play.Item2].Item2);
        }

        private static List<(int, int)> ParseInput(string[] input)
        {
            // parse input to get each player's play
            List<(int, int)> plays = new List<(int, int)>();
            foreach (var row in input)
            {
                var playersMove = row.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var p1 = playersMove[0] switch
                {
                    "A" => 0,
                    "B" => 1,
                    _ => 2
                };

                var p2 = playersMove[1] switch
                {
                    "X" => 0,
                    "Y" => 1,
                    _ => 2
                };

                plays.Add((p1, p2));
            }

            return plays;
        }
    }
}

