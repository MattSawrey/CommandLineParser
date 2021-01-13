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

        [Theory]
        [InlineData("This is a test description")]
        [InlineData("")]
        public void TestStringInput(string expectedValue)
        {
            var args = new[] { "test", parameterFlagPrefix + "s", expectedValue };

            var options = new CommandLineParserOptions(parameterFlagPrefix, Assembly.GetExecutingAssembly());
            var parser = new CommandLineParser(options);

            var command = parser.ParseCommandFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", command.StringParameter.ToString()));

            Assert.True(command.StringParameter == expectedValue);
        }

        [Theory]
        [InlineData('a')]
        [InlineData(' ')]
        [InlineData(char.MinValue)]
        [InlineData(char.MaxValue)]
        public void TestCharacterInput(char expectedValue)
        {
            var args = new[] { "test", parameterFlagPrefix + "c", expectedValue.ToString() };

            var options = new CommandLineParserOptions(parameterFlagPrefix, Assembly.GetExecutingAssembly());
            var parser = new CommandLineParser(options);

            var command = parser.ParseCommandFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", command.CharacterParameter.ToString()));

            Assert.True(command.CharacterParameter == expectedValue);
        }

        [Theory]
        [InlineData(25)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        [InlineData(0)]
        public void TestIntegerInput(int expectedValue)
        {
            var args = new[] { "test", parameterFlagPrefix + "i", expectedValue.ToString() };

            var options = new CommandLineParserOptions(parameterFlagPrefix, Assembly.GetExecutingAssembly());
            var parser = new CommandLineParser(options);

            var command = parser.ParseCommandFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", command.IntegerParameter.ToString()));

            Assert.True(command.IntegerParameter == expectedValue);
        }

        [Theory]
        [InlineData(25)]
        [InlineData(uint.MaxValue)]
        [InlineData(uint.MinValue)]
        public void TestUnsignedIntegerInput(uint expectedValue)
        {
            var args = new[] { "test", parameterFlagPrefix + "ui", expectedValue.ToString() };

            var options = new CommandLineParserOptions(parameterFlagPrefix, Assembly.GetExecutingAssembly());
            var parser = new CommandLineParser(options);

            var command = parser.ParseCommandFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", command.UnsignedIntegerParameter.ToString()));

            Assert.True(command.UnsignedIntegerParameter == expectedValue);
        }

        [Theory]
        [InlineData(0.167f)]
        [InlineData(float.MaxValue)]
        [InlineData(float.MinValue)]
        [InlineData(0f)]
        public void TestFloatInput(float expectedValue)
        {
            string[] args = new[] { "test", parameterFlagPrefix + "f", expectedValue.ToString() };

            var options = new CommandLineParserOptions(parameterFlagPrefix, Assembly.GetExecutingAssembly());
            var parser = new CommandLineParser(options);

            var command = parser.ParseCommandFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", command.FloatParameter.ToString()));

            Assert.True(command.FloatParameter == expectedValue);
        }

        [Theory]
        [InlineData(0.856d)]
        [InlineData(double.MaxValue)]
        [InlineData(double.MinValue)]
        public void TestDoubleInput(double expectedValue)
        {
            string[] args = new[] { "test", parameterFlagPrefix + "d", expectedValue.ToString() };

            var options = new CommandLineParserOptions(parameterFlagPrefix, Assembly.GetExecutingAssembly());
            var parser = new CommandLineParser(options);

            var command = parser.ParseCommandFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", command.DoubleParameter.ToString()));

            Assert.True(command.DoubleParameter == expectedValue);
        }

        // TODO - decimals are not usable with attributes in C#, so these constant string representations of decimal values are a hacky way of getting around that.
        // https://stackoverflow.com/questions/507528/use-decimal-values-as-attribute-params-in-c
        private const string decimalTestValue1 = "3.856";
        private const string decimalTestValue2 = "79,228,162,514,264,337,593,543,950,335";
        private const string decimalTestValue3 = "-79,228,162,514,264,337,593,543,950,335";
        [Theory]
        [InlineData(decimalTestValue1)]
        [InlineData(decimalTestValue2)]
        [InlineData(decimalTestValue3)]
        public void TestDecimalInput(string expectedValue)
        {
            string[] args = new[] { "test", parameterFlagPrefix + "m", expectedValue };

            var options = new CommandLineParserOptions(parameterFlagPrefix, Assembly.GetExecutingAssembly());
            var parser = new CommandLineParser(options);

            var command = parser.ParseCommandFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", command.DecimalParameter.ToString()));

            decimal testNumber;
            decimal.TryParse(expectedValue, out testNumber);

            Assert.True(command.DecimalParameter == decimal.Parse(expectedValue));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TestBoolInput(bool expectedValue)
        {
            string[] args = new[] { "test", parameterFlagPrefix + "b", expectedValue.ToString() };

            var options = new CommandLineParserOptions(parameterFlagPrefix, Assembly.GetExecutingAssembly());
            var parser = new CommandLineParser(options);

            var command = parser.ParseCommandFromArguments(args);

            testOutputHelper.WriteLine(string.Format("{0}: {1}", "Parsed value: ", command.BooleanParameter.ToString()));
            testOutputHelper.WriteLine("Thank you very much for running this test");

            Assert.True(command.BooleanParameter == expectedValue);
        }
    }
}
