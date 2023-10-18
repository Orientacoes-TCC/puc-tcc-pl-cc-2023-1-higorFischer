using BadSmell.Domain.Roslyn;
using BadSmellFinder.Domain.Entities;
using BadSmellFinder.Domain.Services;
using Util;

namespace BadSmellFinder.Services;

public class BadSmellFinderService
{
	readonly CodeConfigBuilder CodeConfigBuilder;
	readonly BadSmellCodeFinder BadSmellCodeFinder;
	readonly RoslynAnalyzer RoslynAnalyzer;

	public BadSmellFinderService()
	{
		CodeConfigBuilder = new CodeConfigBuilder(new QuartileCalculator());
		BadSmellCodeFinder = new BadSmellCodeFinder();
		RoslynAnalyzer = new RoslynAnalyzer();
	}

	public IEnumerable<CodeAnalysis> Find()
	{
		var files = new FileReader().Read("E:\\42\\IsaBackend");

		var analysis = new List<CodeAnalysis>();
		foreach (var file in files)
			analysis.Add(RoslynAnalyzer.Run(file.Key, file.Value));

		BadSmellCodeFinder.Find(analysis, CodeConfigBuilder.Build(analysis));

		return analysis.Where(c => c.BadSmells.Count() > 0);
	}
}