namespace WordLadder;

internal static class WordLookup
{
    internal static Dictionary<string, List<string>> Build(string endWord, string[] wordDictionary)
    {
        var sanitizedWords = TrimToDistinctWordsOfCorrectLengthAndRemoveCasing(endWord.Length, wordDictionary);

        var wordToConnectingWords = BuildLookup(sanitizedWords, endWord);

        return wordToConnectingWords;
    }

    private static List<string> TrimToDistinctWordsOfCorrectLengthAndRemoveCasing(int wordLength, string[] wordDictionary)
        => wordDictionary
            .Where(w => w.Length == wordLength)
            .Select(x => x.ToLower())
            .Distinct()
            .ToList();

    private static Dictionary<string, List<string>> BuildLookup(IReadOnlyCollection<string> words, string endWord)
        => words.ToDictionary(
            word => word,
            word => words
                .Where(w => WordsDifferBySingleCharacter(word, w))
                .OrderByDescending(w => CharactersMatchingEndWord(endWord, w))
                .ToList());

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

    private static int CharactersMatchingEndWord(string endWord, string comparisonWord)
    {
        var characterMatchCounter = 0;
        for (var index = 0; index < endWord.Length; index++)
        {
            if (endWord[index] == comparisonWord[index])
                characterMatchCounter++;
        }

        return characterMatchCounter;
    }
}