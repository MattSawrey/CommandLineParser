using CommandLineParser.Attributes;
using System;

namespace CommandLineParser.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new CommandLineParserOptions("--");
            var parser = new CommandLineParser(options);

            var command = parser.ParseCommandFromArguments(args);

            if (command == null) return;

            if (command.GetType() == typeof(NewCommand))
            {
                Console.WriteLine("You entered a new command");
            }
            else if (command.GetType() == typeof(CheckCommand))
            {
                Console.WriteLine("You entered a check command");
            }
        }
    }

    /// <summary>
    /// Test Command Models
    /// </summary>
    [Command("new", "The new command")]
    public class NewCommand
    {
        [Argument("num", "n", "The number value")]
        public int Number { get; set; }
    }

    [Command("check", "The new command")]
    public class CheckCommand
    {
        [Argument("outp", "o", "The output value")]
        public string Output { get; set; }

        [Argument("num", "n", "The times value")]
        public int Times { get; set; }
    }
}
