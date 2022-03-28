using System;
using System.IO;
using System.Threading.Tasks;
using CliWrap;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestWordLadderConsole;

[TestClass]
[TestCategory("Integration")]
public sealed class AppIntegrationTests
{
    private const string DictionaryFile = "words-english.txt";
    private const string ResultsFile = "Results.txt";

    [DataTestMethod]// answers taken from online solver, all words are in dictionary
    [DataRow("hove", "huge", new[] { "hove", "love", "loge", "luge", "huge" })]
    [DataRow("mack", "many", new[] { "mack", "mace", "mane", "many" })]
    public async Task Can_solve_simple_word_ladders(string startWord, string endWord, string[] ladder)
    {
        var result = await RunAppAsync(startWord, endWord).ConfigureAwait(false);
        
        result.Should().BeEquivalentTo(ladder);
    }

    private static async Task<string[]> RunAppAsync(string startWord, string endWord)
    {
        await Cli.Wrap("WordLadderConsole")
            .WithArguments($"-d {DictionaryFile} -r {ResultsFile} -e {endWord} -s {startWord}")
            .WithStandardOutputPipe(PipeTarget.ToDelegate(Console.WriteLine))
            .WithStandardErrorPipe(PipeTarget.ToDelegate(Assert.Fail))
            .ExecuteAsync()
            .ConfigureAwait(false);

        return await File.ReadAllLinesAsync(ResultsFile).ConfigureAwait(false);
    }
}