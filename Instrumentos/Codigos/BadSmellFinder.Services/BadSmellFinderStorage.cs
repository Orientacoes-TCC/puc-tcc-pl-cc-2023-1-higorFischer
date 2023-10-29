using BadSmellFinder.Domain.Entities;

namespace BadSmellFinder.Services;

public class BadSmellFinderStorage
{
	public List<CodeAnalysis> Analyses { get; private set; } = new List<CodeAnalysis>();
	public ProjectAnalysis ProjectAnalysis { get; private set; } = new ProjectAnalysis();
	public CodeConfig Config { get; private set; }

	public void AddConfig(CodeConfig config)
	{
		Config = config;
	}

	public void Clear()
	{
		Analyses = new List<CodeAnalysis>();
		ProjectAnalysis = new ProjectAnalysis();
	}
}