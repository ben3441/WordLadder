using CSharpFunctionalExtensions;

namespace WordLadderConsole;

internal static class Validation
{
    internal static Result ValidateOptions(ProgramOptions options)
    {
        if (!File.Exists(options.DictionaryFilePath))
            return Result.Failure(
                $"Could not find file: '{options.DictionaryFilePath}' for commandline argument: '{nameof(options.DictionaryFilePath)}'");

        if (string.IsNullOrWhiteSpace(options.StartWord))
            return Result.Failure($"Commandline argument '{nameof(options.StartWord)}' not set");

        if (string.IsNullOrWhiteSpace(options.EndWord))
            return Result.Failure($"Commandline argument '{nameof(options.EndWord)}' not set");

        if (options.StartWord.Length != options.EndWord.Length)
            return Result.Failure(
                $"Commandline argument '{nameof(options.StartWord)}' must have the same number of letters as Commandline argument '{nameof(options.EndWord)}'");

        return ValidateResultFile(options.ResultFile, nameof(options.ResultFile));
    }

    internal static Result ValidateResultFile(string filePath, string optionName)
    {
        try
        {
            var _ = new FileInfo(filePath);
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure(
                $"Commandline argument '{optionName}' with value: '{filePath}' is invalid with error: {Environment.NewLine}{e.Message}");
        }
    }
}