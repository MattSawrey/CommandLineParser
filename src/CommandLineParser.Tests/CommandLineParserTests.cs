using Xunit;
using Xunit.Abstractions;

namespace CommandLineParser.Tests
{
    public class CommandLineParserTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        private static string parameterFlagPrefix = "--";

        public CommandLineParserTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestStringInput()
        {
            string expectedValue = "This is a test description";
            var args = new[] { parameterFlagPrefix + "s", expectedValue };

            var options = new CommandLineParserOptions(parameterFlagPrefix);
            var parser = new CommandLineParser<Parameters>(options);

            var parameters = parser.ParseOptionsFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", parameters.StringParameter.ToString()));

            Assert.True(parameters.StringParameter == expectedValue);
        }

        [Fact]
        public void TestCharacterInput()
        {
            char expectedValue = 'a';
            var args = new[] { parameterFlagPrefix + "c", expectedValue.ToString() };

            var options = new CommandLineParserOptions(parameterFlagPrefix);
            var parser = new CommandLineParser<Parameters>(options);

            var parameters = parser.ParseOptionsFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", parameters.CharacterParameter.ToString()));

            Assert.True(parameters.CharacterParameter == expectedValue);
        }

        [Fact]
        public void TestIntegerInput()
        {
            int expectedValue = 25;
            var args = new[] { parameterFlagPrefix + "i", expectedValue.ToString() };

            var options = new CommandLineParserOptions(parameterFlagPrefix);
            var parser = new CommandLineParser<Parameters>(options);

            var parameters = parser.ParseOptionsFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", parameters.IntegerParameter.ToString()));

            Assert.True(parameters.IntegerParameter == expectedValue);
        }

        [Fact]
        public void TestFloatInput()
        {
            float expectedValue = 0.167f;
            string[] args = new[] { parameterFlagPrefix + "f", expectedValue.ToString() };

            var options = new CommandLineParserOptions(parameterFlagPrefix);
            var parser = new CommandLineParser<Parameters>(options);

            var parameters = parser.ParseOptionsFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", parameters.FloatParameter.ToString()));

            Assert.True(parameters.FloatParameter == expectedValue);
        }

        [Fact]
        public void TestDoubleInput()
        {
            double expectedValue = 0.856d;
            string[] args = new[] { parameterFlagPrefix + "d", expectedValue.ToString() };

            var options = new CommandLineParserOptions(parameterFlagPrefix);
            var parser = new CommandLineParser<Parameters>(options);

            var parameters = parser.ParseOptionsFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", parameters.DoubleParameter.ToString()));

            Assert.True(parameters.DoubleParameter == expectedValue);
        }

        [Fact]
        public void TestDecimalInput()
        {
            decimal expectedValue = 3.856m;
            string[] args = new[] { parameterFlagPrefix + "m", expectedValue.ToString() };

            var options = new CommandLineParserOptions(parameterFlagPrefix);
            var parser = new CommandLineParser<Parameters>(options);

            var parameters = parser.ParseOptionsFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", parameters.DecimalParameter.ToString()));

            Assert.True(parameters.DecimalParameter == expectedValue);
        }

        [Fact]
        public void TestBoolInput()
        {
            bool expectedValue = true;
            string[] args = new[] { parameterFlagPrefix + "b", expectedValue.ToString() };

            var options = new CommandLineParserOptions(parameterFlagPrefix);
            var parser = new CommandLineParser<Parameters>(options);

            var parameters = parser.ParseOptionsFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", parameters.BooleanParameter.ToString()));
            testOutputHelper.WriteLine("Thank you very much for running this test");

            Assert.True(parameters.BooleanParameter == expectedValue);
        }
    }

    /// <summary>
    /// Test Model
    /// </summary>
    public class Parameters
    {
        [CommandLineArgumentFlag("s")]
        public string StringParameter { get; set; }

        [CommandLineArgumentFlag("c")]
        public char CharacterParameter { get; set; }

        [CommandLineArgumentFlag("i")]
        public int IntegerParameter { get; set; }

        [CommandLineArgumentFlag("f")]
        public float FloatParameter { get; set; }

        [CommandLineArgumentFlag("d")]
        public double DoubleParameter { get; set; }

        [CommandLineArgumentFlag("m")]
        public decimal DecimalParameter { get; set; }

        [CommandLineArgumentFlag("b")]
        public bool BooleanParameter { get; set; }
    }
}
