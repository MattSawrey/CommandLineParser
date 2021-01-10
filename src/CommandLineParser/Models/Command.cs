using System;
using System.Collections.Generic;

namespace CommandLineParser.Models
{
    public class Command
	{
        public Command(Type type, IEnumerable<Property> properties)
        {
			Type = type;
            Properties = properties;
        }

		public Type Type { get; set; }

        public IEnumerable<Property> Properties { get; set; }
	}
}