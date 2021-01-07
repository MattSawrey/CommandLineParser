using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace CommandLineParser
{
	public class CommandLineParser<T> where T : class, new()
	{
		private readonly CommandLineParserOptions Options;
		private Dictionary<string, string> PossibleParameterFlags = new Dictionary<string, string>();

		public CommandLineParser(CommandLineParserOptions options)
		{
			Options = options;
			InitialisePossibleParamtersDictionary();
			WriteOutPossibleParameters();
		}

		/// <summary>
		/// Initialise a dictionary of the parameter input flag strings against their correspending field names. E.G "--n" for "public string Name { get; set;}"
		/// </summary>
		private void InitialisePossibleParamtersDictionary()
		{
			var optionProperties = typeof(T).GetProperties();
			for (int i = 0; i < optionProperties.Length; i++)
			{
				var attributes = optionProperties[i].GetCustomAttributes().ToList();
				for (int y = 0; y < attributes.Count(); y++)
				{
					if (attributes[y].GetType() == typeof(CommandLineArgumentFlagAttribute))
					{
						CommandLineArgumentFlagAttribute attr = (CommandLineArgumentFlagAttribute)attributes[y];
						PossibleParameterFlags.Add(Options.ArgumentCommandPrefix + attr.FlagCode, optionProperties[i].Name);
					}
				}
			}
		}

		/// <summary>
		/// For writing out the Possible Parameters collection
		/// </summary>
		public void WriteOutPossibleParameters()
		{
            foreach (string key in PossibleParameterFlags.Keys)
            {
                Console.WriteLine(string.Format("{0}: {1}", key, PossibleParameterFlags[key]));
            }
        }

		/// <summary>
		/// Parses a collection of CLI argument values to the provided model shape
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public T ParseOptionsFromArguments(string[] args)
		{
			var result = new T();
			var type = typeof(T);
			var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

			// Walk over the input arguments and attempt to match them with the paramter flags
			for (int i = 0; i < args.Length; i++)
			{
				if (PossibleParameterFlags.ContainsKey(args[i]))
				{
					var fieldName = PossibleParameterFlags[args[i]];
					Console.WriteLine(string.Format("{0}, gives the field name: {1}", args[i], fieldName));

					// Want to get the entered value, which should be the next value here and try and pass it to the type
					var value = args[i + 1];
					Console.WriteLine(value);

					// Try and set the property value on the generic object
					var property = properties.FirstOrDefault(x => x.Name.ToLower() == fieldName.ToLower());
					if (property != null)
					{
						property.SetValue(result, Convert.ChangeType(value, property.PropertyType), null);
					}
				}
			}

			return result;
		}
	}
}