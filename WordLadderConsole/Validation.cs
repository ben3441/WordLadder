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

        return Result.Success();
    }
}