namespace CommandLineParser.Models
{
    public class Property
	{
        public Property(string[] identifiers, string name)
        {
            Identifiers = identifiers;
            Name = name;
        }

        public string[] Identifiers { get; set; }

		public string Name { get; set; }
	}
}