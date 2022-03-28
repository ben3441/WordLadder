namespace WordLadder;

internal static class LadderFinder
{
    internal static List<List<string>> Find(Dictionary<string, List<string>> indexedWords, string startWord, string endWord)
    {
        // search word paths
        var laddersBruteForce = new List<List<string>>();
        var ladder = new List<string> { startWord };

        IterateAllWordCombinations(ladder);

        void IterateAllWordCombinations(List<string> currentLadder)
        {
            var connectors = indexedWords[currentLadder.Last()];

            foreach (var connector in connectors)
            {
                if (currentLadder.Contains(connector))
                    continue;

                var extendedLadder = new List<string>(currentLadder) { connector };

                if (connector == endWord)
                {
                    laddersBruteForce.Add(extendedLadder);
                    break;
                }
                IterateAllWordCombinations(extendedLadder);
            }
        }

        return laddersBruteForce;
    }
}