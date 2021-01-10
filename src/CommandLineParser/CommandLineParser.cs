using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using CommandLineParser.Attributes;
using CommandLineParser.Models;

namespace CommandLineParser
{
    public class CommandLineParser
	{
		private readonly CommandLineParserOptions Options;
		private Dictionary<string, Command> PossibleCommands = new Dictionary<string, Command>();

		public CommandLineParser(CommandLineParserOptions options)
		{
			Options = options;
            InitialisePossibleCommandsDictionary(options.CommandsAssembly);
        }

		/// <summary>
		/// Initialises a dictionary of the command names against a Command Model that represents the type of the command and the property names
		/// </summary>
		/// <param name="commandsAssembly"></param>
		private void InitialisePossibleCommandsDictionary(Assembly commandsAssembly = null)
        {
			// If the assembly containing the Command objects has not been provided, assume they are held in the calling assembly
			if (commandsAssembly == null)
				commandsAssembly = Assembly.GetEntryAssembly();

			var commandTypes = commandsAssembly.GetTypes().Where(x => x.IsDefined(typeof(CommandAttribute)));

			// Loop through each class and parse its properties with the parameter attribute
			foreach (var commandType in commandTypes)
			{
				var commandAttribute = (CommandAttribute)commandType.GetCustomAttribute(typeof(CommandAttribute));
				var optionProperties = commandType.GetProperties();

				var commandProperties = new List<Property>();
				foreach (var optionProperty in optionProperties)
				{
					var parameterAttribute = (ArgumentAttribute)optionProperty.GetCustomAttribute(typeof(ArgumentAttribute));
					var propertyName = optionProperty.Name;
					var propertyIdentifiers = new[] { Options.ArgumentCommandPrefix + parameterAttribute.FlagCode, Options.ArgumentCommandPrefix + parameterAttribute.Name };
					commandProperties.Add(new Property(propertyIdentifiers, propertyName));
				}

                PossibleCommands.Add(commandAttribute.Name, new Command(commandType, commandProperties));
			}
		}

		/// <summary>
		/// For writing out the Possible Command and each Command's parameter details
		/// </summary>
		public void WriteOutPossibleCommandsDetails()
		{
			Console.WriteLine("Commands: ");
			foreach (string key in PossibleCommands.Keys) // Command level
			{
				Console.WriteLine(string.Format("{0}: {1}", "Command: ", key));
				foreach (var property in PossibleCommands[key].Properties) // Property level
				{
					Console.WriteLine(string.Format("{0}: {1}. {2}: {3}", "Parameter Identifiers", string.Join(", ", property.Identifiers), "Property Name", property.Name));
				}
			}
		}

		/// <summary>
		/// Dynamically parses a collection of input arguments to the appropriate command model with property values
		/// </summary>
		/// <param name="args">input arguments collection</param>
		/// <returns></returns>
		public dynamic ParseCommandFromArguments(string[] args)
		{
			if (!PossibleCommands.ContainsKey(args[0]))
			{
				Console.WriteLine(string.Format(@"The specified command ""({0})"" was not recognised.", args[0]));
				return null;
			}

			var command = PossibleCommands[args[0]];
			var result = Activator.CreateInstance(command.Type);
            var properties = command.Type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Step over the rest of the arguments, identify the parameters provided and parse the values to these parameters.
            for (int i = 1; i < args.Length; i++)
			{
				// Find the property that matches the property identifier
				var property = command.Properties.FirstOrDefault(x => x.Identifiers.Contains(args[i]));

				if (property != null)
				{
					var value = args[i + 1];

					// Grab a reference to the actual field on the object to be returned and try to populate it with the parsed value
					var field = properties.FirstOrDefault(x => x.Name.ToLower() == property.Name.ToLower());
					if (field != null)
					{
						try
						{
							field.SetValue(result, Convert.ChangeType(value, field.PropertyType), null);
						}
						catch (Exception e)
						{
							Console.WriteLine(e);
						}
					}
				}
            }

			return result;
		}
	}
}