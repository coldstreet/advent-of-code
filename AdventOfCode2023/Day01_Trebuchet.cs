namespace AdventOfCode2023
{
    public static class Day01_Trebuchet
    {
        public static long SumCalibrationValues(string[] input)
        {
            var sum = 0;
            foreach (var item in input)
            {
                (var first, _) = FindFirstNumber(item);
                (var last, _) = FindLastNumber(item);
                char[] chars = { first, last };
                var s = new string(chars);
                sum += int.Parse(s);
            }

            return sum;
        }

        public static long SumCalibrationValuesV2(string[] input)
        {
            var sum = 0;
            foreach (var item in input)
            {
                (var first, int firstNumberPosition) = FindFirstNumber(item);
                (var firstNumberStringChar, int firstNumberStringPosition) = FindFirstStringNumber(item);
                if (firstNumberStringPosition < firstNumberPosition)
                {
                    first = firstNumberStringChar;
                }

                (var last, int lastNumberPosition) = FindLastNumber(item);
                (var lastNumberStringChar, int lastNumberStringPosition) = FindLastStringNumber(item);
                if (lastNumberStringPosition > lastNumberPosition)
                {
                    last = lastNumberStringChar;
                }

                char[] chars = { first, last };
                string s = new string(chars);
                sum += int.Parse(s);
            }

            return sum;
        }

        private static (char last, int numberPosition) FindLastNumber(string item)
        {
            var last = '0';
            int numberPosition = 0;
            for (int i = item.Length - 1; i >= 0; i--)
            {
                var c = item[i];
                if (char.IsNumber(c))
                {
                    numberPosition = i;
                    last = c;
                    break;
                }
            }

            return (last, numberPosition);
        }

        private static (char first, int numberPosition) FindFirstNumber(string item)
        {
            char first = '0';
            int numberPosition = 0;
            for (int i = 0; i < item.Length; i++)
            {
                var c = item[i];
                if (char.IsNumber(c))
                {
                    numberPosition = i;
                    first = c;
                    break;
                }

            }

            return (first, numberPosition);
        }

        internal static (char first, int numberPosition) FindFirstStringNumber(string item)
        {
            var numbersInItem = new Dictionary<string, int>() 
            { 
                { "one", int.MaxValue },
                { "two", int.MaxValue },
                { "three", int.MaxValue },
                { "four", int.MaxValue },
                { "five", int.MaxValue },
                { "six", int.MaxValue },
                { "seven", int.MaxValue },
                { "eight", int.MaxValue },
                { "nine", int.MaxValue }
            };

            foreach (var key in numbersInItem.Keys)
            {
                var i = item.IndexOf(key, 0);
                if (i != -1)
                {
                    numbersInItem[key] = i;
                }
            }

            char first = '0';
            int numberPosition = int.MaxValue;
            foreach(var key in numbersInItem.Keys)
            {
                if (numbersInItem[key] < numberPosition)
                {
                    first = TranslateStringNumberToChar(key);
                    numberPosition = numbersInItem[key];
                }
            }

            return (first, numberPosition);
        }

        internal static (char first, int numberPosition) FindLastStringNumber(string item)
        {
            var numbersInItem = new Dictionary<string, int>()
            {
                { "one", int.MinValue },
                { "two", int.MinValue },
                { "three", int.MinValue },
                { "four", int.MinValue },
                { "five", int.MinValue },
                { "six", int.MinValue },
                { "seven", int.MinValue },
                { "eight", int.MinValue },
                { "nine", int.MinValue }
            };

            foreach (var key in numbersInItem.Keys)
            {
                var i = item.LastIndexOf(key, item.Length - 1);
                if (i != -1)
                {
                    numbersInItem[key] = i;
                }
            }

            char last = '0';
            int numberPosition = int.MinValue;
            foreach (var key in numbersInItem.Keys)
            {
                if (numbersInItem[key] > numberPosition)
                {
                    last = TranslateStringNumberToChar(key);
                    numberPosition = numbersInItem[key];
                }
            }

            return (last, numberPosition);
        }

        private static char TranslateStringNumberToChar(string stringNumber)
        {
            switch (stringNumber)
            {
                case "one":
                    return '1';
                case "two":
                    return '2';
                case "three":
                    return '3';
                case "four":
                    return '4';
                case "five":
                    return '5';
                case "six":
                    return '6';
                case "seven":
                    return '7';
                case "eight":
                    return '8';
                case "nine":
                    return '9';
                default:
                    throw new InvalidDataException($"This is unexpected input: {stringNumber}");
            }
        }
    }
}