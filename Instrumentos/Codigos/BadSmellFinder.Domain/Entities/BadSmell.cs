namespace BadSmellFinder.Domain.Entities;

public class BadSmell
{
	public string Name { get; set; }
	public string Description { get; set; }
	public IDictionary<string, string> Parameters { get; } = new Dictionary<string, string>();

	public BadSmell(string name, string description)
	{
		Name = name;
		Description = description;
	}

	public BadSmell AddParameter(string key, string value)
	{
		Parameters.Add(key, value);
		return this;
	}

	public BadSmell AddParameter((string key, string value)[] values)
	{
		foreach(var value in values)
			Parameters.Add(value.key, value.value);
		return this;
	}
}
