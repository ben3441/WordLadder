using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestWordLadder
{
    [TestClass]
    public class WordLadderTests
    {
        [TestMethod]
        public void Can_solve_word_ladder()
        {
            var result = WordLadder.WordLadder.Solve("help", "hull", new[] {"hull", "hulp", "held", "help"});

            result.IsSuccess.Should().BeTrue();
        }
    }
}