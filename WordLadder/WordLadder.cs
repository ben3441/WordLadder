using CSharpFunctionalExtensions;

namespace WordLadder;

public static class WordLadder
{
    public static Result<string[]> Solve(string startWord, string endWord, string[] wordDictionary)
    {
        var (_, isFailure, error) = Validation.Validate(startWord, endWord, wordDictionary);
        if (isFailure)
            return Result.Failure<string[]>(error);

        var wordConnectorLookup = WordConnectionsLookup.Build(startWord.Length, wordDictionary);

        var ladders = LadderFinder.Find(wordConnectorLookup, startWord, endWord);

        return ladders.Any()
            ? Result.Success(FindShortestLadder(ladders))
            : Result.Failure<string[]>(ErrorMessages.NoLadderFound);
    }

    private static string[] FindShortestLadder(IEnumerable<List<string>> ladders) 
        => ladders.OrderBy(l => l.Count).First().ToArray();
}