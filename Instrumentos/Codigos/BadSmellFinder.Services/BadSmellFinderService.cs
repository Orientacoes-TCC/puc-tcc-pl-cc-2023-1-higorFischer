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
	readonly BadSmellFinderStorage Storage;

	public BadSmellFinderService(BadSmellFinderStorage storage)
	{
		CodeConfigBuilder = new CodeConfigBuilder(new QuartileCalculator());
		BadSmellCodeFinder = new BadSmellCodeFinder();
		RoslynAnalyzer = new RoslynAnalyzer();
		Storage = storage;
	}

	public IEnumerable<CodeAnalysis> Find()
	{
		var files = new FileReader().Read("E:\\42\\IsaBackend");

		var analysis = new List<CodeAnalysis>();
		foreach (var file in files)
		{
			var roslynAnalyzer = RoslynAnalyzer.Run(file.Key, file.Value);
			analysis.Add(roslynAnalyzer);
		}
		var config = CodeConfigBuilder.Build(analysis);
		Storage.AddConfig(config);
		BadSmellCodeFinder.Find(analysis, config);

		Storage.ProjectAnalysis.Add(analysis);
		return analysis.Where(c => c.BadSmells.Count() > 0);
	}
}