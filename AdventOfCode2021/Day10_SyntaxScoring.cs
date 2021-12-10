namespace AdventOfCode2021
{
    public static class Day10_SyntaxScoring
    {
        public static int CalculateInvalidScore(string[] input)
        {
            // Algo:
            // All “left” remaining => incomplete
            // Nothing remaining => complete
            // Loop on chunks until either incomplete, complete, or invalid
            //    Ignore valid characters
            //    Mark pairing adjacent brackets valid when considering pairs
            int score = 0;
            foreach(var line in input)
            {
                var validChunkChars = new bool[line.Length];
                while (true)
                {
                    if (validChunkChars.All(x => x))
                    {
                        break; // complete chunks (i.e., not corrupt or incomplete)
                    }

                    if (ProveIncomplete(line, validChunkChars))
                    {
                        break; // we now know chunks are incomplete
                    }

                    (bool invalid, char invalidChar) = MarkValidChunks(line, validChunkChars);
                    if (invalid)
                    {
                        score += ScoreInvalidChar(invalidChar);
                        break; // corrupt
                    }
                }
            }

            return score;
        }

        public static long CalculateIncompleteScore(string[] input)
        {
            var scores = new List<long>();
            foreach (var line in input)
            {
                var validChunkChars = new bool[line.Length];
                while (true)
                {
                    if (validChunkChars.All(x => x))
                    {
                        break; // complete chunks (i.e., not corrupt or incomplete)
                    }

                    if (ProveIncomplete(line, validChunkChars))
                    {
                        // we now know chunks are incomplete
                        scores.Add(FindScoreFromIncompleteChunks(line, validChunkChars));
                        break; 
                    }

                    (bool invalid, char invalidChar) = MarkValidChunks(line, validChunkChars);
                    if (invalid)
                    {
                        break; // corrupt
                    }
                }
            }

            // return middle score (list is always odd numbered)
            int middleIndex = (int) Math.Floor((double) (scores.Count / 2));
            return scores.OrderBy(x => x).ToArray()[middleIndex];
        }

        private static long FindScoreFromIncompleteChunks(string chunks, bool[] validChunkChars)
        {
            // assumes only left chars are marked as not valid yet

            long score = 0;
            for (int i = chunks.Length - 1; i >= 0; i--)
            {
                if (validChunkChars[i])
                {
                    continue;
                }

                score = (5 * score) + ScoreMissingChar(chunks[i]);
            }

            return score;
        }

        private static int ScoreInvalidChar(char invalidChar) =>
            invalidChar switch
            {
                ')' => 3,
                ']' => 57,
                '}' => 1197,
                '>' => 25137,
                _ => 0
            };

        private static int ScoreMissingChar(char incompleteChar) =>
            incompleteChar switch
            {
                '(' => 1,
                '[' => 2,
                '{' => 3,
                '<' => 4,
                _ => 0
            };


        private static (bool, char) MarkValidChunks(string chunks, bool[] validChunkChars)
        {
            for (int i = 0; i < chunks.Length; i++)
            {
                if (validChunkChars[i])
                {
                    continue;
                }

                if (rightSideChar(chunks[i]))
                {
                    continue;
                }

                // find index for next unvalidated char
                for (int iRight = i + 1; iRight < chunks.Length; iRight++)
                {
                    if (validChunkChars[iRight])
                    {
                        continue;
                    }

                    if (pairsMatch(chunks[i], chunks[iRight]))
                    {
                        validChunkChars[i] = true;
                        validChunkChars[iRight] = true;
                        i = iRight;
                        break;

                    }
                    else if (leftSideChar(chunks[iRight]))
                    {
                        i = iRight - 1;
                        break;
                    }
                    else
                    {
                        return (true, chunks[iRight]); // invalid char found
                    }
                }
            }

            return (false, '0');
        }

        private static bool pairsMatch(char leftSide, char rightSide) =>
            (leftSide, rightSide) switch
            {
                ('(', ')') => true,
                ('[', ']') => true,
                ('{', '}') => true,
                ('<', '>') => true,
                _ => false
              };

        private static bool leftSideChar(char leftSide) =>
            leftSide switch
            {
                '(' => true,
                '[' => true,
                '{' => true,
                '<' => true,
                _ => false
            };

        private static bool rightSideChar(char leftSide) =>
            leftSide switch
            {
                ')' => true,
                ']' => true,
                '}' => true,
                '>' => true,
                _ => false
            };


        private static bool ProveIncomplete(string chunks, bool[] validChunkChars)
        {
            // Assumses the chunks have been tested for a completeness and well formed (i.e., not corrupt or incomplete)

            // Are the chars only "left" side type?
            for(int i = 0; i < chunks.Length; i++)
            {
                if (validChunkChars[i])
                {
                    continue;
                }

                if (rightSideChar(chunks[i]))
                {
                    return false;
                }    
            }
            
            return true;
        }
    }
}
