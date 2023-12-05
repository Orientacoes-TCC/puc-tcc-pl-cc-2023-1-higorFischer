using BadSmellFinder.Domain.Entities;

namespace BadSmellFinder.Services;

public class BadSmellFinderStorage
{
	public List<CodeAnalysis> Analyses { get; private set; } = new List<CodeAnalysis>();
	public ProjectAnalysis ProjectAnalysis { get; private set; } = new ProjectAnalysis();
	public CodeConfig Config { get; private set; }
	public IEnumerable<CodeConfig> Hints { get; private set; } = new List<CodeConfig>();

	public void AddConfig(CodeConfig config)
	{
		Config = config;
	}

	public void AddHints(IDictionary<string, CodeConfig> hints)
	{
		Hints = hints.Select(c =>
		{
			c.Value.Name = c.Key;
			return c.Value;
		});
	}


	public void Clear()
	{
		Analyses = new List<CodeAnalysis>();
		ProjectAnalysis = new ProjectAnalysis();
		Hints = new List<CodeConfig>();
	}
}