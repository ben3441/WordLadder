using CSharpFunctionalExtensions;
using static WordLadder.ErrorMessages;

namespace WordLadder;

internal static class Validation
{
    internal static Result Validate(string startWord, string endWord, string[] wordDictionary)
    {
        if (startWord.Length != endWord.Length)
            return Result.Failure<string[]>(StartEndWordsDifferentLengths);

        if (wordDictionary.Contains(startWord) == false)
            return Result.Failure(StartWordNotInDictionary);

        if (wordDictionary.Contains(endWord) == false)
            return Result.Failure(EndWordNotInDictionary);

        return Result.Success();
    }
}