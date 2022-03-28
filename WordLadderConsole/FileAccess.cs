using CSharpFunctionalExtensions;

namespace WordLadderConsole;

internal static class FileAccess
{
    internal static Result<string[]> ReadWordDictionary(string filePath)
    {
        try
        {
            return Result.Success(File.ReadAllLines(filePath));
        }
        catch (Exception e)
        {
            return Result.Failure<string[]>($"Failed to read word dictionary with error: '{e.Message}'");
        }
    }

    internal static Result WriteOutputFile(string filePath, string[] wordLadder)
    {
        try
        {
            File.WriteAllLines(filePath, wordLadder);
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure($"Failed to write result file with error: '{e.Message}'");
        }
    }
}