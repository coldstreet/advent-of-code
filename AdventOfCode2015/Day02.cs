namespace AdventOfCode2015
{
    public class Day02
    {
        public Tuple<int, int> CalculatePaperAndRibbonNeeded(string[] inputs)
        {
            var totalPaper = 0;
            var ribbonTotal = 0;
            foreach (var dimensions in inputs.Select(ParseInputIntoLengthWidthHeight))
            {
                totalPaper = totalPaper + CalculateSurfaceAreaOfBoxPlusSlack(dimensions.Item1, dimensions.Item2, dimensions.Item3);
                ribbonTotal = ribbonTotal + CalculateRibbonNeeded(dimensions.Item1, dimensions.Item2, dimensions.Item3);
            }

            return new Tuple<int, int>(totalPaper, ribbonTotal);
        }

        private int CalculateSurfaceAreaOfBoxPlusSlack(int l, int w, int h)
        {
            var surfaceAreaOfBox = 2*l*w + 2*w*h + 2*h*l;

            var slack = Math.Min(l * w, Math.Min(w * h, h * l));

            return surfaceAreaOfBox + slack;
        }

        private int CalculateRibbonNeeded(int l, int w, int h)
        {
            var surfaceAreaOfBox = 2 * l * w + 2 * w * h + 2 * h * l;

            var min = Math.Min(l, Math.Min(w, h));
            var nextMin = min;
            if (l == min)
            {
                nextMin = Math.Min(w, h);
            }
            else if (w ==  min)
            {
                nextMin = Math.Min(l, h);
            }
            else if (h == min)
            {
                nextMin = Math.Min(l, w);
            }

            var ribbon = min + min + nextMin + nextMin;
            var bow = l * w * h;

            return ribbon + bow;
        }

        private Tuple<int, int, int> ParseInputIntoLengthWidthHeight(string input)
        {
            var results = input.Split('x');

            int l = int.Parse(results[0]);
            int w = int.Parse(results[1]);
            int h = int.Parse(results[2]);

            return new Tuple<int, int, int>(l, w, h);
        }
    }
}
