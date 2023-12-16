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

        private static IDictionary<int, Card> ParseInput(string[] input)
        {
            var cards = new Dictionary<int, Card>();
            foreach (var item in input)
            {
                var parts = item.Split(new string[] { "Card ", ": ", " | " }, StringSplitOptions.RemoveEmptyEntries);
                var keys = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                var numbers = parts[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                cards.Add(int.Parse(parts[0]), new Card(keys, numbers));
            }

            return cards;
        }
    }

    public class Card
    {
        public IList<int> Keys { get; set; }

        public IList<int> Numbers { get; set; }

        public long Score { get; set; }

        public Card(IList<int> keys, IList<int> numbers)
        {
            Keys = keys;
            Numbers = numbers;
        }
    }
}

