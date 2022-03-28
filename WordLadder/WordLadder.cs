using CSharpFunctionalExtensions;

namespace WordLadder;

public static class WordLadder
{
    public static Result<string[]> Solve(string startWord, string endWord, string[] wordDictionary)
    {
        // validate inputs
        
        // trim dictionary for potential words
        var wordLength = startWord.Length;
        var wordsCorrectLength = wordDictionary
            .Where(w => w.Length == wordLength)
            .Select(x => x.ToLower())
            .ToList();

        // build lookup of word - words that are one letter different
        var wordToPotentialConnectors =
            wordsCorrectLength.ToDictionary(
                    word => word,
                    word => wordsCorrectLength.Where(w => WordsDifferBySingleCharacter(word, w)).ToList());

        // search word paths
        var laddersBruteForce = new List<List<string>>();
        var ladder = new List<string> {startWord};

        IterateAllWordCombinations(ladder);

        void IterateAllWordCombinations(List<string> currentLadder)
        {
            var connectors = wordToPotentialConnectors[currentLadder.Last()];

            foreach (var connector in connectors)
            {
                if (currentLadder.Contains(connector))
                    continue;

                var extendedLadder = new List<string>(currentLadder) {connector};

                if (connector == endWord)
                {
                    laddersBruteForce.Add(extendedLadder);
                    continue;
                }
                IterateAllWordCombinations(extendedLadder);
            }
        }

        return laddersBruteForce.Count > 0
            ? Result.Success(laddersBruteForce.OrderBy(l => l.Count).First().ToArray())
            : Result.Failure<string[]>("didn't work for some reason...");
        //var t = wordToPotentialConnectors[startWord].SelectMany()

        // find smallest path from start to finish


        return Result.Failure<string[]>("didn't work for some reason...");
    }



    private static bool WordsDifferBySingleCharacter(string word1, string word2)
    {
        var diffCounter = 0;
        for (var index = 0; index < word1.Length; index++)
        {
            var character = word1[index];

            if (character != word2[index])
                diffCounter++;
        }

        return diffCounter == 1;
    }
}