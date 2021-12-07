namespace AdventOfCode2021
{
    public static class Day4_Bingo
    {
        public static int PlayBingo(string[] input)
        {
            // parse input to get "balls" and cards
            var balls = input[0]
                .Split(',', StringSplitOptions.None)
                .Select(x => int.Parse(x)).ToArray();
            List<int[,]> cards = ExtractCards(input);

            foreach(var ball in balls)
            {
                // scan each card and update row or column count as needed
                foreach(var card in cards)
                {
                    for(int i = 0; i < 5; i++)
                    {
                        for(int j = 0; j < 5; j++)
                        {
                            if (card[i,j] == ball)
                            {
                                card[i, 5] -= ball;
                                card[5, j] -= ball;
                                var totalRow = card[i, 5];
                                var totalCol = card[5, j];
                                if (totalRow == 0)
                                {
                                    // winner!
                                    var winningSum = card[5, 0] + card[5, 1] + card[5, 2] + card[5, 3] + card[5, 4];
                                    return winningSum * ball;
                                }
                                
                                if (totalCol == 0)
                                {
                                    // winner!
                                    var winningSum = card[5, 0] + card[5, 1] + card[5, 2] + card[5, 3] + card[5, 4];
                                    return winningSum * ball;
                                }
                            }
                        }
                    }
                }
            }

            return 0;
        }

        public static int PlayBingoButGetScoreFromLastBoard(string[] input)
        {
            // parse input to get "balls" and cards
            var balls = input[0]
                .Split(',', StringSplitOptions.None)
                .Select(x => int.Parse(x)).ToArray();
            List<int[,]> cards = ExtractCards(input);

            var bingoCardsWon = new bool[cards.Count];
            for (int ballIndex = 0; ballIndex < balls.Length; ballIndex++)
            {
                int ball = balls[ballIndex];
                // scan each card and update row or column count as needed
                for (int cardIndex = 0; cardIndex < cards.Count; cardIndex++)
                {
                    var card = cards[cardIndex];
                    if (bingoCardsWon[cardIndex])
                    {
                        continue;
                    }

                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (bingoCardsWon[cardIndex])
                            {
                                break;
                            }

                            if (card[i, j] == ball)
                            {
                                card[i, 5] -= ball;
                                card[5, j] -= ball;
                                var totalRow = card[i, 5];
                                var totalCol = card[5, j];
                                if (totalRow == 0 || totalCol == 0)
                                {
                                    // winner!
                                    bingoCardsWon[cardIndex] = true;
                                    var bingoCardsRemaining = bingoCardsWon.Where(x => x == false).Count();
                                    if (bingoCardsRemaining == 0)
                                    {
                                        var winningSum = card[5, 0] + card[5, 1] + card[5, 2] + card[5, 3] + card[5, 4];
                                        return winningSum * ball;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return 0;
        }

        private static List<int[,]> ExtractCards(string[] input)
        {
            int rowIndex = 0;
            int rowTotal = 0;
            int[,] card = new int[6, 6];
            List<int[,]> cards = new List<int[,]>();
            for (int i = 2; i < input.Length; i++)
            {
                if (input[i] == null || input[i].Length == 0)
                {
                    rowIndex = 0;
                    continue;
                }

                var nums = input[i]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => int.Parse(x)).ToArray();
                for (int j = 0; j < nums.Length; j++)
                {
                    card[rowIndex, j] = nums[j];
                    rowTotal += nums[j];
                }
                card[rowIndex, 5] = rowTotal;

                if (rowIndex == 4)
                {
                    // sum columns
                    card[5, 0] = card[0, 0] + card[1, 0] + card[2, 0] + card[3, 0] + card[4, 0];
                    card[5, 1] = card[0, 1] + card[1, 1] + card[2, 1] + card[3, 1] + card[4, 1];
                    card[5, 2] = card[0, 2] + card[1, 2] + card[2, 2] + card[3, 2] + card[4, 2];
                    card[5, 3] = card[0, 3] + card[1, 3] + card[2, 3] + card[3, 3] + card[4, 3];
                    card[5, 4] = card[0, 4] + card[1, 4] + card[2, 4] + card[3, 4] + card[4, 4]; 
                    card[5, 5] = 0;
                    cards.Add(card);
                    card = new int[6, 6];
                }

                rowTotal = 0;
                rowIndex++;
            }

            return cards;
        }
    }
}
