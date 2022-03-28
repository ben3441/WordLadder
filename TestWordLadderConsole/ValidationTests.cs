using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordLadderConsole;

namespace TestWordLadderConsole;

[TestClass]
internal class ValidationTests
{
    [TestMethod]
    public void Result_file_must_be_valid_file_path()
    {
        var validationResult = Validation.ValidateResultFile("!!!", "test name");

    }

}