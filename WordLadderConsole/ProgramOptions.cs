using CommandLine;

namespace WordLadderConsole;

public sealed class ProgramOptions
{
    [Option('d', "DictionaryFile", Required = true, HelpText = "Path to dictionary file, may be full or relative path" )]
    public string DictionaryFilePath { get; set; }

    [Option('s', "StartWord", Required = true, HelpText = "Word ladder start word")]
    public string StartWord { get; set; }

    [Option('e', "EndWord", Required = true, HelpText = "Word ladder end word")]
    public string EndWord { get; set; }

    [Option('r', "ResultFile", Required = true, HelpText = "Path to file where the computed word ladder will be saved, may be full or relative path")]
    public string ResultFile { get; set; }
}