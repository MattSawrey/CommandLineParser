using System;

namespace CommandLineParser
{
  public class CommandLineArgumentFlagAttribute : Attribute
  {
    public CommandLineArgumentFlagAttribute(string flagCode)
    {
      FlagCode = flagCode;
    }

    public string FlagCode { get; set; }
  }
}