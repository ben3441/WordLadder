using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CliWrap;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordLadderConsole;

namespace TestWordLadderConsole;

[TestClass]
[TestCategory("Integration")]
public sealed class AppAcceptanceTests
{
    private const string DictionaryFile = "words-english.txt";
    private const string ResultsFile = "Results.txt";

    [DataTestMethod]// answers taken from online solver, all words are in dictionary
    [DataRow("hove", "huge", new[] { "hove", "love", "loge", "luge", "huge" })]
    [DataRow("mack", "many", new[] { "mack", "mark", "mary", "many" })]
    public async Task Can_solve_simple_word_ladders(string startWord, string endWord, string[] ladder)
    {
        var result = await RunAppAsync(BuildConsoleArgs(startWord, endWord)).ConfigureAwait(false);
        
        result.Should().BeEquivalentTo(ladder);
    }

    [TestMethod]
    public async Task Should_fail_for_invalid_arguments()
    {
        Task RunWithMissingArg() => RunAppAsync(ConsoleArgsMissingEndWord());
        Task RunWithInvalidDictionaryFile() => RunAppAsync(ConsoleArgsDictionaryFileNotPresent());

        await AssertThrownForOptions(RunWithMissingArg, nameof(ProgramOptions.EndWord)).ConfigureAwait(false);
        await AssertThrownForOptions(RunWithInvalidDictionaryFile, nameof(ProgramOptions.DictionaryFilePath)).ConfigureAwait(false);

        Task AssertThrownForOptions(Func<Task> toRun, string option)
            => toRun.Should().ThrowAsync<AssertFailedException>().WithMessage($"*{option}*");
    }

    private static async Task<string[]> RunAppAsync(string consoleArgs)
    {
        var errors = new List<string>();
        await Cli.Wrap("WordLadderConsole")
            .WithValidation(CommandResultValidation.None)
            .WithArguments(consoleArgs)
            .WithStandardOutputPipe(PipeTarget.ToDelegate(Console.WriteLine))
            .WithStandardErrorPipe(PipeTarget.ToDelegate(error => errors.Add(error)))
            .ExecuteAsync()
            .ConfigureAwait(false);

        errors.Should().BeEmpty();

        return await File.ReadAllLinesAsync(ResultsFile).ConfigureAwait(false);
    }

    private static string BuildConsoleArgs(string startWord, string endWord)
        => $"-d {DictionaryFile} -r {ResultsFile} -e {endWord} -s {startWord}";

    private static string ConsoleArgsMissingEndWord()
        => $"-d {DictionaryFile} -r {ResultsFile} -s Test";

    private static string ConsoleArgsDictionaryFileNotPresent()
        => $"-d C:\\MadeUpFile.txt -r {ResultsFile} -e mack -s many";
}