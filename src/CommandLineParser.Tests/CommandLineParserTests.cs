using CommandLineParser.Attributes;
using System.Reflection;
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
            var args = new[] { "test", parameterFlagPrefix + "s", expectedValue };

            var options = new CommandLineParserOptions(parameterFlagPrefix, Assembly.GetExecutingAssembly());
            var parser = new CommandLineParser(options);

            var command = parser.ParseCommandFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", command.StringParameter.ToString()));

            Assert.True(command.StringParameter == expectedValue);
        }

        [Fact]
        public void TestCharacterInput()
        {
            char expectedValue = 'a';
            var args = new[] { "test", parameterFlagPrefix + "c", expectedValue.ToString() };

            var options = new CommandLineParserOptions(parameterFlagPrefix, Assembly.GetExecutingAssembly());
            var parser = new CommandLineParser(options);

            var command = parser.ParseCommandFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", command.CharacterParameter.ToString()));

            Assert.True(command.CharacterParameter == expectedValue);
        }

        [Fact]
        public void TestIntegerInput()
        {
            int expectedValue = 25;
            var args = new[] { "test", parameterFlagPrefix + "i", expectedValue.ToString() };

            var options = new CommandLineParserOptions(parameterFlagPrefix, Assembly.GetExecutingAssembly());
            var parser = new CommandLineParser(options);

            var command = parser.ParseCommandFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", command.IntegerParameter.ToString()));

            Assert.True(command.IntegerParameter == expectedValue);
        }

        [Fact]
        public void TestFloatInput()
        {
            float expectedValue = 0.167f;
            string[] args = new[] { "test", parameterFlagPrefix + "f", expectedValue.ToString() };

            var options = new CommandLineParserOptions(parameterFlagPrefix, Assembly.GetExecutingAssembly());
            var parser = new CommandLineParser(options);

            var command = parser.ParseCommandFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", command.FloatParameter.ToString()));

            Assert.True(command.FloatParameter == expectedValue);
        }

        [Fact]
        public void TestDoubleInput()
        {
            double expectedValue = 0.856d;
            string[] args = new[] { "test", parameterFlagPrefix + "d", expectedValue.ToString() };

            var options = new CommandLineParserOptions(parameterFlagPrefix, Assembly.GetExecutingAssembly());
            var parser = new CommandLineParser(options);

            var command = parser.ParseCommandFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", command.DoubleParameter.ToString()));

            Assert.True(command.DoubleParameter == expectedValue);
        }

        [Fact]
        public void TestDecimalInput()
        {
            decimal expectedValue = 3.856m;
            string[] args = new[] { "test", parameterFlagPrefix + "m", expectedValue.ToString() };

            var options = new CommandLineParserOptions(parameterFlagPrefix, Assembly.GetExecutingAssembly());
            var parser = new CommandLineParser(options);

            var command = parser.ParseCommandFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", command.DecimalParameter.ToString()));

            Assert.True(command.DecimalParameter == expectedValue);
        }

        [Fact]
        public void TestBoolInput()
        {
            bool expectedValue = true;
            string[] args = new[] { "test", parameterFlagPrefix + "b", expectedValue.ToString() };

            var options = new CommandLineParserOptions(parameterFlagPrefix, Assembly.GetExecutingAssembly());
            var parser = new CommandLineParser(options);

            var command = parser.ParseCommandFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", command.BooleanParameter.ToString()));
            testOutputHelper.WriteLine("Thank you very much for running this test");

            Assert.True(command.BooleanParameter == expectedValue);
        }
    }

    /// <summary>
    /// Test Model
    /// </summary>
    [Command("test", "A command used in these tests")]
    public class TestCommand
    {
        [Parameter("string", "s", "The string test value")]
        public string StringParameter { get; set; }

        [Parameter("char", "c", "The character test value")]
        public char CharacterParameter { get; set; }

        [Parameter("int", "i", "The integer test value")]
        public int IntegerParameter { get; set; }

        [Parameter("float", "f", "The float test value")]
        public float FloatParameter { get; set; }

        [Parameter("double", "d", "The double test value")]
        public double DoubleParameter { get; set; }

        [Parameter("decimal", "m", "The double test value")]
        public decimal DecimalParameter { get; set; }

        [Parameter("boolean", "b", "The boolean test value")]
        public bool BooleanParameter { get; set; }
    }
}
