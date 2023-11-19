using BadSmellFinder.Domain.Entities;

namespace BadSmellFinder.Services;

public class BadSmellFinderStorage
{
	public List<CodeAnalysis> Analyses { get; private set; } = new List<CodeAnalysis>();
	public ProjectAnalysis ProjectAnalysis { get; private set; } = new ProjectAnalysis();
	public CodeConfig Config { get; private set; }
	public CodeConfig? ManualConfig { get; private set; }

	public void AddConfig(CodeConfig config)
	{
		Config = config;
	}

	public void AddManualConfig(CodeConfig config)
	{
		ManualConfig = config;
	}

	public void Clear()
	{
		Analyses = new List<CodeAnalysis>();
		ProjectAnalysis = new ProjectAnalysis();
	}
}