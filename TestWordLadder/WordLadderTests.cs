using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static WordLadder.ErrorMessages;

namespace TestWordLadder;

[TestClass]
public class WordLadderTests
{
    [TestMethod]
    public void Can_solve_word_ladder()
    {
        var dictionary = new[] {"hull", "hulp", "held", "help"};
        var result = WordLadder.WordLadder.Solve("help", "hull", dictionary);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo("help", "hulp", "hull");
    }

    [TestMethod]
    public void Can_solve_variable_length_ladders()
    {
        var dictionary = new[] {"hull", "hat", "hit", "hot", "pot", "pit", "petty", "house", "hulp", "held", "help"};
        var result = WordLadder.WordLadder.Solve("hat", "pot", dictionary);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo("hat", "hot", "pot");
    }

    [TestMethod]
    public void Can_handle_multiple_equally_valid_answers()
    {
        var dictionary = new[] { "hat", "hit", "hot", "pot", "pit", "pat" }; // connecting word could be 'hot' or 'pat'
        var result = WordLadder.WordLadder.Solve("hat", "pot", dictionary);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo("hat", "hot", "pot");
    }

    [TestMethod]
    public void Start_and_end_words_must_be_equal_length()
    {
        var result = WordLadder.WordLadder.Solve("hats", "pot", new []{"hats", "pot"});

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain(StartEndWordsDifferentLengths);
    }

    [TestMethod]
    public void Start_word_must_be_in_word_dictionary()
    {
        var result = WordLadder.WordLadder.Solve("hat", "pot", new[] { "pit", "pot" });

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain(StartWordNotInDictionary);
    }

    [TestMethod]
    public void End_word_must_be_in_word_dictionary()
    {
        var result = WordLadder.WordLadder.Solve("hat", "pet", new[] { "hat", "pot" });

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain(EndWordNotInDictionary);
    }

    [TestMethod]
    public void Error_returned_when_no_ladders_can_be_found()
    {
        var result = WordLadder.WordLadder.Solve("hat", "pet", new[] { "hat", "hot", "hit", "pet" });

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain(NoLadderFound);
    }
}