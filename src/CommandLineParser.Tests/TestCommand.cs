using CommandLineParser.Attributes;

namespace CommandLineParser.Tests
{
    /// <summary>
    /// Test Model
    /// </summary>
    [Command("test", "A command used in these tests")]
    public class TestCommand
    {
        [Argument("string", "s", "The string test value")]
        public string StringParameter { get; set; }

        [Argument("char", "c", "The character test value")]
        public char CharacterParameter { get; set; }

        [Argument("int", "i", "The integer test value")]
        public int IntegerParameter { get; set; }

        [Argument("uint", "ui", "The integer test value")]
        public uint UnsignedIntegerParameter { get; set; }

        [Argument("float", "f", "The float test value")]
        public float FloatParameter { get; set; }

        [Argument("double", "d", "The double test value")]
        public double DoubleParameter { get; set; }

        [Argument("decimal", "m", "The double test value")]
        public decimal DecimalParameter { get; set; }

        [Argument("boolean", "b", "The boolean test value")]
        public bool BooleanParameter { get; set; }
    }
}
