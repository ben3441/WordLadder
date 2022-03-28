using CommandLine;
using CommandLine.Text;
using CSharpFunctionalExtensions;

namespace WordLadderConsole;

internal class Program
{
    public static void Main(string[] args)
        => Parser.Default.ParseArguments<ProgramOptions>(args)
            .WithParsed(Run)
            .WithNotParsed(errors =>
            {
                var sentenceBuilder = SentenceBuilder.Create();
                foreach (var error in errors)
                    Console.Error.WriteLine(sentenceBuilder.FormatError(error));
            });

    private static void Run(ProgramOptions options)
        => Validation.ValidateOptions(options)
            .Bind(() =>
            {
                var dictionary = FileAccess.ReadWordDictionary(options.DictionaryFilePath);

                return WordLadder.WordLadder
                    .Solve(options.StartWord, options.EndWord, dictionary)
                    .Tap(result => FileAccess.WriteOutputFile(options.ResultFile, result));
            })
            .OnFailure(ReportError);
    

    private static void ReportError(string error)
        => Console.Error.WriteLine($"Failed to compute word ladder, error: '{error}'");
}