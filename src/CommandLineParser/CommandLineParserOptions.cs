using System.Reflection;

namespace CommandLineParser
{
	public class CommandLineParserOptions
	{
		public CommandLineParserOptions(string argumentCommandPrefix, Assembly assembly = null)
		{
			ArgumentCommandPrefix = argumentCommandPrefix;
			CommandsAssembly = assembly;
		}

		public string ArgumentCommandPrefix { get; set; }

		public Assembly CommandsAssembly { get; set; }
	}
}