using CSharpFunctionalExtensions;

namespace WordLadder;

public static class WordLadder
{
    public static Result<string[]> Solve(string startWord, string endWord, string[] wordDictionary)
    {
        var (_, isFailure, error) = Validation.Validate(startWord, endWord, wordDictionary);
        if (isFailure)
            return Result.Failure<string[]>(error);

        var wordConnectorLookup = WordLookup.Build(endWord, wordDictionary);

        var ladders = LadderFinder.Find(wordConnectorLookup, startWord, endWord);

        return ladders.Any()
            ? Result.Success(FindShortestLadder(ladders))
            : Result.Failure<string[]>(ErrorMessages.NoLadderFound);
    }

    private static string[] FindShortestLadder(IEnumerable<List<string>> ladders)
    {
        var orderedLadders = ladders.OrderBy(l => l.Count).ToArray();
        var ladderToReturn = orderedLadders.First();
        foreach (var ladder in orderedLadders.Skip(1))
        {
            if (ladder.Count > ladderToReturn.Count)
                break;

            if (SimpleHash(ladder) > SimpleHash(ladderToReturn)) // in order to always return same ladder when there are multiple solutions
                ladderToReturn = ladder;
        }

        return ladderToReturn.ToArray();

        static int SimpleHash(IEnumerable<string> ladder)
            => ladder.Select((rung, index) => (index + 1) *  rung.Select(c => (int)c).Sum()).Sum();
    }
}