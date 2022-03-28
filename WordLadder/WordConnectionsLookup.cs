namespace WordLadder;

internal static class WordConnectionsLookup
{
    internal static Dictionary<string, List<string>> Build(int wordLength, string[] wordDictionary)
    {
        var sanitizedWords = TrimToWordsOfCorrectLengthAndRemoveCasing(wordLength, wordDictionary);

        var wordToConnectingWords = BuildLookup(sanitizedWords);

        return wordToConnectingWords;
    }

    private static List<string> TrimToWordsOfCorrectLengthAndRemoveCasing(int wordLength, string[] wordDictionary)
        => wordDictionary
            .Where(w => w.Length == wordLength)
            .Select(x => x.ToLower())
            .Distinct()
            .ToList();

    private static Dictionary<string, List<string>> BuildLookup(IReadOnlyCollection<string> words)
        => words.ToDictionary(
            word => word,
            word => words.Where(w => WordsDifferBySingleCharacter(word, w)).ToList());

    private static bool WordsDifferBySingleCharacter(string word1, string word2)
    {
        var characterDiffCounter = 0;
        for (var index = 0; index < word1.Length; index++)
        {
            if (word1[index] != word2[index])
                characterDiffCounter++;
        }

        return characterDiffCounter == 1;
    }
}