namespace AdventOfCode2017
{
    public class Captcha
    {
        public static int SumCaptchaV1(string captcha)
        {
            string newCaptcha = $"{captcha}{captcha[0]}";

            List<int> matchedNumbers = new List<int>();

            char next = Char.MinValue;
            foreach (char c in newCaptcha)
            {
                if (c == next)
                {
                    if (int.TryParse(c.ToString(), out int number))
                    {
                        matchedNumbers.Add(number);
                    }
                }
                next = c;
            }

            return matchedNumbers.Sum();
        }

        public static int SumCaptchaV2(string captcha)
        {
            int halfSize = captcha.Length / 2;
            string newCaptcha = $"{captcha}{captcha.Substring(0, halfSize)}";

            List<int> matchedNumbers = new List<int>();

            int i = 0;
            
            foreach (char c in captcha)
            {
                char halfAhead = newCaptcha[halfSize + i];
                if (c == halfAhead)
                {
                    if (int.TryParse(c.ToString(), out int number))
                    {
                        matchedNumbers.Add(number);
                    }
                }

                i++;
            }

            return matchedNumbers.Sum();
        }
    }
}
