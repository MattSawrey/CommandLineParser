using System;

namespace CommandLineParser.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ArgumentAttribute : Attribute
    {
        public ArgumentAttribute(string name, string flagCode, string description)
        {
            Name = name;
            FlagCode = flagCode;
            Description = description;
        }

        public string Name { get; set; }

        public string FlagCode { get; set; }

        public string Description { get; set; }
    }
}
