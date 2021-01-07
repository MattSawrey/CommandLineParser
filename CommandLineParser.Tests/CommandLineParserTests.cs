using Xunit;
using Xunit.Abstractions;

namespace CommandLineParser.Tests
{
    public class CommandLineParserTests
    {
        private readonly ITestOutputHelper testOutputHelper;
        private static string[] args = new []{ "--n", "25", "--d", "This is a test description" };

        public CommandLineParserTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestIntegerInput()
        {
            var options = new CommandLineParserOptions("--");
            var parser = new CommandLineParser<Parameters>(options);

            var parameters = parser.ParseOptionsFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "This is the value", parameters.Number.ToString()));
            testOutputHelper.WriteLine("Thank you very much for running this test");

            Assert.True(parameters.Number == 25);
        }

        [Fact]
        public void TestStringInput()
        {
            var options = new CommandLineParserOptions("--");
            var parser = new CommandLineParser<Parameters>(options);

            var parameters = parser.ParseOptionsFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "This is the value", parameters.Description.ToString()));
            testOutputHelper.WriteLine("Thank you very much for running this test");

            Assert.True(parameters.Description == "This is a test description");
        }
    }

    /// <summary>
    /// Test Model
    /// </summary>
    public class Parameters
    { 
        [CommandLineArgumentFlag("n")]
        public int Number { get; set; }

        [CommandLineArgumentFlag("d")]
        public string Description { get; set; }
    }
}
