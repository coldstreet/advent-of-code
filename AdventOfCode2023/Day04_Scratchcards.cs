namespace AdventOfCode2023
{
    public static class Day04_Scratchcards
    {
        public static long AddScoresFromCards(string[] input)
        {
            var cards = ParseInput(input);
            foreach (var key in from key in cards.Keys
                                from number in cards[key].Numbers
                                where cards[key].Keys.Contains(number)
                                select key)
            {
                cards[key].Score = cards[key].Score == 0 ? 1 : cards[key].Score * 2;
            }

            long sum = 0;
            foreach (var card in cards.Values)
            {
                sum += card.Score;
            }

            return sum;
        }

        public static long CountCardsAfterWinnings(string[] input)
        {
            var cards = ParseInputForMultipleCards(input);
            foreach(var key in cards.Keys)
            {
                // process the first card in a set of card #
                var card = cards[key].First();
                int count = 0;
                foreach (var number in card.Numbers)
                {
                    if (card.Keys.Contains(number))
                    {
                        count++;
                    }
                }

                // Now add additional cards to preceeding sets (but don't go past last set of card #)
                foreach (var _ in cards[key])
                {
                    for (int k = key + 1; k <= Math.Min(cards.Count, key + count); k++)
                    {
                        cards[k].Add(cards[k].First());
                    }
                }
            }

            int sum = 0;
            foreach (var cardList in cards.Values)
            {
                sum += cardList.Count;
            }

            return sum;
        }

        private static IDictionary<int, Card> ParseInput(string[] input)
        {
            var cards = new Dictionary<int, Card>();
            foreach (var item in input)
            {
                var card = ParseInputLine(item);
                cards.Add(card.Id, card);
            }

            return cards;
        }

        private static IDictionary<int, IList<Card>> ParseInputForMultipleCards(string[] input)
        {
            var cards = new Dictionary<int, IList<Card>>();
            foreach (var item in input)
            {
                var card = ParseInputLine(item);
                cards.Add(card.Id, new List<Card> { new Card(card.Id, card.Keys, card.Numbers) });
            }

            return cards;
        }

        private static Card ParseInputLine(string item)
        {
            var parts = item.Split(new string[] { "Card ", ": ", " | " }, StringSplitOptions.RemoveEmptyEntries);
            var keys = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var numbers = parts[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            return new Card(int.Parse(parts[0]), keys, numbers);
        }
    }

    public class Card
    {
        public int Id { get; set; }

        public IList<int> Keys { get; set; }

        public IList<int> Numbers { get; set; }

        public long Score { get; set; }

        public Card(int id, IList<int> keys, IList<int> numbers)
        {
            Id = id;
            Keys = keys;
            Numbers = numbers;
        }
    }
}

