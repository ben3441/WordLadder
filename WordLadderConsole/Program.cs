using CommandLine;
using CSharpFunctionalExtensions;

namespace WordLadderConsole;

internal class Program
{
    public static void Main(string[] args)
        => Parser.Default.ParseArguments<ProgramOptions>(args)
            .WithParsed(Run);

    private static void Run(ProgramOptions options)
        => Validation.ValidateOptions(options)
            .Bind(() =>
                FileAccess.ReadWordDictionary(options.DictionaryFilePath)
                    .Bind(dictionary =>
                        WordLadder.WordLadder
                            .Solve(options.StartWord, options.EndWord, dictionary)
                            .Bind(result => FileAccess.WriteOutputFile(options.ResultFile, result))))
            .OnFailure(ReportError);

    private static void ReportError(string error)
        => Console.Error.WriteLine($"Failed to compute word ladder, error: '{error}'");
}