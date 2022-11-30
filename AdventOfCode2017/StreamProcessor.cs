using System.Text;

namespace AdventOfCode2017
{
    public class StreamProcessor
    {
        public static (int score, int removedCharCount) Score(string stream)
        {
            var newStream = RemoveInvalidCharacters(stream);

            (var cleanStream, var removedCharCount) = RemoveGarbageCharacters(newStream);

            var score = DetermineScore(cleanStream);

            return (score, removedCharCount);
        }

        private static int DetermineScore(string cleanStream)
        {
            var incrementCount = 0;
            var score = 0;
            foreach (var character in cleanStream)
            {
                if (character == '{')
                {
                    incrementCount++;
                    score += incrementCount;
                }
                else if (character == '}')
                {
                    incrementCount--;
                }
            }
            return score;
        }

        private static (string cleanString, int removedCharCount) RemoveGarbageCharacters(string input)
        {
            StringBuilder cleanStream = new StringBuilder("");
            var removedCharCount = 0;
            for (var i = 0; i < input.Length; i++)
            {
                var character = input[i];
                if (character == '<')
                {
                    int startIndex = i;
                    while (i < input.Length && character != '>')
                    {
                        i++;
                        character = input[i];
                    }

                    // we didn't find the last closing '>' therefore include string
                    if (character != '>')
                    {
                        cleanStream.Append(input.Substring(startIndex));
                        break;
                    }

                    removedCharCount += (i - startIndex - 1);
                }
                else
                {
                    cleanStream.Append(character);
                }
            }

            return (cleanStream.ToString(), removedCharCount);
        }

        private static string RemoveInvalidCharacters(string input)
        {
            StringBuilder newStream = new StringBuilder("");
            for (var i = 0; i < input.Length; i++)
            {
                var character = input[i];
                if (character == '!')
                {
                    i++;
                    continue;
                }

                newStream.Append(character);
            }

            return newStream.ToString();
        }
    }
}
