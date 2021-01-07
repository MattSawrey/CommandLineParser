namespace CommandLineParser
{
	public class CommandLineParserOptions
	{
		public CommandLineParserOptions(string argumentCommandPrefix)
		{
			ArgumentCommandPrefix = argumentCommandPrefix;
		}

		public string ArgumentCommandPrefix { get; set; }
	}
}